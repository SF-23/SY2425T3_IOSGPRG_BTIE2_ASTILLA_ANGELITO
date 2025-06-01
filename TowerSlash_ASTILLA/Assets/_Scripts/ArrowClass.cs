using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite[] _arrowSprites;
    [SerializeField] private SpriteRenderer _arrowSR;
    [SerializeField] private GameObject _arrowBG;
    [SerializeField] private Direction enum_arrowDirection;

    public Direction _getEnumArrowDir { get { return enum_arrowDirection; } }

    [Header("Options")]
    [SerializeField] private float _interval;
    [SerializeField] public bool _isPlayerNear;
    [SerializeField] private bool _isColorRed = false;

    public bool _getIsColorRed { get { return _isColorRed; } }

    public bool _getIsPlayerNear { get { return _isPlayerNear; } }

    private int _currentArrow = 0;

    private void Start()
    {
        StartCoroutine(CO_RotateArrow());
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
            ChangeArrowColor();
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

    private void ChangeArrowColor()
    {
        int randomIndex = Random.Range(0, 20);

        if (randomIndex % 2 == 0)
        {
            _arrowSR.color = Color.green;
        }
        else
        {
            _arrowSR.color = Color.red;
            _isColorRed = true;
        }
    }
}
