using Godot;

public partial class StartButton : Button
{
	Timer timer;
	public override void _Ready()
	{
		Connect("pressed", new Callable(this, nameof(OnStartButtonPressed)));

		timer = new Timer();
		AddChild(timer);
	}

	private void OnStartButtonPressed()
	{
		var parent = GetParent() as SceneFade;
		parent.FadeToBlack();

		timer.Connect("timeout", new Callable(this, nameof(AfterFadeOut)));
		timer.WaitTime = 0.5f; // Fade set to 1 second in SceneFade.cs
		timer.Start();
	}

	private void AfterFadeOut()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
