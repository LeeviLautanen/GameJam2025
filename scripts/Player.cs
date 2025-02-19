using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float BoostMultiplier = 1.5f;
	private const float boostTime = 0.33f;
	private const float cooldownTime = 7f;
	bool isBoosted = false;
	bool isOnBoostCooldown = false;
	private Timer boostTimer;
	private Timer cooldownTimer;
	private Label cooldownLabel;

	private TileMapLayer tileMap;
	private const float currentStrength = 6.0f;

	private ProgressBar airBar;
	private Timer airBarTimer;
	private const float maxAir = 10f;

	public override void _Ready()
	{
		tileMap = GetNode<TileMapLayer>("/root/Map/TheJunk");
		airBar = GetNode<ProgressBar>("/root/Map/UI/AirBar");
		airBarTimer = new Timer();
		AddChild(airBarTimer);
		airBarTimer.WaitTime = 1f;
		airBarTimer.Start();
		airBarTimer.Connect("timeout", new Callable(this, "OnAirBarTimeout"));

		boostTimer = new Timer();
		AddChild(boostTimer);
		boostTimer.OneShot = true;
		boostTimer.WaitTime = boostTime;
		boostTimer.Connect("timeout", new Callable(this, "OnBoostTimeout"));

		cooldownTimer = new Timer();
		AddChild(cooldownTimer);
		cooldownTimer.OneShot = true;
		cooldownTimer.WaitTime = cooldownTime;
		cooldownTimer.Connect("timeout", new Callable(this, "OnCoolDownTimeout"));

		cooldownLabel = GetNode<Label>("/root/Map/UI/CooldownLabel");
		cooldownLabel.Set("text", "Boost Ready");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Get the tile we are on
		Vector2I tileCoordinate = tileMap.LocalToMap(GlobalPosition);
		Vector2I atlasIndex = tileMap.GetCellAtlasCoords(tileCoordinate);

		switch (atlasIndex)
		{
			// Left tile
			case (0, 0):
				Position = Position + new Vector2(-1, 0) * currentStrength;
				break;

			// Right tile
			case (1, 0):
				Position = Position + new Vector2(1, 0) * currentStrength;
				break;

			// Down tile
			case (2, 0):
				Position = Position + new Vector2(0, 1) * currentStrength;
				break;

			// Up tile
			case (3, 0):
				Position = Position + new Vector2(0, -1) * currentStrength;
				break;
			default:
				break;
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		//Sprite rotation
		if (Input.IsActionPressed("ui_right"))
		{
			RotationDegrees = 90;
		}
		else if (Input.IsActionPressed("ui_left"))
		{
			RotationDegrees = 270;
		}
		else if (Input.IsActionPressed("ui_down"))
		{
			RotationDegrees = 180; 
		}
		else if (Input.IsActionPressed("ui_up"))
		{
			RotationDegrees = 0;
		}
		
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("boost") && !isBoosted && !isOnBoostCooldown)
		{
			Boost();
			BoostCoolDown();
		}
		//Movement
		if (isBoosted)
		{
			CurrentSpeed = Speed * BoostMultiplier;
		}
		else
		{
			CurrentSpeed = Speed;
		}
		
		if (isBoosted)
		{
			CurrentSpeed = Speed * BoostMultiplier;
			cooldownLabel.Set("text", "Boost active: " + Mathf.Max(boostTimer.TimeLeft, 0).ToString("F1") + "s");
		}
		else if (isOnBoostCooldown)
		{
			cooldownLabel.Set("text", "Boost on cooldown: " + Mathf.Max(cooldownTimer.TimeLeft, 0).ToString("F1") + "s");
		}
		else
		{
			cooldownLabel.Set("text", "Boost ready!");
		}

		Velocity = direction * CurrentSpeed;
		MoveAndSlide();
	}

	public void ReduceAir(float amount)
	{
		airBar.Value = Math.Clamp(airBar.Value - amount, 0, maxAir);
		if (airBar.Value == 0)
		{
			// Has to be deferred to prevent errors
			GetTree().CallDeferred("change_scene_to_file", "res://scenes/death_animation.tscn");

			GetTree().ChangeSceneToFile("res://scenes/death.tscn");
		}
	}

	public void AddAir(float amount)
	{
		airBar.Value = Math.Clamp(airBar.Value + amount, 0, maxAir);
	}

	private void OnAirBarTimeout()
	{
		ReduceAir(1);
	}

	//Boost 
	private void Boost()
	{
		boostTimer.Start();
		isBoosted = true;
	}

	//Boost ends
	private void OnBoostTimeout()
	{
		isBoosted = false;
	}

	//Sets BoostCooldown
	private void BoostCoolDown()
	{
		cooldownTimer.Start();
		isOnBoostCooldown = true;
	}
	//BoostCooldown ends
	private void OnCoolDownTimeout()
	{
		isOnBoostCooldown = false;
	}
}
