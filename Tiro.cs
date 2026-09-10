using Godot;
using System;

public partial class Tiro : Area2D
{
	public float Velocidade = 600.0f;
	
	

	
	public override void _Process(double delta)
	{
		Position -= new Vector2(0, Velocidade *(float)delta);
	
	}
}
