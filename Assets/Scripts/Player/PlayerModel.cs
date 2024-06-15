using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : IControllable, IRespawnable
{
    public event Action Respawned;
    public float TurnSpeed { get { return _turnDirection; } }
    public Vector2 Velocity { get { return _velocity; } }

    private float _speed = 5f;
    private float _turnSpeed = 150f;
    private Vector2 _velocity;
    private float _turnDirection;
    //private float _lookDirection;

    public ScoreModel ScoreModel = new ScoreModel();


    public void Move(Vector2 direction)
    {
        _velocity = direction * _speed;
    }

    public void Shoot()
    {
        //уменьшение патронов и тд
    }

    public void Turn(float direction)
    {
        _turnDirection = direction * _turnSpeed;
        //Debug.Log(_turnDirection);
    }

    public void Respawn()
    {
        Respawned?.Invoke();
    }
}
