using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI txt_currScore;
    [SerializeField] private GameObject _panelPlayerSelect;
    [SerializeField] private GameObject _panelGameOverScreen;
    [SerializeField] private GameObject _buttonDash;
    [SerializeField] private TextMeshProUGUI txt_playerLife;

    public void ScoreUiUpdate(int score)
    {
        txt_currScore.text = score.ToString();
    }

    public void TogglePanelPlayerSelect(bool isActive)
    {
        _panelPlayerSelect.SetActive(isActive);
    }

    public void ToggleGameOverPanel(bool isActive)
    {
        _panelGameOverScreen.SetActive(isActive);
        Time.timeScale = 0f;
    }

    public void ToggleButtonDash(bool isActive) 
    { 
        _buttonDash.SetActive(isActive);
    }

    public void playerlifeUiUpdate(int playerLife)
    {
        txt_playerLife.text = playerLife.ToString();   
    }
}
