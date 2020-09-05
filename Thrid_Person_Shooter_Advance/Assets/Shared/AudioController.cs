using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip[] clips;
    [SerializeField] float DelayBetweenClips;

    bool canPlay;

    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        if (!canPlay)
            return;
        GameManager.Instance.Timer.add(() =>
        {
            canPlay = true;
        }, DelayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }

    public void Stop()
    {
        source.Stop();
    }
}
