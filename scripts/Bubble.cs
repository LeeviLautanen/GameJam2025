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
		if (body is Player player)
		{
			player.AddAir(5); // Add 5 air to the player
			QueueFree(); // Destroy the bubble
		}
	}
}
