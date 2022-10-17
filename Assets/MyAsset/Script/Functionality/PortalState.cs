using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalState : MonoBehaviour
{
    public bool portalUse;
    public int exitCount;
    private void Start()
    {
        portalUse = false;
        exitCount = 0;
    }
}
