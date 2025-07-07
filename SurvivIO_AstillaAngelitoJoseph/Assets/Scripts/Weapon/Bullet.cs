using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _bulletDmg;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _bulletRange;

    public float SetBulletDmg(float _dmg)
    {
        _bulletDmg = _dmg;
        return _bulletDmg;
    }

    public float SetBulletRange(float _range) 
    { 
        _bulletRange = _range;
        return _bulletRange;
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);

        Destroy(this.gameObject, _bulletRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)bulletDmg);
            Destroy(this.gameObject);
        }
        */
    }
}
