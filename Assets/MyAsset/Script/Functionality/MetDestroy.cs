using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetDestroy : MonoBehaviour
{
    ParticleSystem metPat;
    void Start()
    {
        metPat = GetComponent<ParticleSystem>();
        metPat.Play();
    }
}
