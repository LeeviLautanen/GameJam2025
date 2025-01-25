using Godot;

public partial class SceneFade : CanvasLayer
{
	private ColorRect colorRect;
	private Tween tween;

	public override void _Ready()
	{
		colorRect = GetNode<ColorRect>("ColorRect");
		tween = GetTree().CreateTween();

		colorRect.Color = new Color(0, 0, 0, 1);
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		colorRect.SetSize(screenSize);
		FadeOut();
	}

	public void FadeOut()
	{
		tween.TweenProperty(colorRect, "modulate:a", 0, 1f);
	}

	public void FadeIn()
	{
		tween.TweenProperty(colorRect, "modulate:a", 1f, 0);
	}
}