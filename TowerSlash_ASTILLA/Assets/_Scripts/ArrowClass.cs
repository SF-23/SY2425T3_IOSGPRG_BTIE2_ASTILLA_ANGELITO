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
    [SerializeField] private Direction _arrowDirection;

    public Direction _getEnumArrowDir { get { return _arrowDirection; } }

    [Header("Options")]
    [SerializeField] private ArrowColor _arrowColor;
    [SerializeField] private float _interval;
    [SerializeField] private bool _isPlayerNear;

    private int _currentArrow = 0;

    public ArrowColor _setEnumArrowColor { get { return _setEnumArrowColor; } set { _arrowColor = value; } }

    private void Start()
    {
        SetArrowColor();
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

    private void ArrowEnumUpdate()  //to assign a Enum according to the direction of arrow after it stops rotating
    {
        switch (_currentArrow)
        {
            case 0:
                _arrowDirection = Direction.Up;
                break;
            case 1:
                _arrowDirection = Direction.Right;
                break;
            case 2:
                _arrowDirection = Direction.Down;
                break;
            case 3:
                _arrowDirection = Direction.Left;
                break;
            default:
                Debug.LogWarning("Arrow Enum Error");
                break;
        }
    }

    private void SetRandomArrowDir()
    {
        _currentArrow = Random.Range(0, 3);
        _arrowSR.sprite = _arrowSprites[_currentArrow];
        ArrowEnumUpdate();
    }

    private void SetArrowColor()
    {
        switch (_arrowColor)
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

    private IEnumerator CO_RotateArrow()
    {
        _interval = Random.Range(0.1f, 0.3f);
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

    
}
