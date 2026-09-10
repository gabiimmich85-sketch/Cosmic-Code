using Godot;
using System;

public partial class FundoEstrelado : ParallaxBackground
{
	[Export]
	public float Velocidade = 150f;
	
	public override void _Process(double delta)
	{
		Vector2 novaPosicao = ScrollBaseOffset;
		
		novaPosicao.Y += Velocidade * (float)delta;
		
		ScrollBaseOffset = novaPosicao;
	}
}
