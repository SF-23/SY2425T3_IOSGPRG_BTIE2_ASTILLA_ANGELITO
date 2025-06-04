using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArrowColor
{
    Red,Yellow,Green
}

public class ArrowClass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite[] _arrowSprites;
    [SerializeField] private SpriteRenderer _arrowSR;
    [SerializeField] private GameObject _arrowBG;
    [SerializeField] private Direction enum_arrowDirection;

    public Direction _getEnumArrowDir { get { return enum_arrowDirection; } }

    [Header("Options")]
    [SerializeField] private ArrowColor enum_arrowColor;
    [SerializeField] private float _interval;
    [SerializeField] private bool _isPlayerNear;

    private int _currentArrow = 0;

    public ArrowColor _setEnumArrowColor { get { return _setEnumArrowColor; } set { enum_arrowColor = value; } }

    private void Awake()
    {
        SetArrowColor();
    }

    private void SetRandomArrowDir()
    {
        _currentArrow = Random.Range(0, 3);
        _arrowSR.sprite = _arrowSprites[_currentArrow];
        ArrowEnumUpdate();
    }

    public bool _setIsPlayerNear
    {
        get { return _isPlayerNear; }
        set
        {
            if (_isPlayerNear != value) // Only react if the value actually changes
            {
                _isPlayerNear = value;
                _arrowBG.SetActive(true);
            }
        }
    }

    private IEnumerator CO_RotateArrow()
    {
        yield return new WaitForSeconds(_interval);

        _currentArrow++;
        _currentArrow %= _arrowSprites.Length;

        _arrowSR.sprite = _arrowSprites[_currentArrow];

        if (!_isPlayerNear)
        {
            StartCoroutine(CO_RotateArrow());
        }
        else
        {
            _arrowBG.SetActive(true);
            ArrowEnumUpdate();
            yield break;
            // show black box & stop rotating
        }
    }

    private void ArrowEnumUpdate()  //to assign a Enum according to the direction of arrow after it stops rotating
    {
        switch(_currentArrow)
        {
            case 0:
                enum_arrowDirection = Direction.Up; 
                break;
            case 1:
                enum_arrowDirection = Direction.Right;
                break;
            case 2:
                enum_arrowDirection = Direction.Down;
                break;
            case 3:
                enum_arrowDirection = Direction.Left;
                break;
            default:
                Debug.LogWarning("Arrow Enum Error");
                break;
        }
    }

    private void SetArrowColor()
    {
        switch(enum_arrowColor)
        {
            case ArrowColor.Red:
                _arrowSR.color = Color.red;
                SetRandomArrowDir();
                break;
            case ArrowColor.Green:
                _arrowSR.color = Color.green;
                SetRandomArrowDir();
                break;
            case ArrowColor.Yellow:
                _arrowSR.color = Color.yellow;
                StartCoroutine(CO_RotateArrow());
                break;
            default:
                return;
        }
    }
}
