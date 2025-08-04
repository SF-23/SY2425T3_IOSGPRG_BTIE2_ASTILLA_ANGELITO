using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private EnemyState _enemyState;
    [SerializeField] private float _speed  = 2f;
    [SerializeField] private float _sightRange = 3f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private bool _isTargetDetected;
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector2 _wanderTarget;

    [Header("Enemy")]
    [SerializeField] private Enemy _enemy;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyState = EnemyState.Wander;
        this.gameObject.GetComponentInChildren<CircleCollider2D>().radius = _sightRange;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(_target != null)
        {
            Seek();
            return;
        }

        Wander();
    }

    private void Wander()
    {
        _enemyState = EnemyState.Wander;
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

    private void Seek()
    {
        _enemyState = EnemyState.Seek;

        Vector2 direction = _target.transform.position - transform.position;
        direction.Normalize();

        // rotation
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (IsInRangeToAttack())
        {
            Debug.LogWarning("Fire");
            DestroyTarget();
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position,_speed * Time.deltaTime);
    }

    public void DestroyTarget()
    {
       _enemy.ShootWeapon();
    }

    private bool IsInRangeOfTarget()
    {
        return Vector3.Distance(_target.transform.position,transform.position) < _sightRange;
    }
    private bool IsInRangeToAttack()
    {
        return Vector3.Distance(_target.transform.position,transform.position) < _attackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<HealthComponent>() != null) 
        { 
            _target = collision.gameObject;
        }
        else
        {
            Debug.LogError("No Target");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            _target = null;
        }
    }

}
