using Godot;
using System;

public partial class Date : Control
{
	[Export]
	DayNightCycle dayNightCycle;
	Label label;
	Label timeLabel;
	Label currentState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetChild<Label>(0);
		timeLabel = GetChild<Label>(1);
		currentState = GetChild<Label>(2);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var time = dayNightCycle.GetTime();
		string timeString = (time.X / 10 > 0 ? "" : "0") + time.X + ":" + (time.Y / 10 > 0 ? "" : "0") + time.Y;

		label.Text = "Day: " + dayNightCycle.GetDay() + "/1";
		timeLabel.Text = timeString;
		currentState.Text = (dayNightCycle.IsNight() ? "Night" : "Day");
	}
}
