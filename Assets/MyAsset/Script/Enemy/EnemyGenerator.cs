using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [field: SerializeField] private GameObject enemyShip;
    private void Start()
    {
        StartCoroutine("createEnemy");
    }
    IEnumerator createEnemy()
    {
        while (ShipControl.ship.shipRender.enabled)
        {
            yield return new WaitForSeconds(Random.Range(10,30));
            Instantiate(enemyShip, transform.position, transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DisableCreater")
        {
            StopCoroutine("createEnemy");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DisableCreater")
        {
            StartCoroutine("createEnemy");
        }
    }

}
