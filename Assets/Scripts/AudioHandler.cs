using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _objectSound;

    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        Object.IsClicked += PlayObjectSound;
    }

    private void PlayObjectSound(int x) 
    {
        _audioSource.PlayOneShot(_objectSound);
    }

    private void OnDestroy()
    {
        Object.IsClicked -= PlayObjectSound;
    }
}
