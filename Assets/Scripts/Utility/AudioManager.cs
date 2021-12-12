/*
Nathan Nguyen
101268067

12/12/2021
Audio Manager used in A1 and learned through breckys
Created Static instance


*/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
	public Sound[] sounds;



    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<AudioManager>("AudioManager"));
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }



	void Awake()
	{
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
            s.source.volume = s.volume;
		}
	}

	public static void Play(string sound)
	{
		Sound s = Array.Find(instance.sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.Log("Sound Clip not found");
			return;
		}
		s.source.Play();
	}

    public static void Stop(string sound)
    {
        Sound s = Array.Find(instance.sounds, item => item.name == sound);
		s.source.Stop();
    }

}
