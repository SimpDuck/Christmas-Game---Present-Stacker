using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
 public class InputManager : MonoBehaviour
{
    public delegate void MousePressEvent();
    public event MousePressEvent OnMousePress;
    public delegate void MousePressEventOver();
    public event MousePressEventOver OnMousePressed;


    private PlayerControls playerControls;

    
    private void Awake()
    {
        playerControls = new PlayerControls();
        SetupSingleton();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void Start()
    {
        playerControls.Touch.TouchInput.started += _  => MousePress();
    }

    private void MousePress()
    {
        if (OnMousePress != null) OnMousePress();
    }

    private void MousePressed()
    {
        if (OnMousePressed != null) OnMousePressed();
    }

    private void SetupSingleton()
    {
        int numberInputManager = FindObjectsOfType<InputManager>().Length;
        if (numberInputManager > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

}
