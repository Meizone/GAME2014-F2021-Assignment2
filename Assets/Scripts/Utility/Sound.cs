/*
Nathan Nguyen
101268067

12/12/2021

Sound System


*/

using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 1.0f;
	[Range(.1f, 1f)]
	public float pitch = 1f;
	public bool loop = false;
	

	[HideInInspector]
	public AudioSource source;

}