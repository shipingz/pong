using Godot;
using System;

public partial class Ball : CharacterBody2D
{

	public Godot.Vector2 initPosition = new Godot.Vector2(600, 350);
	public Godot.Vector2 initVelocity = new Godot.Vector2(400.0f, 300.0f);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity = initVelocity;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void HitPaddle()
	{
		Velocity = new Godot.Vector2(-Velocity.X, Velocity.Y);
	}

	public void HitWall()
	{
		Velocity = new Godot.Vector2(Velocity.X, -Velocity.Y);
	}

	public void ResetBall(Godot.Vector2 position, Godot.Vector2 speed)
	{
		Position = position;
		Velocity = speed;
	}	

	public void ResetBallWithDirection(float directionX)
	{
		ResetBall(initPosition, initVelocity * new Godot.Vector2(directionX, 1.0f));
	}

	public void ResetBall()
	{
		ResetBall(initPosition, initVelocity);
	}

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(Velocity * (float)delta);
		if (collision != null)
		{
			Node collider = (Node)collision.GetCollider();
			if (collider.IsInGroup("wall_up") || collider.IsInGroup("wall_down")){
				HitWall();
			}
			else if (collider.IsInGroup("paddle_left") || collider.IsInGroup("paddle_right")){
				HitPaddle();
			}
		}
	}
}