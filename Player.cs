using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] 
	public float Velocidade = 500.0f;
	
	[Export]
	public PackedScene CenaDoTiro;
	
	private float tempoRecarga = 0.5f;
	
	private float cronometroTiro = 0.0f;
	
	private bool invencivel = false;
	
	private float cronometroInvencivel = 0.0f;
	
	private float tempoIframe = 2.0f;
	
	private bool tiroRapido = false;
	private float cronometroTiroRapido = 0.0f;
	private float tempoOriginal;
	
	public override void _PhysicsProcess(double delta){
	
		Vector2 direcao = Input.GetVector("ui_left","ui_right","ui_up", "ui_down");
		
		//Vai aplicar a velocidade
		Velocity = direcao * Velocidade;
		MoveAndSlide();
		
		if(cronometroTiro > 0){
			cronometroTiro -= (float)delta;
		}
		
		if(Input.IsActionPressed("ui_accept")){
			if(cronometroTiro <= 0){
			Atirar();
			cronometroTiro = tempoRecarga;
			
			AudioStreamPlayer somDoLaser = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
			
			somDoLaser.Play();
			}
			
		}
		
		if (invencivel){
			cronometroInvencivel -= (float)delta;
			
			GetNode<Sprite2D>("Sprite2D").Visible = (Mathf.RoundToInt(cronometroInvencivel * 15) % 2 == 0);
			
			if (cronometroInvencivel <= 0){
				invencivel = false;
				GetNode<Sprite2D>("Sprite2D").Visible = true;
			}
		}
		
		if(tiroRapido){
			cronometroTiroRapido -= (float)delta;
			if(cronometroTiroRapido <= 0){
				tiroRapido = false;
				tempoRecarga = tempoOriginal;
			}
		}
	}
	private void Atirar(){
		//Cria uma cópia do tiro
		Node2D novoTiro = CenaDoTiro.Instantiate<Node2D>();
		
		//Coloca a bala na posição certa
		novoTiro.GlobalPosition = this.GlobalPosition;
		
		//joga a bala na cena principal.
		GetParent().AddChild(novoTiro);
		GetNode<AudioStreamPlayer>("TiroPowerUp").Play();
		
	}
	
	public void ReceberDano(){
		if (invencivel){
			return;
		}
		
		invencivel = true;
		cronometroInvencivel = tempoIframe;
		
		Mundo mundoAtual = (Mundo)GetTree().CurrentScene;
	mundoAtual.PerderVida();
	}
	
	public void AtivarTiroRapido(){
	if (!tiroRapido){
		tempoOriginal = tempoRecarga;
	}
	tiroRapido = true;
	cronometroTiroRapido = 5.0f;
	
	tempoRecarga = 0.1f;
	
	
	}
}
