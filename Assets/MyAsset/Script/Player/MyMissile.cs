using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMissile : MonoBehaviour
{
    [field: SerializeField] private float forceMagnitude;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        StartCoroutine(destoryCountDown());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        addForce();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject == this) return;
        Destroy(this.gameObject);
    }
    Vector2 calcForce()
    {
        Vector2 direction = ShipControl.ship.transform.position - transform.position;
        direction = direction.normalized;
        direction *= forceMagnitude;
        return direction;
    }
    Vector2 calcForceNF()
    {
        Vector2 direction = Vector2.up;
        direction *= forceMagnitude;
        return direction;
    }
    void addForce()
    {
        rb2d.AddRelativeForce(calcForceNF());
    }
    IEnumerator destoryCountDown()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
