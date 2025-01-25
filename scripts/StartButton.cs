using Godot;

public partial class StartButton : Button
{
	public override void _Ready()
	{
		Connect("pressed", new Callable(this, nameof(OnStartButtonPressed)));
	}

	private void OnStartButtonPressed()
	{
		GD.Print(GetParent().Name);
		GetTree().ChangeSceneToFile("res://scenes/norppa.tscn");
	}
}
