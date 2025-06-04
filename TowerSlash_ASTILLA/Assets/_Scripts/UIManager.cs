using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI txt_currScore;

    [SerializeField] private GameObject _panelPlayerSelect;

    public void ScoreUiUpdate(int score)
    {
        txt_currScore.text = score.ToString();
    }

    public void TogglePanelPlayerSelect(bool isActive)
    {
        _panelPlayerSelect.SetActive(isActive);
    }
}
