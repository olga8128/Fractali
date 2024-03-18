using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clic1;
    public AudioClip clic2;
    public AudioClip clic3;
    public AudioClip echo1;
    void Start()
    {
    }

    public void ClicSound()
    {
        audioSource.PlayOneShot(clic1);
    }

    public void EchoSound()
    {
        audioSource.PlayOneShot(echo1);
    }

    public void OkSound()
    {
        audioSource.PlayOneShot(clic2);
    }

    public void CancelSound()
    {
        audioSource.PlayOneShot(clic3);
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }
}
