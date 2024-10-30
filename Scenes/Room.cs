using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public partial class Room : Control
{
	[Export]
	bool logNumber = false;
	int index;
	Container container;
	PointLight2D light;
	Array<Device> devices = new Array<Device>();
	Map map;
	DayNightCycle dayNightCycle;

	Array<NPC> npcsInRoom = new Array<NPC>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		dayNightCycle = GetNode<DayNightCycle>("/root/Main/DayNightCycle");


		foreach (var child in GetChildren())
		{
			if (child as PointLight2D != null)
			{
				light = child as PointLight2D;
				continue;
			}
			if (child as Container != null)
			{
				container = child as Container;
				continue;
			}
			if (child as Device != null)
			{
				devices.Add(child as Device);
				continue;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckLight(delta);
	}

	public void Setup(Map map, int index)
	{
		this.map = map;
		this.index = index;
	}

	public void EnterNPC(NPC npc)
	{
		if (npc.GetParent() != null) npc.GetParent().RemoveChild(npc);
		container.AddChild(npc);
		npc.SetRoom(this);

		if (!npc.IsNightMonster()) npcsInRoom.Add(npc);
	}
	public void ExitNPC(NPC npc)
	{
		if (!npc.IsNightMonster()) npcsInRoom.Remove(npc);
		container.RemoveChild(npc);
	}

	public void MoveNPC(NPC npc, bool isNightMonster)
	{
		ExitNPC(npc);
		map.MoveNPCBetweenRooms(index, npc, isNightMonster);
	}

	public void TurnLightOn()
	{
		light.Enabled = true;
	}

	public void TurnOnDevice()
	{
		if (devices == null) return;
		foreach (var device in devices)
		{
			if (!device.IsOn())
			{
				device.TurnOn();
				return;
			}
		}
	}


	private void CheckLight(double delta)
	{
		foreach (var device in devices)
		{
			if (device.IsOn()) Score.NotUseEnergy(delta);
		}

		if (light.Enabled)
		{
			if (GetNumberOfPeople() > 0) Score.UseEnergy(delta);
			else Score.NotUseEnergy(delta);
		}
		else
		{
			if (GetNumberOfPeople() > 0) Score.AddUnhappyness(delta);
		}
		Score.AddTime(delta);
	}

	private int GetNumberOfPeople()
	{
		int count = 0;
		foreach (var npc in npcsInRoom)
		{
			if (!npc.IsNightMonster()) count++;
		}
		if (logNumber) GD.Print(count);
		return count;
	}
}
