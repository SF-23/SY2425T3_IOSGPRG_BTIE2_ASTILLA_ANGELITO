using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystickMovement;
    [SerializeField] private Joystick _joystickRotation;
    [SerializeField] private float _playerSpd;
    [SerializeField] private float _playerRotSpd;
    [SerializeField] private Rigidbody2D _rb;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayerMove();
        PlayerRotate();
    }

    private void PlayerMove()
    {
       if(_joystickMovement.Direction.y != 0)
       {
            _rb.velocity = new Vector2(_joystickMovement.Direction.x * _playerSpd * Time.deltaTime, 
                                       _joystickMovement.Direction.y * _playerSpd * Time.deltaTime);
       }
       else
       {
            _rb.velocity = Vector2.zero;
       }
    }

    private void PlayerRotate()
    {
        Vector2 rotDirection = _joystickRotation.Direction;

        if (rotDirection.magnitude > 0.1f) // Use a small threshold
        {
            float angle = Mathf.Atan2(rotDirection.y, rotDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            
            _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle - 90f, _playerRotSpd * Time.deltaTime);
        }
    }
}
