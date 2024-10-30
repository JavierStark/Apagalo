using Godot;
using System;

public partial class LightButton : TextureButton
{

	[Export]
	Light2D light;
	AudioStreamPlayer audio;

	bool lastStateChanged;


	public override void _Ready()
	{
		audio = GetChild<AudioStreamPlayer>(0);
		ButtonPressed = light.Enabled;
		Toggled += (bool state) => ToggleButton(state);

	}

	public override void _Process(double delta)
	{
		if (lastStateChanged != light.Enabled)
		{
			SetPressedNoSignal(light.Enabled);
			lastStateChanged = light.Enabled;
		}
	}

	public void ToggleButton(bool state)
	{
		light.Enabled = state;
		lastStateChanged = state;
		audio.Play();
	}
}
