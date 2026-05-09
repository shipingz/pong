using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class GameController : Node2D
{
	public int leftScore = 0;
	public int rightScore = 0;
	public int maxScore = 5;
	public GameState gameState;

	public PaddleAI paddleAI;
	[Export]
	public Label ScoreLabel;

	[Export]
	public Panel StartScreenContainer;
	[Export]
	public VBoxContainer PlayerNumberSelectionContainer;
	[Export]
	public Button SinglePlayerButton;
	[Export]
	public Button DoublePlayerButton;
	[Export]
	public VBoxContainer AISelectorContainer;
	[Export]	
	public Button LeftAsPlayer;
	[Export]
	public Button RightAsPlayer;

	[Export]
	public PaddleControl leftPaddle;
	[Export]
	public PaddleControl rightPaddle;
	[Export]
	public Ball ball;
	[Export]
	public Label ScoreBoard;
	[Export]
	public Node2D GameWorld;
	[Export]
	public Label GameOverMsg;
	[Export]
	public Button RestartGameButton;
	[Export]
	public Panel EndScreenContainer;	
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
	public enum GameMode
	{
		SinglePlayer,
		DoublePlayer
	}

	public GameMode gameMode;
	public Player AISide;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		GameWorld.Visible = false;
		GetTree().Paused = true;

        gameState = GameState.StartingScreen;
		ball.ResetBall();

		Area2D leftGoal = GetNode<Area2D>("GameWorld/LeftGoal");
		Area2D rightGoal = GetNode<Area2D>("GameWorld/RightGoal");

		leftGoal.BodyEntered += body => ScorePoint(body, Player.Right);
		rightGoal.BodyEntered += body => ScorePoint(body, Player.Left);

		// // Set up AI for the right paddle
		// PaddleControl rightPaddle = GetNode<PaddleControl>("RightPaddle");
		// paddleAI = new PaddleAI(ball, rightPaddle);
		// rightPaddle.IsAIControlled = true;

		SinglePlayerButton.Pressed += OnSinglePlayerButtonPressed;
		DoublePlayerButton.Pressed += OnDoublePlayerButtonPressed;
		LeftAsPlayer.Pressed += () => ChosePlayerSide(Player.Left);
		RightAsPlayer.Pressed += () => ChosePlayerSide(Player.Right);
		RestartGameButton.Pressed += onRestartGameButtonPressed;
	}

	public void StartGame()
	{
		leftScore = 0;
		rightScore = 0;
		ScoreLabel.Text = $"{leftScore} : {rightScore}";
		gameState = GameState.Playing;
		ScoreBoard.Visible = true;
		GameWorld.Visible = true;
		EndScreenContainer.Visible = false;
		StartScreenContainer.Visible = false;
		GetTree().Paused = false;
	}

	public void OnSinglePlayerButtonPressed()
	{
		PlayerNumberSelectionContainer.Visible = false;
		AISelectorContainer.Visible = true;
		gameMode = GameMode.SinglePlayer;
	}

	public void OnDoublePlayerButtonPressed()
	{
		StartScreenContainer.Visible = false;
		gameMode = GameMode.DoublePlayer;
		StartGame();
	}

	public void ChosePlayerSide(Player side)
	{
		if(side == Player.Left)
		{
			AISide = Player.Right;
			rightPaddle.SetAI(true);
			paddleAI = new PaddleAI(ball, rightPaddle);
		}
		else
		{
			AISide = Player.Left;
			leftPaddle.SetAI(true);
			paddleAI = new PaddleAI(ball, leftPaddle);
		}
		StartScreenContainer.Visible = false;
		StartGame();
	}

	public void onRestartGameButtonPressed()
	{
		ResetGame();
		StartGame();
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
			if (gameMode == GameMode.SinglePlayer)
			{
				paddleAI.update();
				GD.Print("AI updated paddle movement");
			}
		}
	}

	public void ScorePoint(Node body,Player player)
	{
		
		if (body is not Ball){
			return;
		}
		if (player == Player.Left)
		{
			leftScore++;
			GD.Print("Left player scored! Score: " + leftScore);
			ScoreLabel.Text = $"{leftScore} : {rightScore}";
		}
		else
		{
			rightScore++;
			GD.Print("Right player scored! Score: " + rightScore);
			ScoreLabel.Text = $"{leftScore} : {rightScore}";
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
			GameWorld.Visible = false;
			EndScreenContainer.Visible = true;
			GameOverMsg.Text = "Left Player Wins!";
		}
		else if (rightScore >= maxScore)
		{
			gameState = GameState.GameOver;
			GD.Print("Right player wins!");
			GetTree().Paused = true;
			GameWorld.Visible = false;
			EndScreenContainer.Visible = true;
			GameOverMsg.Text = "Right Player Wins!";
		}
	}

	public void ResetGame()
	{
		leftScore = 0;
		rightScore = 0;
		ball.ResetBall();
	}



}