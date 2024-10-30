using Godot;
using System;
using System.Runtime.Serialization;

public partial class Device : Control
{
	[Export]
	TextureButton turnOffButton;

	AnimationPlayer animationPlayer;
	AudioStreamPlayer audio;

	string animation = "DeviceOn";


	bool isOn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetChild<AnimationPlayer>(0);
		audio = GetChild<AudioStreamPlayer>(1);

		turnOffButton.Pressed += TurnOff;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TurnOn()
	{
		isOn = true;
		turnOffButton.Disabled = false;
		animationPlayer.Play(animation);
		audio.Play();
	}

	public void TurnOff()
	{
		isOn = false;
		turnOffButton.Disabled = true;
		animationPlayer.Stop();
		audio.Stop();
	}

	public bool IsOn()
	{
		return isOn;
	}

}
