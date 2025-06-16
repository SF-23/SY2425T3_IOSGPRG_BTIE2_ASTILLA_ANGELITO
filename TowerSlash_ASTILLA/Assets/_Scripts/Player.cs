using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerCurrLife;
    [SerializeField] private int _playerMaxLife;
    [SerializeField] private bool _isPlayerAlive = true;

    [Header("Dash Variables")]
    [SerializeField] private float _dashValue;
    [SerializeField] private Slider slider_dashGuage;
    [SerializeField] private bool _canDash = false;
    [SerializeField] private bool _isDashing = false;

    public float _GetSetDashV { get { return _dashValue; } set { _dashValue = value; } }

    public int _GetSetPlayerMaxLife { get { return _playerMaxLife; } set {_playerMaxLife = value; } }

    private void Start()
    {
        LifeReset();
        DashSliderUpdate();
    }

    public void DashSliderUpdate()
    {
        _dashValue = Mathf.Clamp(_dashValue, 0, 1);
        slider_dashGuage.value = _dashValue;

        if (_dashValue >= 1)
        {
            _canDash = true;
            UIManager.Instance.ToggleButtonDash(true);
        }
    }

    public void EnemyDeteced(Enemy enemy)
    {
       enemy._SetCanSwipe = true;
    }

    public void TakeDmg()
    {
        _playerCurrLife--;
        UIManager.Instance.playerlifeUiUpdate(_playerCurrLife);

        if (_playerCurrLife <= 0)
        {
            _isPlayerAlive = false;
            UIManager.Instance.ToggleGameOverPanel(true);
            this.gameObject.SetActive(false);
        }
    }

    public void LifeReset()
    {
        _playerCurrLife = _playerMaxLife;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.playerlifeUiUpdate(_playerCurrLife);
        }
    }

    public void Button_DoDash() //For Button
    {
        if (_canDash)
        {
            _isDashing = true;
            Time.timeScale = 30.0f;
            StartCoroutine(CO_DrainDash(0.1f, 5f));
            UIManager.Instance.ToggleButtonDash(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if(_isDashing)
            {
                enemy.DoEnemyCollidePlayer(_isDashing);
            }
            else
            {
                TakeDmg();
                enemy.DoEnemyCollidePlayer(!_isDashing);
            }
        }
    }

    private IEnumerator CO_DrainDash(float _dashV, float duration)
    {
        float deductionRate = _dashV / duration;
        // Continue draining as long as the value is positive
        while (_dashValue > 0) 
        { 
            _dashValue -= deductionRate * Time.deltaTime;
            DashSliderUpdate();
            yield return new WaitForSeconds(0.5f);
        }

        if (_dashValue <= 0)
        {
            _dashValue = 0;
            Time.timeScale = 1.0f;
            _canDash = false;
            _isDashing = false;
            yield break;
        }
    }
}
