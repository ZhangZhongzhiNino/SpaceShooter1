using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipCollisionSystem : MonoBehaviour
{
    
    
    [field: SerializeField] public float collisionLoudness { get; private set; }

   
    private GameObject collidObject;

    void Start()
    {
        ShipControl.ship.collisionSound.Stop();
        ShipControl.ship.explosionSound.Stop();
        ShipControl.ship.damagedSound.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collidObject = collision.gameObject;
        if (collidObject.tag=="MyMissile" || collidObject.tag == "AirWall")
        {
            return;
        }
        if (collidObject.tag == "meteorites")
        {
            ShipControl.ship.collisionSound.Play();
            var meteScript = collidObject.GetComponent<Meteorite>();
            var DMG = meteScript.DMG;
            DMG *= ShipControl.ship.shipSpeed;
            DMG /= ShipControl.ship.shipDefense;
            ShipControl.ship.shipHealth -= DMG;
            ShipControl.ship.checkShipSta();
        }
        if(collidObject.tag == "EnMissile")
        {
            ShipControl.ship.collisionSound.Play();
            ShipControl.ship.shipHealth -= 50;
            ShipControl.ship.checkShipSta();
        }
        if(collidObject.tag == "Enemy")
        {
            ShipControl.ship.collisionSound.Play();
            ShipControl.ship.shipHealth -= 100;
            ShipControl.ship.checkShipSta();
        }
    }
   
    
    
}
