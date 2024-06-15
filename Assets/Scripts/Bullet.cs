using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] public Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private int _bounceCounter;
    [SerializeField] private int _maxCountBounce;
    private Rigidbody2D _rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlayerView player))
        {
            player.TakeBulletHit();
        }
        else
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Bounce(contact.normal);
                
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _direction = transform.up;
        _rb.velocity = _speed * transform.up;
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Bounce(Vector3 normal)
    {
        _direction = Vector3.Reflect(_direction, normal);
        CountBounces();
    }

    public void CountBounces()
    {
        _bounceCounter++;
        if (_bounceCounter == _maxCountBounce)
        {
            Dispose();
        }
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        _rb.velocity = _speed * _direction;
    }
}
