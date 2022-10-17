using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTriggerSystem : MonoBehaviour
{
    GameObject triggerObj;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        triggerObj = collision.gameObject;
        if (triggerObj.tag == "EnMissile")
        {
            ShipControl.ship.shipHealth -= 50;
            ShipControl.ship.checkShipSta();
        }
    }
   
}
