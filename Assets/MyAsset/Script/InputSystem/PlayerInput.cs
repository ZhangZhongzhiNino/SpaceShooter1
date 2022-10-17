using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour, ShipControlInput.IPlayerInActions
{
    [field: SerializeField]public GameObject Portalr { get; private set; }
    [field: SerializeField] public GameObject Portall { get; private set; }
    [field: SerializeField] public Camera MainCamera { get; private set; }
    [field: SerializeField] public CanvasGroup black { get; private set; }
    private ShipControlInput controls;
    //out var
    public Vector2 movement;
    public float tourk;
    public Vector2 mouseLocation;
    public static PlayerInput instance;
    private void Awake()
    {
        if (!Mouse.current.enabled)
        {
            InputSystem.EnableDevice(Mouse.current);
        }
        if (instance != null) Destroy(instance);
        else instance = this;
        black.alpha = 1;
        StartCoroutine(Open());
    }
    private void Start()
    {
        controls = new ShipControlInput();
        controls.PlayerIn.SetCallbacks(this);
        controls.PlayerIn.Enable();
        //controls.PlayerIn.MosePosition.performed += OnMosePosition;
        canLunch = true;
    }
    public void OnLocation(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void OnRotation(InputAction.CallbackContext context)
    {
        tourk = -context.ReadValue<float>();
    }
    public void OnLunchMissile(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (canLunch)
        {
            ShipControl.ship.missileLuncher.lunchMissile();
            StartCoroutine("missileTimmer");
        }
    }

    public void OnMiddleMouse(InputAction.CallbackContext context)
    {
        if (ShipControl.ship == null) return;
        if (!ShipControl.ship.shipRender.enabled) return;
        if (!context.performed) return;
        Destroy(PortalL.instance.gameObject);
        Destroy(PortalR.instance.gameObject);
    }

    //missile timmer
    bool canLunch;
    IEnumerator missileTimmer()
    {
        canLunch = false;
        yield return new WaitForSeconds(0.3f);
        canLunch = true;
    }
    //---------------------------------
    void ShipControlInput.IPlayerInActions.OnMousePosition(InputAction.CallbackContext context)
    {
        mouseLocation = context.ReadValue<Vector2>();
    }

    void ShipControlInput.IPlayerInActions.OnMouseL(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (ShipControl.ship == null) return;
        if (!ShipControl.ship.shipRender.enabled) return;
        if (ScoreCounter.instance.score < 1) return;
        ScoreCounter.instance.score -= 1;
        Vector3 l = MainCamera.ScreenToWorldPoint(mouseLocation);
        l.z = 1;
        Instantiate(Portall, l, Quaternion.identity);
    }

    void ShipControlInput.IPlayerInActions.OnMouseR(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (ShipControl.ship == null) return;
        if (!ShipControl.ship.shipRender.enabled) return;
        if (ScoreCounter.instance.score < 1) return;
        ScoreCounter.instance.score -= 1;
        Vector3 l = MainCamera.ScreenToWorldPoint(mouseLocation);
        l.z = 1;
        Instantiate(Portalr, l, Quaternion.identity);
    }

    void ShipControlInput.IPlayerInActions.OnRestart(InputAction.CallbackContext context)
    {
        StartCoroutine(Close());
        
    }
    void destroyAllSingleten()
    {
        controls.PlayerIn.Disable();
        if (ShipControl.ship != null) Destroy(ShipControl.ship.gameObject);
        if (PortalR.instance!= null) Destroy(PortalR.instance);
        if (PortalL.instance != null) Destroy(PortalL.instance);
        if (TimeManager.instance != null) Destroy(TimeManager.instance);
        if (ScoreCounter.instance != null) Destroy(ScoreCounter.instance);
        ShipControl.ship = null;
        PortalR.instance = null;
        PortalL.instance = null;
        TimeManager.instance = null;
        ScoreCounter.instance = null;
    }
    IEnumerator Close()
    {
        while (black.alpha < 1)
        {
            black.alpha += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        destroyAllSingleten();
        SceneManager.LoadSceneAsync("BaseScene");
        yield return null;
    }
    IEnumerator Open()
    {
        while (black.alpha > 0)
        {
            black.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    void ShipControlInput.IPlayerInActions.OnQuit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Application.Quit();
    }

    void ShipControlInput.IPlayerInActions.OnA1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(1);
    }

    void ShipControlInput.IPlayerInActions.OnA2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(2);
    }

    void ShipControlInput.IPlayerInActions.OnA3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(3);
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        float scrollValue = context.ReadValue<float>() / 240;
        if(MainCamera.orthographicSize - scrollValue > 7 && MainCamera.orthographicSize - scrollValue < 17)
        {
            MainCamera.orthographicSize -= scrollValue;
        }
    }

}
