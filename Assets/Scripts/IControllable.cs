using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void Move(Vector2 direction);
    void Turn(float direction);
    void Shoot();
}
