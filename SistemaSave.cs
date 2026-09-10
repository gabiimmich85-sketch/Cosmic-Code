using Godot;
using System;

public static class SistemaSave
{
	public static int PontuacaoAtual = 0;
	
	private static string caminhoSave = "user://recorde.save";
	
	public static void SalvarRecorde(int pontos){
		using var arquivo = FileAccess.Open(caminhoSave, FileAccess.ModeFlags.Write);
		if(arquivo != null){
			arquivo.Store32((uint)pontos);
		}
		
	}
	
	public static int CarregarRecorde(){
		if (FileAccess.FileExists(caminhoSave)){
			using var arquivo = FileAccess.Open(caminhoSave, FileAccess.ModeFlags.Read);
			return (int)arquivo.Get32();
		}
		return 0;
	}
}
