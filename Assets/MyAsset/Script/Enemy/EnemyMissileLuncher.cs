using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileLuncher : MonoBehaviour
{
    [field: SerializeField] public GameObject Missel { get; private set; }
    IEnumerator MisselCoroutine()
    {
        while (true)
        {
            Vector3 f = transform.up.normalized;
            Instantiate(Missel, transform.position + f*1.6f, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("MisselCoroutine");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine("MisselCoroutine");
        }
    }
}
