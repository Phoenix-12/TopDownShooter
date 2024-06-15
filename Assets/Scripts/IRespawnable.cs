using System;

internal interface IRespawnable
{
    public event Action Respawned;
    public void Respawn();
}