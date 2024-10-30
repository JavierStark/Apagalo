using Godot;
using System;
using System.Globalization;

public partial class NPC : TextureRect
{

	[Export]
	float moveMin = 4, moveMax = 7;
	[Export]
	float actionMin = 10, actionMax = 15;

	[Export]
	bool isNightMonster = false;
	bool isSleeping = false;

	Room room;
	DayNightCycle dayNightCycle;
	Timer moveTimer, actionTimer;
	AudioStreamPlayer audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		moveTimer = GetChild<Timer>(0);
		actionTimer = GetChild<Timer>(1);
		audio = GetChild<AudioStreamPlayer>(2);
		moveTimer.Timeout += () => OnTimeout(0);
		actionTimer.Timeout += () => OnTimeout(1);
		dayNightCycle = GetNode<DayNightCycle>("/root/Main/DayNightCycle");

		CheckTime();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckTime();
	}

	public void SetRoom(Room newRoom)
	{
		room = newRoom;
		if (isNightMonster && dayNightCycle.IsNight()) room.TurnLightOn();
	}

	private void OnTimeout(int timerIndex)
	{
		if (isSleeping)
		{
			moveTimer.WaitTime = GD.RandRange(moveMin, moveMax);
			moveTimer.Start();
			actionTimer.WaitTime = GD.RandRange(actionMin, actionMax);
			actionTimer.Start();
			return;
		};
		switch (timerIndex)
		{
			case 0:
				{
					Move();
					return;
				}
			case 1:
				{
					Action();
					return;
				}
		}

	}

	private void Move()
	{
		moveTimer.WaitTime = GD.RandRange(moveMin, moveMax);
		moveTimer.Start();
		room.MoveNPC(this, isNightMonster);
		audio.Play();
	}

	private void Action()
	{
		actionTimer.WaitTime = GD.RandRange(actionMin, actionMax);
		actionTimer.Start();

		room.TurnOnDevice();
	}

	private void CheckTime()
	{
		if (dayNightCycle.IsNight())
		{
			isSleeping = !isNightMonster;
		}
		else
		{
			isSleeping = isNightMonster;
		}

		Visible = !isSleeping;
	}

	public bool IsNightMonster()
	{
		return isNightMonster;
	}

	public bool IsSleeping()
	{
		return isSleeping;
	}
}
