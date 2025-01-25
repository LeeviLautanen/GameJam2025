using Godot;
using System;

public partial class Player : CharacterBody2D {
	public const float Speed = 300.0f;
	public const float BoostMultiplier = 3.0f;
	bool isBoosted = false;
	bool isOnBoostCooldown = false;
	
	public override void _PhysicsProcess(double delta) {
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("boost") && !isBoosted && !isOnBoostCooldown) {
			Boost();
			BoostCoolDown();
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
	
	// When boost is pressed call BoostTimer
	private void Boost() {
		var BoostTimer = GetNode<Timer>("BoostTimer");
		BoostTimer.Timeout += OnBoostTimeout;
		isBoosted = true;
	}
	//BoostTimer gives Timeout and boost ends
	private void OnBoostTimeout() {
		isBoosted = false;
	}
	
	//Sets BoostCooldown
	private void BoostCoolDown() {
		var CoolDown = GetNode<Timer>("BoostCoolDown");
		CoolDown.Timeout += BoostCoolDownTimeout;
		isOnBoostCooldown = true;
	}
	
	private void BoostCoolDownTimeout() {
		isOnBoostCooldown = false;
	}
	
}
