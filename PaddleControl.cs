using Godot;
using System;

public partial class PaddleControl : CharacterBody2D
{

	public float Speed = 400.0f;
	[Export]
	public string UpKey = "ui_up";
	[Export]
	public string DownKey = "ui_down";

	[Export]
	public bool IsAIControlled = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsAIControlled)
		{
			MoveAndSlide();
			return;
		}
		float direction = 0.0f;
		if (Input.IsActionPressed(UpKey)){
			direction -= 1.0f;
		}
		if (Input.IsActionPressed(DownKey)){
			direction += 1.0f;
		}
		Vector2 velocity = new Vector2(0, direction * Speed);
		Velocity = velocity;
		MoveAndSlide();
	}

	public void SetMovement(float direction)
	{
		Vector2 velocity = Vector2.Zero;
		velocity.Y = direction * Speed;
		Velocity = velocity;
	}

	public void SetAI(bool isAI)
	{
		IsAIControlled = isAI;
	}
}