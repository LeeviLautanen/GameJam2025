using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
<<<<<<< HEAD
	public const float BoostMultiplier = 5.0f;
=======
	public const float BoostMultiplier = 2.0f;
	private const float boostTime = 2f;
	private const float cooldownTime = 10f;
>>>>>>> 4b9c330306affb97c5ffc72634cf23352aa1cfba
	bool isBoosted = false;
	bool isOnBoostCooldown = false;

	private TileMapLayer tileMap;
	private const float currentStrength = 2.0f;

	private Timer boostTimer;
	private Timer cooldownTimer;
	private Label cooldownLabel;

	public override void _Ready()
	{
		// Get the TileMap node
		tileMap = GetNode<TileMapLayer>("/root/Map/TheJunk");

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

		cooldownLabel = GetNode<Label>("CooldownLabel");
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
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("boost") && !isBoosted && !isOnBoostCooldown)
		{
			Boost();
<<<<<<< HEAD
			BoostCooldown();
			} 
=======
			BoostCoolDown();
		}
>>>>>>> 4b9c330306affb97c5ffc72634cf23352aa1cfba
		//Movement
		if (isBoosted)
		{
			CurrentSpeed = Speed * BoostMultiplier;
			cooldownLabel.Set("text", "Boost active: " + Mathf.Max(boostTimer.TimeLeft, 0).ToString("F1") + "s");
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
<<<<<<< HEAD
	
	//Starts Boost
	private void Boost() {
		var BoostTimer = GetNode<Timer>("BoostTimer");
		BoostTimer.Timeout += OnBoostTimeout;
		isBoosted = true;
	}
	//Ends Boost
	private void OnBoostTimeout() {
		isBoosted = false;
	}
	
	//Starts BoostCooldown
	private void BoostCooldown() {
		var Cooldown = GetNode<Timer>("BoostCooldown");
		Cooldown.Timeout += BoostCooldownTimeout;
		isOnBoostCooldown = true;
	}
	//Ends BoostCooldown
	private void BoostCooldownTimeout() {
=======

	// When boost is pressed call BoostTimer
	private void Boost()
	{
		boostTimer.Start();
		isBoosted = true;
	}

	//BoostTimer gives Timeout and boost ends
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

	private void OnCoolDownTimeout()
	{
>>>>>>> 4b9c330306affb97c5ffc72634cf23352aa1cfba
		isOnBoostCooldown = false;
	}
}
