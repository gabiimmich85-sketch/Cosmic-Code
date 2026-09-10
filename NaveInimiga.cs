using Godot;
using System;

public partial class NaveInimiga : Area2D
{
	[Export]
public PackedScene CenaDeExplosao {get; set;}	
[Export]
public PackedScene CenaPowerUp;
public float Velocidade = 100.0f;
private RandomNumberGenerator geradorAleatorio = new RandomNumberGenerator();

private Node2D alvoJogador;

	public override void _Ready()
	{
		alvoJogador = GetTree().GetFirstNodeInGroup("Jogador") as Node2D;
		
		AreaEntered += OnAreaEntered;
		
		BodyEntered += OnBodyEntered;
	}

	
	public override void _Process(double delta)
	{
		if (alvoJogador != null && IsInstanceValid(alvoJogador)){
			
			Vector2 direcao = (alvoJogador.GlobalPosition - GlobalPosition).Normalized();
			
			GlobalPosition += direcao * Velocidade * (float)delta;
			
			//LookAt(alvoJogador.Position);
		}
		else{
			GlobalPosition += new Vector2(0, 1) * Velocidade * (float)delta;
		}
	}
	private void OnAreaEntered(Area2D area){
		//verifica se a área é da classe "tiro"
		if(area is Tiro) {
			
			Mundo mundoAtual = (Mundo)GetTree().CurrentScene;
			
			mundoAtual.AdicionarPontos();
			
			CpuParticles2D explosaoAtual = CenaDeExplosao.Instantiate<CpuParticles2D>();
			
			GetTree().CurrentScene.AddChild(explosaoAtual);
			
			explosaoAtual.GlobalPosition = this.GlobalPosition;
			
			explosaoAtual.Emitting = true;
			
			Shake camera = GetViewport().GetCamera2D() as Shake;
		if (camera != null){
			camera.Tremer(20.0f);
		} 
		
		if (geradorAleatorio.Randf() <= 0.10f){
			Node2D item = CenaPowerUp.Instantiate<Node2D>();
			item.GlobalPosition = this.GlobalPosition;
			GetTree().CurrentScene.AddChild(item);
		}
			//QueueFree() = comando que destrói o objeto e limpa ele da memória
			area.QueueFree(); //destrói o laser
			this.QueueFree(); //destrói o inimigo
		}
	}
	
	//função do game over
	private void OnBodyEntered(Node2D body){
		if (body is Player){
			
			Player jogador = (Player)body;
			jogador.ReceberDano();
			
			this.QueueFree();
			}
			
	}
}
