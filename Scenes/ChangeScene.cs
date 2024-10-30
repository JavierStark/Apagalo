using Godot;
using System;

public partial class ChangeScene : Button
{
	[Export]
	string sceneToChange;
	public override void _Ready()
	{
		Pressed += () => GetTree().ChangeSceneToFile(sceneToChange);
	}
}
