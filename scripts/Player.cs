using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float BurstMultiplier = 2.0f;

	public override void _PhysicsProcess(double delta) {
		//Get direction
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("burst")) {
		CurrentSpeed *= BurstMultiplier;
		}
		//Movemement
		Velocity = direction * CurrentSpeed;
		MoveAndSlide();
	}
}
