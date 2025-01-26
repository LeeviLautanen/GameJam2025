using Godot;
using System;

public partial class StartAnimation : Node2D
{
	private AnimatedSprite2D animatedSprite2D;
	private Timer timer;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("start_animation_eng");
		animatedSprite2D.AnimationFinished += OnAnimationFinished;

		timer = new Timer();
		AddChild(timer);
		timer.OneShot = true;
	}

	private void OnAnimationFinished()
	{
		var parent = GetParent() as SceneFade;
		parent.FadeToBlack();

		timer.Connect("timeout", new Callable(this, nameof(AfterFadeOut)));
		timer.WaitTime = 1f; // Fade set to 1 second in SceneFade.cs
		timer.Start();
	}

	private void AfterFadeOut()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

}
