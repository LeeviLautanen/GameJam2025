using Godot;
using System;

public partial class Exit : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("pressed", new Callable(this, nameof(OnStartButtonPressed)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnStartButtonPressed() {
		GD.Print("pressed");
		GetTree().Quit();
	}
}
