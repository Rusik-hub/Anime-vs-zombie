using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private void Start()
    {
        transform.rotation = Quaternion.AngleAxis(90, Vector3.back);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BulletDestroyer>(out BulletDestroyer bulletDestroyer))
        {
            Destroy(this.gameObject);
        }

        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
