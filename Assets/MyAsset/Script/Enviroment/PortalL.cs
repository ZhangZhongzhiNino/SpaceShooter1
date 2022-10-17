using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalL : MonoBehaviour
{
    public static PortalL instance;
    [field: SerializeField] public GameObject Portallremove { get; private set; }
    private void Start()
    {
        if (instance != null) 
        {
            Instantiate(Portallremove, instance.transform.position, instance.transform.rotation);
            Destroy(instance.gameObject); 
        }
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PortalR.instance == null) return;
        PortalState ps = collision.GetComponent<PortalState>();
        if (!ps.portalUse)
        {
            ps.portalUse = true;
            if (collision.gameObject.tag == "Player") ShipControl.ship.Teleport.Play();
            collision.gameObject.transform.position = PortalR.instance.transform.position;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PortalR.instance == null) return;
        PortalState ps = collision.GetComponent<PortalState>();
        ps.exitCount += 1;
        if (ps.exitCount > 1)
        {
            ps.portalUse = false;
            ps.exitCount = 0;
        }
    }
}
