using System;
using Godot;

public partial class Norppa : CharacterBody2D
{
	[Export] public float Speed = 100f;

	private Area2D visionArea;
	private Area2D bodyArea;
	private CharacterBody2D target;
	private Random random = new Random();
	private bool wallHit = false;

	public override void _Ready()
	{
		visionArea = GetNode<Area2D>("VisionArea");
		visionArea.BodyEntered += OnVisionEntered;
		visionArea.BodyExited += OnVisionExited;

		visionArea = GetNode<Area2D>("BodyArea");
		visionArea.BodyEntered += OnBodyEntered;
		visionArea.BodyExited += OnBodyExited;


	}

	public override void _PhysicsProcess(double delta)
	{
		if (target != null)
		{
			MoveTowardsTarget(target);
		}

		// If a wall is hit, do a 180
		if (wallHit)
		{
			Rotation = (float)(Rotation - Mathf.Pi + random.NextDouble() * (Mathf.Pi / 2) - (Mathf.Pi / 4));
		}

		// Set direction and velocity
		Vector2 direction = new(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private void MoveTowardsTarget(CharacterBody2D target)
	{
		Vector2 direction = (target.GlobalPosition - GlobalPosition).Normalized();
		Rotation = direction.Angle();
	}

	private void OnBodyEntered(Node body)
	{
		wallHit = true;
	}

	private void OnBodyExited(Node body)
	{
		wallHit = false;
	}

	private void OnVisionEntered(Node body)
	{
		if (body is CharacterBody2D b && b.Name == "Player")
		{
			target = b;
		}
	}

	private void OnVisionExited(Node body)
	{
		if (body is CharacterBody2D)
		{
			target = null;
			// Do a 180 if player escapes
			Rotation = (float)(Rotation - Mathf.Pi + random.NextDouble() * (Mathf.Pi / 2) - (Mathf.Pi / 4));
		}
	}
}
