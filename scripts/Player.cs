using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float BurstMultiplier = 2.0f;
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