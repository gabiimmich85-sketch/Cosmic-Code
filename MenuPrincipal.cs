using Godot;
using System;

public partial class MenuPrincipal : Control
{
	
	public override void _Ready(){
		Label textoRecorde = GetNode<Label>("TextoRecorde");
		
		int maiorPontuacao = SistemaSave.CarregarRecorde();
		
		textoRecorde.Text = "Recorde Atual: " + maiorPontuacao;
	}
	private void _on_button_pressed(){
		GetTree().ChangeSceneToFile("res://mundo.tscn");
	}
}
