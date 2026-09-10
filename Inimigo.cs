using Godot;
using System;

public partial class Inimigo : Area2D
{
	[Export]
	public PackedScene CenaDeExplosao {get; set;}
	[Export]
	public PackedScene CenaVida;
	private RandomNumberGenerator geradorAleatorio = new RandomNumberGenerator();
	
	public float Velocidade = 200.0f;
	public override void _Ready()
	{
		GD.Print("O CÓDIGO NOVO COMPILOU!");
		//Diz quando rodar a função OnAreaEntered.
		AreaEntered += OnAreaEntered;
		GD.Print("Coração nasceu na posição: " + GlobalPosition);
		BodyEntered += OnBodyEntered;
		
		
	
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(0, Velocidade *(float)delta);
	}
	
	//função que roda na PAULADA
	private void OnAreaEntered(Area2D area){
		//verifica se a área é da classe "tiro"
		if(area is Tiro) {
			if (GlobalPosition.Y < 0) 
		{
			return;
		}
			
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
		
		if (geradorAleatorio.Randf() <= 0.05f){
			Node2D item = CenaVida.Instantiate<Node2D>();
			item.GlobalPosition = this.GlobalPosition;
			GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, item);
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
