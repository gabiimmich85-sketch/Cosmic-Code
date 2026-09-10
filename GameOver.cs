using Godot;
using System;

public partial class GameOver : Control
{
	private Label textoPlacar;
	private void _on_button_pressed(){
		GetTree().ChangeSceneToFile("res://mundo.tscn");
	}
	
	private void _on_button_tentar_pressed(){
		GetTree().ChangeSceneToFile("res://MenuPrincipal.tscn");
	}
	
	private string apiKey = "dev_505417fb13ca4f43b540b107f4fd8389";
	private string leaderboardId = "36012";
	
	private string tokenSessao = "";
	
	private HttpRequest requisicaoLogin;
	private HttpRequest requisicaoPlacar;
	private HttpRequest requisicaoNome;
	private HttpRequest requisicaoTop10;
	
	private LineEdit campoNome;
	private Button botaoEnviar;
	
	public override void _Ready(){
		
		campoNome = GetNode<LineEdit>("CampoNome");
		botaoEnviar = GetNode<Button>("BotaoEnviar");
		
		botaoEnviar.Pressed += AoClicarEnviar;
		
		requisicaoLogin = new HttpRequest();
		requisicaoLogin.ProcessMode = ProcessModeEnum.Always;
		AddChild(requisicaoLogin);
		requisicaoLogin.RequestCompleted += AoTerminarLogin;
		
		requisicaoNome = new HttpRequest();
		requisicaoNome.ProcessMode = ProcessModeEnum.Always;
		AddChild(requisicaoNome);
		requisicaoNome.RequestCompleted += AoTerminarNome;
		
		requisicaoPlacar = new HttpRequest();
		requisicaoPlacar.ProcessMode = ProcessModeEnum.Always;
		AddChild(requisicaoPlacar);
		requisicaoPlacar.RequestCompleted += AoTerminarEnvio;
		
		requisicaoTop10 = new HttpRequest();
		requisicaoTop10.ProcessMode = ProcessModeEnum.Always;
		AddChild(requisicaoTop10);
		requisicaoTop10.RequestCompleted += AoTerminarTop10;
		
		textoPlacar = GetNode<Label>("TextoPlacar");
		
		textoPlacar.Text = "Carregando placar...";
		
		FazerLoginServidor();
	}
	
	private void FazerLoginServidor(){
		GD.Print("Conectando ao servidor...");
		
		string url = "https://auo8mdsj.api.lootlocker.io/game/v2/session/guest";
		string [] cabecalhos = new string[] {"Content-Type: application/json"};
		
		var dados = new Godot.Collections.Dictionary{
			{"game_api_key", apiKey},
			{"game_version", "1.0.0"},
			{"development_mode", true}
		};
		string json = Json.Stringify(dados);
		
		requisicaoLogin.Request(url, cabecalhos, HttpClient.Method.Post, json);
	}
	private void AoTerminarLogin(long result, long responseCode, string[] headers, byte[] body)
	{
		if(responseCode == 200){
			string respostaTexto = System.Text.Encoding.UTF8.GetString(body);
			var json = new Json();
			json.Parse(respostaTexto);
			var respostaDados = (Godot.Collections.Dictionary)json.Data;
			
			tokenSessao = (string)respostaDados["session_token"];
			GD.Print("Login feito! Aguardando nome do jogador...");
			
			
			}
		}
			private void AoClicarEnviar(){
				string nick = campoNome.Text;
				
				if(string.IsNullOrWhiteSpace(nick)){
					GD.Print("O nome está vazio!");
					return;
				}
				GD.Print("Salvando o nome: " + nick + " no banco de dados...");
				
				string url = "https://auo8mdsj.api.lootlocker.io/game/player/name";
				
				if (string.IsNullOrEmpty(tokenSessao)){
					GD.Print("ERRO:O token de sessão está vazio.");
					return;
				}
				string [] cabecalhos = new string[]{
					"Content-Type: application/json",
					"x-session-token: " + tokenSessao
				};
				
				var dados = new Godot.Collections.Dictionary { { "name", nick } };
				
				Error erroDisparo = requisicaoNome.Request(url, cabecalhos, HttpClient.Method.Patch, Json.Stringify(dados));
			if (erroDisparo != Error.Ok){
				GD.Print("Godot se recusou a disparar a requisicao de nome. Erro: " + erroDisparo);
			}
			}
			
			private void AoTerminarNome(long result, long responseCode, string[] headers, byte[] body)
	{
		if (responseCode == 200)
		{
			GD.Print("Nome salvo com sucesso! Agora sim, disparando os pontos...");
			// Agora que o nome tá atrelado ao nosso ID na nuvem, podemos enviar a pontuação final
			EnviarPontuacao(SistemaSave.PontuacaoAtual);
		}
		else
		{
			GD.Print("Erro ao salvar nome. Código HTTP: " + responseCode);
			GD.Print("Motivo: " + System.Text.Encoding.UTF8.GetString(body));
		}
	}
	
	private void EnviarPontuacao(int pontos){
		GD.Print("Enviando " + pontos + "pontos para a rede...");
		string url = $"https://auo8mdsj.api.lootlocker.io/game/leaderboards/{leaderboardId}/submit";
		
		string[] cabecalhos = new string []{
			"Content-Type: application/json",
			"x-session-token: " + tokenSessao
		};
		var dados = new Godot.Collections.Dictionary{
			{"score", pontos},
			{"member-id", campoNome.Text}
		};
		string json = Json.Stringify(dados);
		requisicaoPlacar.Request(url, cabecalhos, HttpClient.Method.Post, json);
	}
	private void AoTerminarEnvio(long result, long responseCode, string[] headers, byte[] body){
		if (responseCode == 200){
			GD.Print("Sucesso! Pontuação de " + campoNome + "salva!");
			botaoEnviar.Disabled = true; //pra desativar e não flodar o banco
			
			BuscarTop10();
		}
		else{
			
			GD.Print("Erro ao enviar pontos. Código: " + responseCode);
			
		}
	}
	private void BuscarTop10(){
		GD.Print("Buscando os 10 melhores no servidor...");
		
		string url = $"https://auo8mdsj.api.lootlocker.io/game/leaderboards/{leaderboardId}/list?count=10";
		
		string [] cabecalhos = new string[]{
			"Content-Type: application/json",
			"x-session-token: " + tokenSessao
		};
		requisicaoTop10.Request(url, cabecalhos, HttpClient.Method.Get, "");
	}
	
	private void AoTerminarTop10(long result, long responseCode, string [] headers, byte[] body){
		if(responseCode == 200){
			string jsonString = System.Text.Encoding.UTF8.GetString(body);
			var json = new Json();
			json.Parse(jsonString);
			
			var dados = (Godot.Collections.Dictionary)json.Data;
			
			var listaJogadores = (Godot.Collections.Array)dados["items"];
			
			textoPlacar.Text = "--- TOP GLOBAL ---\n";
			
			foreach (var item in listaJogadores){
				var info = (Godot.Collections.Dictionary)item;
				
				int rank = (int)info["rank"];
				int score = (int)info["score"];
				
				var gavetaPlayer = (Godot.Collections.Dictionary)info["player"];
				string nome = (string)gavetaPlayer["name"];
				
				textoPlacar.Text += $"{rank}º | {nome} - {score} pts\n ";
			}
			GD.Print("--------------\n");
		}
		else{
			GD.Print("Erro ao buscar top 10. Código: " + responseCode);
		}
	}
}
