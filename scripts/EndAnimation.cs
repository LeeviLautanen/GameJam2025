using Godot;
using System;

public partial class EndAnimation : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animatedSprite2D;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("end_animation_eng");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
