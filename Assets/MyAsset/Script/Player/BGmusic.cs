using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;

    [field: SerializeField] private AudioClip A1;
    [field: SerializeField] private AudioClip A2;
    [field: SerializeField] private AudioClip A3;
    private AudioSource Source;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    private void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!Source.isPlaying) PlayNextMusic();
        if (ShipControl.ship == null) return;
        transform.position = ShipControl.ship.transform.position;
    }
    void PlayNextMusic()
    {
        if (Source.clip == A1) Source.clip = A2;
        else if (Source.clip == A2) Source.clip = A3;
        else if (Source.clip == A3) Source.clip = A1;
        Source.Play();
    }
    public void SwitchTo(int i)
    {
        if (i == 1) Source.clip = A1;
        else if (i == 2) Source.clip = A2;
        else if (i == 3) Source.clip = A3;
        Source.Play();
    }
}
