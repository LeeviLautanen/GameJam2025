using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float BurstMultiplier = 2.0f;

	public override void _PhysicsProcess(double delta) {
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("burst")) {
		CurrentSpeed *= BurstMultiplier;
		}
		//Making speed consistant when movin diagonally
		Velocity = direction * CurrentSpeed;
		MoveAndSlide();
	}
}
