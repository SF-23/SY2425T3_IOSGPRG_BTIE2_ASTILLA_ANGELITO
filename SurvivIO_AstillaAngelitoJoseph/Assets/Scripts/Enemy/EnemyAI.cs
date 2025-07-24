using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyState _enemyState;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector2 _wanderTarget;

    [SerializeField] private CircleCollider2D _seekRange;
    [SerializeField] private CircleCollider2D _destroyRange;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Wander();
    }

    private void Wander()
    {
        float wanderRadius = 20;
        float wanderDist = 10;
        float wanderJitter = 1;

        _wanderTarget += new Vector2(Random.Range(-1.0f, 1.0f) * wanderJitter, Random.Range(-1.0f, 1.0f) * wanderJitter);

        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector2 targetLocal = _wanderTarget + new Vector2(0, wanderDist);
        Vector2 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        this.transform.Translate(targetWorld * _speed * Time.deltaTime / 4);
    }

    private void Seek(GameObject target)
    {
        this.transform.Translate(target.transform.position * _speed * Time.deltaTime);
    }

    private void Destroy(GameObject target)
    {
        //Shoot()
    }

    private Vector2 DistanceChecker(GameObject target)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            _target = collision.gameObject;
        }
    }
}
