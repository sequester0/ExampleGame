using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private bool _musicEnable;

    [SerializeField]
    private AudioSource music;

    private void Awake()
    {
        _musicEnable = true;
    }

    public void ToggleMusic()
    {
        if (!_musicEnable)
        {
            //Enable Music
            //music.enabled = true;
            music.Play();
            _musicEnable = true;
        }
        else
        {
            //Disable Music
            //music.enabled = false;
            music.Pause();
            _musicEnable = false;
        }
    }
}
