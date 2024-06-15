using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerView : MonoBehaviour, IControllable
{
    public event Action BulletHited;

    private Rigidbody2D _rb;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField]private Transform _spawnPointTransform;

    //public Vector2 MoveDirection;

    public void TakeBulletHit()
    {
        BulletHited?.Invoke();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        _rb.velocity = (transform.up * velocity.y + transform.right * velocity.x);
    }

    public void SetPosition(Vector2 position, float rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    public void Shoot()
    {
        Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        Debug.Log("SHOOT!");
    }

    public void Turn(float rotate)
    {
        _rb.rotation += rotate * Time.deltaTime;
    }

    internal void PlaceOnSpawn()
    {
        transform.position = _spawnPointTransform.position;
        transform.rotation = _spawnPointTransform.rotation;
    }
}
