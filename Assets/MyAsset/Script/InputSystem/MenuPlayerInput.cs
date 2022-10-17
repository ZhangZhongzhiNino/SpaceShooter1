using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPlayerInput : MonoBehaviour, MenuShipControlInput.IPlayerInActions
{
    MenuShipControlInput menuIn;
    private void Awake()
    {
        if (!Mouse.current.enabled)
        {
            InputSystem.EnableDevice(Mouse.current);
        }
    }
    private void OnEnable()
    {
        menuIn = new MenuShipControlInput();
        menuIn.PlayerIn.SetCallbacks(this);
        menuIn.PlayerIn.Enable();
    }
    private void OnDisable() => menuIn.PlayerIn.Disable();

    public void OnA1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(1);
    }

    public void OnA2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(2);
    }

    public void OnA3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        BGmusic.instance.SwitchTo(3);
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Application.Quit();
    }
}
