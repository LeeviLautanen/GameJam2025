using Godot;
using System;

public partial class WinArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	
	private CharacterBody2D player;
	
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
	}
	
	 private void _on_body_entered(Node2D body) {
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/end_animation.tscn");
	}
}
