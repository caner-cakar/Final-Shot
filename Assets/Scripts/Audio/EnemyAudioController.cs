using NUnit.Framework;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip attackClip;
    public AudioClip chaseClip;

    [Header("Audio Settings")]
    public float walkVolume = 0.6f;
    public float attackVolume = 0.6f;
    public float chaseVolume = 0.6f;

    private AudioSource audioSource;
    private string currentState = "";


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayWalkSound()
    {
        if (currentState == "Walk") return;
        PlayClip(walkClip, walkVolume, "Walk", loop: true);
    }

    public void PlayAttackSound()
    {
        PlayClip(attackClip, attackVolume, "Attack");
    }

    public void PlayChaseSound()
    {
        if (currentState == "Chase") return;
        PlayClip(chaseClip, chaseVolume, "Chase", loop: true);
    }

    public void StopSound()
    {
        audioSource.Stop();
        currentState = "";
    }

    private void PlayClip(AudioClip clip, float volume, string state, bool loop = false)
    {
        if (clip == null) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
        currentState = state;
    }
}
