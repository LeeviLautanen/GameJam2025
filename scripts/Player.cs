using Godot;
using System;

public partial class Player : CharacterBody2D {
	public const float Speed = 300.0f;
	public const float BoostMultiplier = 5.0f;
	bool isBoosted = false;
	bool isOnBoostCooldown = false;
	
	public override void _PhysicsProcess(double delta) {
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("boost") && !isBoosted && !isOnBoostCooldown) {
			Boost();
			BoostCooldown();
			} 
		//Movement
		if (isBoosted) {
			CurrentSpeed = Speed * BoostMultiplier;
		} else {
			CurrentSpeed = Speed;
		}
		Velocity = direction * CurrentSpeed;
		MoveAndSlide();
	}
	
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
		isOnBoostCooldown = false;
	}
	
}
