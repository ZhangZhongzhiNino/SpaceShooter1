using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float DMG;
    private int health;
    [field: SerializeField] GameObject metDestoryEffect;
    private void Awake()
    {
        DMG = 10;
        health = 5;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 1;
        if (health < 1)
        {
            Instantiate(metDestoryEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
