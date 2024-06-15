using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private IControllable _controllable;
    private GameInput _gameInput;
    private Vector2 _direction;

    public void SetControllable(IControllable conrollable)
    {
        _controllable = conrollable; 
    }

    private void Awake()
    {
        _gameInput = new GameInput();

        /*_controllable = GetComponent<IControllable>();
        if (_controllable != null)
            Debug.Log("No Controller Component");*/
    }

    private void OnEnable()
    {
        _gameInput.Enable();
        _gameInput.Gameplay.Shoot.performed += ShootOnPerformed;

    }



    private void OnDisable()
    {
        _gameInput.Disable();
        _gameInput.Gameplay.Shoot.performed -= ShootOnPerformed;
    }


    void Update()
    {
        ReadMovement();
    }

    private void ShootOnPerformed(InputAction.CallbackContext context)
    {
        _controllable.Shoot();
    }

    private void ReadMovement()
    {
        //Debug.Log(_gameInput.Gameplay.Aim.ReadValue<Vector2>());
        var moveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        var turnDirection = _gameInput.Gameplay.Turn.ReadValue<float>();
        _direction = new Vector2(moveDirection.x, moveDirection.y);
        _controllable.Turn(turnDirection);
        //Debug.Log(_direction);
        _controllable.Move(_direction);
    }
}
