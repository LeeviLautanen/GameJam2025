using Godot;
using System;

public partial class Bubble : Node
{
	private Area2D bodyArea;
	public override void _Ready()
	{
		bodyArea = GetNode<Area2D>("BodyArea");
		bodyArea.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
		var audioAirBubble = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		audioAirBubble.Stream = GD.Load<AudioStream>("res://audio/mixkit-liquid-bubble-3000.wav");
  		audioAirBubble.Play();
		
		if (body is Player player)
		{
			player.AddAir(5); // Add 5 air to the player
			
			GetTree().CreateTimer(0.15).Timeout += () =>
		{
			QueueFree();
		
		};
		}
	}
}
