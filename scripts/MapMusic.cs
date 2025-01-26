using Godot;
using System;

public partial class MapMusic : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var audioMap = GetNode<AudioStreamPlayer>("GameMusic");
		audioMap.Stream = GD.Load<AudioStream>("res://audio/Sakura-Girl-Daisy-chosic.com_.mp3");
  		audioMap.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
