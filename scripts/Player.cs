using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float BurstMultiplier = 2.0f;
	private const float currentStrength = 2.0f;
	private TileMapLayer tileMap;

	public override void _Ready()
	{
		// Get the TileMap node
		tileMap = GetNode<TileMapLayer>("/root/Map/TheJunk");
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

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


		//When "Spacebar" is pressed double speed
		float CurrentSpeed = Speed;
		if (Input.IsActionPressed("burst"))
		{
			CurrentSpeed *= BurstMultiplier;
		}
		//Making speed consistant when movin diagonally
		Velocity = direction * CurrentSpeed;
		MoveAndSlide();
	}
}