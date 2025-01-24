using System;
using Godot;

public partial class Norppa : CharacterBody2D
{
    [Export] public float Speed = 100f;

    private Area2D visionArea;
    private CharacterBody2D target;
    private Random random = new Random();
    private bool wallVisible = false;

    public override void _Ready()
    {
        visionArea = GetNode<Area2D>("VisionArea");
        visionArea.BodyEntered += OnBodyEntered;
        visionArea.BodyExited += OnBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (target != null)
        {
            MoveTowardsTarget(target);
        }

        if (wallVisible)
        {
            Rotation = (float)(Rotation - Mathf.Pi + random.NextDouble() * (Mathf.Pi / 2) - (Mathf.Pi / 4));
        }

        Vector2 direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
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
        if (body is CharacterBody2D b && b.Name == "Player")
        {
            target = b;
            wallVisible = false;
        }
        else if (body is StaticBody2D && target == null)
        {
            wallVisible = true;
        }

        GD.Print(body.Name);
    }

    private void OnBodyExited(Node body)
    {
        if (body is CharacterBody2D)
        {
            target = null;
        }
        else if (body is StaticBody2D)
        {
            wallVisible = false;
        }
    }
}
