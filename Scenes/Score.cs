using Godot;
using System;

public partial class Score : Control
{

	static private double energyNotUsed = 0, energyUsed = 0;
	static private double unhappyness = 0;
	static private double timeAllPeople = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GD.Print(energySpent, energyUsed, unhappyness);
	}

	public static void NotUseEnergy(double time)
	{
		energyNotUsed += time;
	}
	public static void UseEnergy(double time)
	{
		energyUsed += time;
	}
	public static void AddUnhappyness(double time)
	{
		unhappyness += time;
	}
	public static void AddTime(double time)
	{
		timeAllPeople += time;
	}

	public static double GetEnergyNotUsed() => energyNotUsed;
	public static double GetEnergyUsed() => energyUsed;
	public static double GetUnhappyness() => unhappyness;
	public static double GetTimeAllPeople() => timeAllPeople;

	public static void ResetScore()
	{
		energyNotUsed = energyUsed = unhappyness = timeAllPeople = 0;
	}
}
