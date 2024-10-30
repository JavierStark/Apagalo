using Godot;
using System;

public partial class GameOver : Control
{

	[Export]
	Label sostenibilityLabel, satisfactionLabel;

	public override void _Ready()
	{
		sostenibilityLabel.Text = GetStars(CalculateSostenibility());
		satisfactionLabel.Text = GetStars(CalculateSatisfaction());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private int CalculateSostenibility()
	{
		double used = Score.GetEnergyUsed();
		double notUsed = Score.GetEnergyNotUsed();

		float percentage = (float)(notUsed / used * 100);
		float goodPercentage = 100 - percentage;

		int stars = Mathf.RoundToInt(goodPercentage / 20);

		if (stars > 5) stars = 5;
		return stars;
	}

	private int CalculateSatisfaction()
	{
		float percentage = (float)(Score.GetUnhappyness() / Score.GetTimeAllPeople()) * 100;

		float goodPercentage = 100 - percentage;

		int stars = Mathf.RoundToInt(goodPercentage / 20);

		return stars;
	}

	private string GetStars(int n)
	{
		switch (n)
		{
			case 1: return "*";
			case 2: return "**";
			case 3: return "***";
			case 4: return "****";
			case 5: return "*****";
			default: return "";
		}
	}
}
