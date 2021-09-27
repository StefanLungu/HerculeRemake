using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip attack;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAttack()
    {
        audioSource.clip = attack;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopAttack()
    {
        audioSource.loop = false;
    }
}
