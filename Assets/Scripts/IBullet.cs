using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void Move();
    void Bounce(Vector3 normal);
    void Dispose();
    void CountBounces();
}
    
