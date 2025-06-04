using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerLife;
    [SerializeField] private bool _isPlayerAlive = true;

    [Header("Dash Variables")]
    [SerializeField] private float _dashValue;
    [SerializeField] private Slider slider_dashGuage;
    [SerializeField] private bool _canDash = false;

    public float _getSetDashV { get { return _dashValue; } set { _dashValue = value; } }

    public int _setPlayerLife { get { return _playerLife; } set { _playerLife = value; } }

    private void Start()
    {
        dashSliderUpdate();
    }

    private void Update()
    {
        dashSliderUpdate();
        //DoDash();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.DoEnemyCollidePlayer();
            TakeDmg();
        }
    }

    private void TakeDmg()
    {
        _playerLife--;
        
        if( _playerLife <= 0 )
        {
            _isPlayerAlive = false;
            Destroy(gameObject);
        }
    }

    private void dashSliderUpdate()
    {
        _dashValue = Mathf.Clamp(_dashValue, 0, 1);
        slider_dashGuage.value = _dashValue;

        if (_dashValue >= 1)
        {
            _canDash = true;
        }
    }

    public void Button_DoDash() //For Button
    {
        if(_canDash)
        {
            Time.timeScale = 30.0f;
            StartCoroutine(CO_DrainDash(0.1f, 100f));
        }
    }

    private IEnumerator CO_DrainDash(float _dashV, float duration)
    {
        float deductionRate = _dashV / duration;
        while (_dashValue > 0) // Continue draining as long as the value is positive
        { 
            _dashValue -= deductionRate;
            yield return new WaitForSeconds(0.5f);
        }

        if (_dashValue <= 0)
        {
            _dashValue = 0;
            Time.timeScale = 1.0f;
            _canDash = false;
            yield break;
        }
    }
}
