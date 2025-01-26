using Godot;
using System;

public partial class EndAnimation : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animatedSprite2D;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("end_animation"); 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_animated_sprite_2d_animation_finished() {
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/start.tscn");
	}
}
