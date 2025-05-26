using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite[] _arrowSprites;
    [SerializeField] private SpriteRenderer _arrowSR;
    [SerializeField] private GameObject arrowBG;

    [Header("Options")]
    [SerializeField] private float _interval;
    [SerializeField] private bool _isPlayerNear;

    private int _currentArrow = 0;

    private void Start()
    {
        StartCoroutine(CO_RotateArrow());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StopCoroutine(CO_RotateArrow());
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
            arrowBG.SetActive(true);
            // show black box & stop rotating
        }
    }
}
