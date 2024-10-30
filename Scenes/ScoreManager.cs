using Godot;
using System;

public partial class ScoreManager : Control
{
	Label energyNotUsed;
	Label energyUsed;
	Label unhappyness;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		energyNotUsed = GetChild<Label>(1);
		energyUsed = GetChild<Label>(2);
		unhappyness = GetChild<Label>(3);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		energyNotUsed.Text = "Energía malgastada: " + (int)(Score.GetEnergyNotUsed() / 10);
		energyUsed.Text = "Energía aprovechada: " + (int)(Score.GetEnergyUsed() / 10);
		unhappyness.Text = "Descontento: " + (int)(Score.GetUnhappyness() / 10);
	}
}
