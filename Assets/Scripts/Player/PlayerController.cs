using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : IControllable
{
    private PlayerModel _model;
    private PlayerView _view;

    public PlayerConroller(PlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;
    }

    public void Move(Vector2 direction)
    {
        _model.Move(direction);
        _view.Move(_model.Velocity);

        _view.BulletHited += _model.Respawn;
        _view.BulletHited += _view.PlaceOnSpawn;
        _model.Respawned += () => { _model.ScoreModel.Score++; };
        _model.ScoreModel.ScoreUpdated += (a) => { };
    }

    public void Shoot()
    {
        _model.Shoot();
        _view.Shoot();
    }

    public void Turn(float direction)
    {
        _model.Turn(direction);
        _view.Turn(_model.TurnSpeed);
    }
}