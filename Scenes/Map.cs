using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Map : Control
{


	const int MATRIX_SIZE = 6;
	int currentFloor = 0, maxFloor = 2;
	Array<NPC> npcs = new Array<NPC>();
	Array<Room> rooms = new Array<Room>();
	Array<CanvasItem> floors = new Array<CanvasItem>();

	int[,] adjacencyMatrix = new int[MATRIX_SIZE, MATRIX_SIZE] {
	{ 0, 1, 0, 0, 0, 0},
	{ 1, 0, 0, 0, 0, 1},
	{ 0, 0, 0, 0, 0, 1},
	{ 0, 0, 0, 0, 0, 1},
	{ 0, 0, 0, 0, 0, 1},
	{ 0, 1, 1, 1, 1, 0}};

	public override void _Ready()
	{
		Score.ResetScore();
		var npcNodes = GetTree().GetNodesInGroup("NPC");
		var roomNodes = GetTree().GetNodesInGroup("Room");
		var floorNodes = GetTree().GetNodesInGroup("Floor");

		foreach (var npc in npcNodes)
		{
			npcs.Add(npc as NPC);
		}
		foreach (var room in roomNodes)
		{
			rooms.Add(room as Room);
			rooms.Last().Setup(this, rooms.Count - 1);
		}
		foreach (var floor in floorNodes)
		{
			floors.Add(floor as CanvasItem);
		}

		SetupFloor(0);

		var shuffledRooms = rooms.Duplicate();
		shuffledRooms.Shuffle();


		for (int i = 0; i < npcs.Count; i++)
		{
			shuffledRooms[i].CallDeferred("EnterNPC", npcs[i]);
		}
	}

	public void MoveNPCBetweenRooms(int roomIndex, NPC npc, bool isNightMonster)
	{
		if (isNightMonster)
		{
			rooms[GD.RandRange(0, rooms.Count - 1)].EnterNPC(npc);
			return;
		}
		var possibleRoomsToMove = new List<int>();
		for (int i = 0; i < rooms.Count; i++)
		{
			if (adjacencyMatrix[roomIndex, i] == 0) continue;

			possibleRoomsToMove.Add(i);
		}

		int roomToMove = possibleRoomsToMove[GD.RandRange(0, possibleRoomsToMove.Count - 1)];
		rooms[roomToMove].EnterNPC(npc);
	}

	private void SetupFloor(int index)
	{
		for (int i = 0; i < floors.Count; i++)
		{
			floors[i].Visible = index == i;
		}
	}

	private void NextFloor()
	{
		currentFloor++;
		if (currentFloor >= maxFloor) currentFloor = 0;
		SetupFloor(currentFloor);
	}

	// public override void _UnhandledInput(InputEvent @event)
	// {
	// 	if (@event is InputEventKey key)
	// 	{
	// 		if (!key.Echo && key.Pressed && key.Keycode == Key.Space)
	// 		{
	// 			NextFloor();
	// 		}
	// 	}
	// }

}
