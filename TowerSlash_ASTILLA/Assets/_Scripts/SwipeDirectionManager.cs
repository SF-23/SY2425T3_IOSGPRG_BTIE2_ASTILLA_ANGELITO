using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up, Right, Down, Left,None
}

public class SwipeDirectionManager : Singleton<SwipeDirectionManager>
{
    private Vector2 _touchStartPosition;
    public Direction enum_currentDir;
    public bool _isSwipeProcessed = false;
    [SerializeField] private float _minSwipeDistance = 50f;
    [SerializeField] private float _directionThreshold = 0.9f;

    private void Update()
    {
        _isSwipeProcessed = false;

        if (Input.touchCount <= 0)
        {
            enum_currentDir = Direction.None;
            return;
        }
           

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartPosition = touch.position;
                //_isSwipeProcessed = true;
                break;
            case TouchPhase.Ended:
                EvaluateSwipe(touch.position);

                if(enum_currentDir != Direction.None)
                {
                    _isSwipeProcessed = true;
                }
                
                break;
            default:
                break;
        }
    }

    private void EvaluateSwipe(Vector2 endPosition)
    {
        Vector2 swipeVector = endPosition - _touchStartPosition;

        if (swipeVector.magnitude < _minSwipeDistance) //to stop code from running of the distance of the drag is too short 
        {
            Debug.Log("Swipe too short.");
            return;
        }

        swipeVector.Normalize();

        Vector2 up = Vector2.up;     // (0, 1)
        Vector2 down = Vector2.down;   // (0, -1)
        Vector2 left = Vector2.left;   // (-1, 0)
        Vector2 right = Vector2.right; // (1, 0)

        float dotUp = Vector2.Dot(swipeVector, up);
        float dotDown = Vector2.Dot(swipeVector, down);
        float dotLeft = Vector2.Dot(swipeVector, left);
        float dotRight = Vector2.Dot(swipeVector, right);

        if (dotRight > _directionThreshold)
        {
            enum_currentDir = Direction.Right;
            Debug.Log("Swipe: Right");
        }
        else if (dotLeft > _directionThreshold)
        {
            enum_currentDir = Direction.Left;
            Debug.Log("Swipe: Left");
        }
        else if (dotUp > _directionThreshold)
        {
            enum_currentDir = Direction.Up;
            Debug.Log("Swipe: Up");
        }
        else if (dotDown > _directionThreshold)
        {
            enum_currentDir = Direction.Down;
            Debug.Log("Swipe: Down");
        }

        StartCoroutine(CO_NoInput());
    }

    private IEnumerator CO_NoInput()
    {
        yield return new WaitForSeconds(0.2f);
        enum_currentDir = Direction.None;
    }

    public bool IsSwipeDetectedThisFrame()
    {
        return _isSwipeProcessed;
    }
}
