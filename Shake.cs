using Godot;
using System;

public partial class Shake : Camera2D
{

private float forcaAtual = 0.0f;
private float taxaDecaimento = 5.0f;
private RandomNumberGenerator gerador = new RandomNumberGenerator();

	public override void _Ready()
	{
		gerador.Randomize();
		
	}

	public override void _Process(double delta)
	{
		if (forcaAtual > 0)
		{
			forcaAtual = Mathf.Lerp(forcaAtual, 0, taxaDecaimento * (float)delta);
			
			float xSorteado = gerador.RandfRange(-forcaAtual, forcaAtual);
			float ySorteado = gerador.RandfRange(-forcaAtual, forcaAtual);
			
			Offset = new Vector2(xSorteado, ySorteado);
			
			if (forcaAtual < 0.05f)
			{
				forcaAtual = 0;
				Offset = Vector2.Zero;
			}
		}
	}
	public void Tremer(float forca){
		forcaAtual = forca;
	}
}
