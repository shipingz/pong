using Godot;
using System;

public partial class PaddleAI : Node
{
    Ball ball;
    PaddleControl paddle;
    
    public PaddleAI(Ball ball, PaddleControl paddle)
    {
        this.ball = ball;
        this.paddle = paddle;
    }

    public void update()
    {
        float directionY = Mathf.Sign(ball.Position.Y - paddle.Position.Y);
        this.paddle.SetMovement(directionY);
    }
}
