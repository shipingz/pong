using Godot;
using System;

public partial class GameController : Node2D
{
	public int leftScore = 0;
	public int rightScore = 0;
	public int maxScore = 5;
	public GameState gameState;

	public PaddleAI paddleAI;
	public enum GameState
	{
		StartingScreen,
		Playing,
		GameOver
	}
	public enum Player
	{
		Left,
		Right
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        gameState = GameState.Playing;
		Ball ball = GetNode<Ball>("Ball");
		ball.ResetBall();

		Area2D leftGoal = GetNode<Area2D>("LeftGoal");
		Area2D rightGoal = GetNode<Area2D>("RightGoal");

		leftGoal.BodyEntered += body => ScorePoint(body, Player.Right);
		rightGoal.BodyEntered += body => ScorePoint(body, Player.Left);

		// Set up AI for the right paddle
		PaddleControl rightPaddle = GetNode<PaddleControl>("RightPaddle");
		paddleAI = new PaddleAI(ball, rightPaddle);
		rightPaddle.IsAIControlled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		GD.Print("PhysicsProcess");
		GD.Print("Game state: " + gameState);
		if (gameState == GameState.Playing)
		{
			paddleAI.update();
			GD.Print("AI updated paddle movement");
		}
	}

	public void ScorePoint(Node body,Player player)
	{
		
		if (body is not Ball){
			return;
		}
		Ball ball = GetNode<Ball>("Ball");
		if (player == Player.Left)
		{
			leftScore++;
			GD.Print("Left player scored! Score: " + leftScore);
		}
		else
		{
			rightScore++;
			GD.Print("Right player scored! Score: " + rightScore);
		}
		ball.ResetBallWithDirection(player == Player.Left ? -1 : 1);
		CheckGameOver();
	}

	public void CheckGameOver()
	{
		if (leftScore >= maxScore)
		{
			gameState = GameState.GameOver;
			GD.Print("Left player wins!");
			GetTree().Paused = true;
		}
		else if (rightScore >= maxScore)
		{
			gameState = GameState.GameOver;
			GD.Print("Right player wins!");
			GetTree().Paused = true;
		}
	}



}