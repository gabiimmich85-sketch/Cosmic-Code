using Godot;
using System;

public partial class GeradorInimigos : Node2D
{
	[Export]
	public PackedScene CenaInimigo;
	
	[Export]
	public PackedScene CenaNaveInimiga;
	
	// RandomNumberGenerator = Faz gerar números aleatórios
	private RandomNumberGenerator geradorAleatorio = new RandomNumberGenerator();
	
	public override void _Ready()
	{
		//vai fazer o timer rodar a função Gerar quando o tempo acabar
		Timer relogio = GetNode<Timer>("Timer");
		relogio.Timeout += Gerar;
	}
	private void Gerar()
	{
		Node2D novoInimigo;
		
		float chance = geradorAleatorio.Randf();
		
		if (chance <= 0.70f){
			novoInimigo = CenaInimigo.Instantiate<Node2D>();
		}
		else {
			novoInimigo = CenaNaveInimiga.Instantiate<Node2D>();
		}
		
		float xSorteado = geradorAleatorio.RandfRange(50.0f, 1100.0f);
		novoInimigo.GlobalPosition = new Vector2(xSorteado, -50.0f);
		
		AddChild(novoInimigo);
	}
}
