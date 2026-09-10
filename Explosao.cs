using Godot;
using System;

public partial class Explosao : CpuParticles2D
{
	private void _on_finished(){
		this.QueueFree();
	}
}
