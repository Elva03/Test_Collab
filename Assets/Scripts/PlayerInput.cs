using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")] 
    [HideInInspector] public Vector2 moveDirection;

    [Header("Jump")] 
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool jumpPressed, jumpReleased, jumpHeld;

    [Header("Interact")] 
    [HideInInspector] public bool canInteract;
    [HideInInspector] public bool interactPressed, interactReleased, interactHeld;

    [SerializeField] private bool usingGamepad, usingDpad;

    private Keyboard _keyboard;
    private Gamepad _gamepad;

    private void Start()
    {
        _keyboard = Keyboard.current;
        _gamepad = Gamepad.current;
    }

    private void Update()
    {
        if (usingGamepad && _gamepad != null)
        {
            UpdateGamepadInput();
        }
        else
        {
            UpdateKeyboardInput();
        }
    }

    private void UpdateKeyboardInput()
    {
        moveDirection.x = (_keyboard.dKey.isPressed ? 1 : 0) + (_keyboard.aKey.isPressed ? -1 : 0);
        moveDirection.y = (_keyboard.wKey.isPressed ? 1 : 0) + (_keyboard.sKey.isPressed ? -1 : 0);

        jumpPressed = _keyboard.spaceKey.wasPressedThisFrame;
        jumpReleased = _keyboard.spaceKey.wasReleasedThisFrame;
        jumpHeld = _keyboard.spaceKey.isPressed;

        interactPressed = _keyboard.fKey.wasPressedThisFrame;
        interactReleased = _keyboard.fKey.wasReleasedThisFrame;
        interactHeld = _keyboard.fKey.isPressed;
    }

    private void UpdateGamepadInput()
    {
        if (usingDpad)
        {
            moveDirection.x = (_gamepad.dpad.right.isPressed ? 1 : 0) + (_gamepad.dpad.left.isPressed ? -1 : 0);
            moveDirection.y = (_gamepad.dpad.up.isPressed ? 1 : 0) + (_gamepad.dpad.down.isPressed ? -1 : 0); 
        }
        else
        {
            moveDirection = _gamepad.leftStick.ReadValue();
        }
    
        jumpPressed = _gamepad.buttonSouth.wasPressedThisFrame;
        jumpReleased = _gamepad.buttonSouth.wasReleasedThisFrame;
        jumpHeld = _gamepad.buttonSouth.isPressed;
        
        interactPressed = _gamepad.buttonEast.wasPressedThisFrame;
        interactReleased = _gamepad.buttonEast.wasReleasedThisFrame;
        interactHeld = _gamepad.buttonEast.isPressed;
    }
    
}
