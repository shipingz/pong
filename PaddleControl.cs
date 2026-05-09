using Godot;
using System;

public partial class PaddleControl : CharacterBody2D
{

	public float Speed = 400.0f;
	[Export]
	public string UpKey = "ui_up";
	[Export]
	public string DownKey = "ui_down";
	
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
		Vector2 velocity = Vector2.Zero;
		float direction = 0.0f;
		if (Input.IsActionPressed(UpKey)){
			GD.Print("Up key pressed");
			direction -= 1.0f;
			GD.Print("Direction after up key: " + direction);
		}
		if (Input.IsActionPressed(DownKey)){
			GD.Print("Down key pressed");
			direction += 1.0f;
		}
		velocity.Y = direction * Speed;
		Velocity = velocity;
		MoveAndSlide();
	}
}
