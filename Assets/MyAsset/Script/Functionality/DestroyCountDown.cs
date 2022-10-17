using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCountDown : MonoBehaviour
{
    [field: SerializeField] public float duration;
    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown() 
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

}
