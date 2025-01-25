using Godot;
using System;

public partial class StartAnimation : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Node2D animationNode;
	private AnimatedSprite2D _animatedSprite2D;
	public override void _Ready()
	{
		animationNode = GetNode<Node2D>("Animation");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Play("start_animation");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
