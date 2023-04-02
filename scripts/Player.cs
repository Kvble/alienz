using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Export]
	private float FRICTION = 800;
    [Export]
    private int ACCELERATION = 5;
    [Export]
    private int MAXSPEED = 500;

	public string Id { get; set; }
	private Vector2 _syncPosition;
    private Vector2 _direction;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Debug.WriteLine($"Player {Name} created.");
        
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (IsLocalAuthority())
		{
			Position = _syncPosition;
			return;
		}
        LookAt(GetGlobalMousePosition());
        _direction = GetInputAxis();
		
		if (_direction != Vector2.Zero)
		{
			ApplyAcceleration();
		}
		else
		{
			ApplyFriction((float)delta);
		}
		MoveAndSlide();

		//RpcId(1, "PushToServer", Position);
    }

	private static Vector2 GetInputAxis()
	{
		var inputDirection = Vector2.Zero;
		inputDirection.X = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        inputDirection.Y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
		return inputDirection.Normalized();
    }

	private void ApplyFriction(float delta)
	{
		Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
    }

	private void ApplyAcceleration()
	{
        Velocity = Velocity.MoveToward(_direction * MAXSPEED, ACCELERATION);
    }

	private bool IsLocalAuthority()
	{
		return Id == GetTree().GetMultiplayer().GetUniqueId().ToString();
	}

	private void PushToServer(Vector2 position)
	{
		if (!GetTree().GetMultiplayer().IsServer()) 
		{
			return;
		}
		if (Id != GetTree().GetMultiplayer().GetRemoteSenderId().ToString()) 
		{
			return;
		}
		_syncPosition = position;
	}
}
