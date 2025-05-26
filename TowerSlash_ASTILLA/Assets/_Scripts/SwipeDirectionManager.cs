using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDirectionManager : MonoBehaviour
{
    private Vector2 _touchStartPosition;

    private void Update()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartPosition = touch.position;
                break;
            case TouchPhase.Ended:
                EvaluateSwipe(touch.position);
                break;
            default:
                break;
        }
    }

    private void EvaluateSwipe(Vector2 endPosition)
    {
        if (endPosition.x < _touchStartPosition.x)
        {
            Debug.Log("Left");
        }
        else if (endPosition.x > _touchStartPosition.x)
        {
            Debug.Log("Right");
        }
    }
}
