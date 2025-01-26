using Godot;
using System;

public partial class StartAnimation : Node2D
{
	private AnimatedSprite2D animatedSprite2D;
	private Timer timer;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("start_animation");
		animatedSprite2D.AnimationFinished += OnAnimationFinished;

		timer = new Timer();
		AddChild(timer);
		timer.OneShot = true;
	}

	private void OnAnimationFinished()
	{
		var parent = GetNode<CanvasLayer>("../FadeLayer") as SceneFade;
		parent.FadeToBlack();

		timer.Connect("timeout", new Callable(this, nameof(AfterFadeOut)));
		timer.WaitTime = 1f; // Fade set to 1 second in SceneFade.cs
		timer.Start();
	}

	private void AfterFadeOut()
	{
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/game.tscn");
	}

}
