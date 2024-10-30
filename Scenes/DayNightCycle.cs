using Godot;
using System;

public partial class DayNightCycle : CanvasModulate
{

	const float MINUTES_PER_DAY = 60 * 24;

	[Export]
	float INITIAL_HOUR = 12;
	[Export]
	float NUMBER_DAYS = 5;

	[Export]
	float DAY_NIGHT_MINUTES = 0.5f;
	float TIME_SCALER;

	[Export]
	GradientTexture1D gradient;

	[Export]
	string gameOverScene;


	[Export]
	AudioStreamPlayer audio;
	bool audioPlayed = false;

	float time = 0;
	int day = 0;

	public override void _Ready()
	{
		TIME_SCALER = 2 * Mathf.Pi / (DAY_NIGHT_MINUTES * 60);
		time = INITIAL_HOUR / 24 * DAY_NIGHT_MINUTES * 60;
	}

	public override void _Process(double delta)
	{
		time += (float)delta;
		day = (int)((time - INITIAL_HOUR / 24 * DAY_NIGHT_MINUTES * 60) / (DAY_NIGHT_MINUTES * 60) + 1);

		if (day > NUMBER_DAYS)
		{
			GetTree().ChangeSceneToFile(gameOverScene);
		}

		var dayNightValue = (Mathf.Sin(time * TIME_SCALER - Mathf.Pi / 2.0) + 1.0) / 2.0;
		Color = gradient.Gradient.Sample((float)dayNightValue);
	}

	public Vector2I GetTime()
	{
		float totalHours = (time / (DAY_NIGHT_MINUTES * 60) * 24);
		float totalMinutes = totalHours * 60;

		int hours = (int)(totalHours % 24);
		int minutes = (int)(totalMinutes % 60);

		string dayMoment = hours + ":" + minutes;
		return new Vector2I(hours, minutes);
	}

	public bool IsNight()
	{
		int hour = GetTime().X;
		bool isNight = hour < 8 || hour > 19;

		return isNight;
	}

	public int GetDay()
	{
		return day;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey key)
		{
			if (!key.Echo && key.Pressed && key.Keycode == Key.R)
			{
				GetTree().ChangeSceneToFile(gameOverScene);
			}
		}
	}
}
