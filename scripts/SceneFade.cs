using Godot;

public partial class SceneFade : CanvasLayer
{
	private ColorRect colorRect;
	private Tween tween;
	private Timer timer;

	public override void _Ready()
	{
		colorRect = GetNode<ColorRect>("FadeRect");

		colorRect.Color = new Color(0, 0, 0, 1);

		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		colorRect.SetSize(screenSize);

		timer = new Timer();
		AddChild(timer);

		FadeFromBlack();
	}

	public void FadeFromBlack()
	{
		tween = CreateTween();
		tween.TweenProperty(colorRect, "modulate:a", 0f, 1f);

		timer.Connect("timeout", new Callable(this, nameof(AfterFadeIn)));
		timer.WaitTime = 1f;
		timer.OneShot = true;
		timer.Start();
	}

	public void FadeToBlack()
	{
		colorRect.Visible = true;
		Tween tween = CreateTween();
		tween.TweenProperty(colorRect, "modulate:a", 1f, 1f);
	}

	private void AfterFadeIn()
	{
		colorRect.Visible = false;
	}
}
