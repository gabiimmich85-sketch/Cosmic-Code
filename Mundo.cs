using Godot;
using System;

public partial class Mundo : Node2D
{
	public int Vidas = 3;
	[Export]
	public Label UiTextoVida {get; set;}
	private int pontuacao = 0;
	private Label placarTexto;
	
	
	private Timer relogioGerador;
	
	public override void _Ready(){
		placarTexto = GetNode<Label>("Placar/Label");
		
		relogioGerador = GetNode<Timer>("GeradorInimigos/Timer");
		
		if(UiTextoVida != null){
		UiTextoVida.Text = "Vidas: " + Vidas;
		}
	}
	
	public void AdicionarPontos(){
		SistemaSave.PontuacaoAtual += 10;
		placarTexto.Text = "Pontos: " + SistemaSave.PontuacaoAtual;
		
		if (SistemaSave.PontuacaoAtual >= 450)
	{
		relogioGerador.WaitTime = 0.5;
	}
	else if (SistemaSave.PontuacaoAtual >= 300)
	{
		relogioGerador.WaitTime = 0.8;
	}
	else if (SistemaSave.PontuacaoAtual >= 200)
	{
		relogioGerador.WaitTime = 1.0;
	}
	else if (SistemaSave.PontuacaoAtual >= 100)
	{
		relogioGerador.WaitTime = 1.5;
	}
	}
	
	public void PerderVida(){
		Vidas -= 1;
		
		Shake camera = GetViewport().GetCamera2D() as Shake;
		if (camera != null){
			camera.Tremer(20.0f);
		}
		if (UiTextoVida != null) 
		{
			UiTextoVida.Text = "Vidas: " + Vidas;
			}
		if (Vidas <= 0){
			if (SistemaSave.PontuacaoAtual > SistemaSave.CarregarRecorde()){
				SistemaSave.SalvarRecorde(SistemaSave.PontuacaoAtual);
			}
			GetTree().ChangeSceneToFile("res://GameOver.tscn");
		}
	}
	public void GanharVida(){
		if (Vidas < 3){
			Vidas += 1;
			UiTextoVida.Text = "Vidas: " + Vidas;
		}
	}
	
}
