using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [field: SerializeField] public GameObject Missel { get; private set; }
    public void lunchMissile()
    {
        Instantiate(Missel, transform.position, transform.rotation);
    }
    
}
