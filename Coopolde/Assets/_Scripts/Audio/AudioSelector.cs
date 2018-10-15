using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSelector : MonoBehaviour
{
	[SerializeField]
	private AudioClipEntry[] clipsEntry;

	private Dictionary<string, AudioClipEntry> clips;

	private void Start()
	{
		clips = new Dictionary<string, AudioClipEntry>();

		foreach (AudioClipEntry ace in clipsEntry)
		{
			clips.Add(ace.key, ace);
		}
		clipsEntry = null;
	}

	public void PlayUI(string key)
	{
		Play (key);
	}

	public AudioSource Play(string key)
	{
		AudioClipEntry c = clips[key];
		return Play(key, Random.Range(0, c.clips.Length));
	}

	public AudioSource Play(string key, int index)
	{
		AudioClipEntry c = clips[key];
		float pitch = c.basePitch + (c.pitchOffset > 0 ? (((Random.value - 0.5F) * 2.0F) * c.pitchOffset) : 0.0F);
		float volume = c.baseVolume + (c.volumeOffset > 0 ? (((Random.value - 0.5F) * 2.0F) * c.volumeOffset) : 0.0F);
		return AudioPool.PlayAudioClip(c.clips[index], pitch, volume, c.loop);
	}

	public AudioSource Play(string key, float pitch)
	{
		AudioClipEntry c = clips[key];
		float volume = c.baseVolume + (c.volumeOffset > 0 ? (((Random.value - 0.5F) * 2.0F) * c.volumeOffset) : 0.0F);
		return AudioPool.PlayAudioClip(c.clips[Random.Range(0, c.clips.Length)], pitch, volume, c.loop);
	}

	public AudioSource Play(string key, float pitch, float volume)
	{
		AudioClipEntry c = clips[key];
		return AudioPool.PlayAudioClip(c.clips[Random.Range(0, c.clips.Length)], pitch, volume, c.loop);
	}
}

[System.Serializable]
public class AudioClipEntry
{
	[SerializeField]
	public string key;

	[SerializeField]
	public AudioClip[] clips;

	[SerializeField]
	public float basePitch = 1.0F;

	[SerializeField]
	public float pitchOffset = 0.0F;

	[SerializeField]
	public float baseVolume = 1.0F;

	[SerializeField]
	public float volumeOffset = 0.0F;

	[SerializeField]
	public bool loop = false;
}
