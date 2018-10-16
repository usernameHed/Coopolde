using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
	[SerializeField]
	private int initialSimultaneousAudio = 30;

	private static Queue<AudioSource> audioSources;
	private static List<AudioSource> playingAudioSources;

	private float updateCheckDelay = 1.0F;

	void Awake ()
	{
		audioSources = new Queue<AudioSource>();
		playingAudioSources = new List<AudioSource>();

		for (int i = 0; i < initialSimultaneousAudio; i++)
		{
			CreateAudioObject();
		}
	}

	private void Update()
	{
		updateCheckDelay -= Time.unscaledDeltaTime;

		if (updateCheckDelay <= 0)
		{
			// Remove finished played audio
			for (int i = playingAudioSources.Count - 1; i >= 0; i--)
			{
				if (!playingAudioSources[i].isPlaying)
				{
					audioSources.Enqueue(playingAudioSources[i]);
					playingAudioSources.RemoveAt(i);
				}
			}

			updateCheckDelay += 1.0F;
		}
	}

	public static AudioSource PlayAudioClip(AudioClip clip, float pitch, float volume, bool loop)
	{
		if(audioSources.Count == 0)
		{
			CreateAudioObject();
		}

		AudioSource audioSource = audioSources.Dequeue();
		audioSource.clip = clip;
		audioSource.pitch = pitch;
		audioSource.volume = volume;
		audioSource.loop = loop;
		audioSource.Play();

		playingAudioSources.Add(audioSource);

		return audioSource;
	}

	private static void CreateAudioObject()
	{
		GameObject audioObject = new GameObject("AudioObject");
		AudioSource audioSource = audioObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSources.Enqueue(audioSource);
	}
}
