using Godot;

public partial class Norppa : CharacterBody2D // Use KinematicBody2D or similar if needed
{
    [Export] public float Speed = 100f;
    [Export] public Vector2 Direction = new Vector2(1, 0); // Moving right by default

    private Area2D visionArea;
    private CharacterBody2D target;

    public override void _Ready()
    {
        visionArea = GetNode<Area2D>("VisionArea");
        visionArea.BodyEntered += PlayerInView;
        visionArea.BodyExited += PlayerLeftView;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (target != null)
        {
            GD.Print("Target not null");
            MoveTowardsTarget(target);
        }

        MoveAndSlide();
    }

    private void MoveTowardsTarget(CharacterBody2D target)
    {
        Vector2 direction = (target.GlobalPosition - GlobalPosition).Normalized();
        Rotation = direction.Angle();
        Direction = direction;
        GD.Print(Direction);
        Velocity = Direction.Normalized() * Speed;
    }

    private void PlayerInView(Node body)
    {
        if (body is CharacterBody2D b)
        {
            target = b;
        }
        GD.Print("PLayer in view");
    }

    private void PlayerLeftView(Node body)
    {
        target = null;
        Velocity = Vector2.Zero;
        GD.Print("PLayer left view");
    }
}
