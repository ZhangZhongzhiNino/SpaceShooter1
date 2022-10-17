using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnShipControl : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public Rigidbody2D rb2d { get; private set; }
    [field: SerializeField] public GameObject destoryEffect { get; private set; }
    [field: SerializeField] public GameObject destorySound { get; private set; }

    private void FixedUpdate()
    {
        if (ShipControl.ship == null) return;
        ApplyRotation();
        addForce();
    }
    void ApplyRotation()
    {
        transform.up = new Vector2(
            ShipControl.ship.transform.position.x - transform.position.x,
            ShipControl.ship.transform.position.y - transform.position.y
            );
    }
    Vector2 calcForce()
    {
        Vector2 direction = ShipControl.ship.transform.position - transform.position;
        direction = direction.normalized;
        direction *= speed;
        return direction;
    }
    void addForce()
    {
        rb2d.AddForce(calcForce());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(destoryEffect, transform.position, transform.rotation);
        if(ShipControl.ship != null)
        {
            if (ShipControl.ship.shipRender.enabled)
            {
                Instantiate(destorySound, transform.position, transform.rotation);
                ScoreCounter.instance.score += 1;
            }
        }
        Destroy(this.gameObject);
    }
}
