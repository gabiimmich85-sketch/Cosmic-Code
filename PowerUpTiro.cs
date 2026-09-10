using Godot;
using System;

public partial class PowerUpTiro : Area2D
{
	[Export]
	public float Velocidade = 80.0f;
	
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += new Vector2(0,1) * Velocidade * (float)delta;
	}
	
	private void OnBodyEntered(Node2D body){
		if (body is Player jogador){
			jogador.AtivarTiroRapido();
			
			QueueFree();
		}
	}
}
