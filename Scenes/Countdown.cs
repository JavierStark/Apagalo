using Godot;
using System;

public partial class Countdown : Label
{
	int seconds = 3;
	public override void _Ready()
	{
		GetChild<Timer>(0).Timeout += () => seconds--;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (seconds == 0)
		{
			Text = "Go!";
			return;
		}
		if (seconds < 0)
		{
			GoToGame();
		}
		Text = seconds.ToString();
	}

	private void GoToGame()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Map.tscn");
	}
}
