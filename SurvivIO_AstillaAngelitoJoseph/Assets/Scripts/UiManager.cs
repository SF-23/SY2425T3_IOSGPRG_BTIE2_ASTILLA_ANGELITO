using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] private TextMeshProUGUI _pistolAmmoTxt;
    [SerializeField] private TextMeshProUGUI _rifleAmmoTxt;
    [SerializeField] private TextMeshProUGUI _shottyAmmoTxt;
    [SerializeField] private TextMeshProUGUI _currentWeaponAmmoTxt;
    [SerializeField] private GameObject[] _weaponImg;

    [SerializeField] private TextMeshProUGUI _enemyCountTxt;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;

    public void ToggleWin(bool isActive)
    {
        _winPanel.SetActive(!isActive);
    }
    public void ToggleGameOver(bool isActive)
    {
        _gameOverPanel.SetActive(!isActive);
    }

    public void ToggleStart(bool isActive)
    {
        _startPanel.SetActive(!isActive);
    }

    public void EnemyCountUpdate(int enemyCount)
    {
        _enemyCountTxt.text = enemyCount.ToString();
    }

    public void PistolAmmoUpdate(int pistolAmmo)
    {
        _pistolAmmoTxt.text = pistolAmmo.ToString();
    }

    public void RifleAmmoUpdate(int rifleAmmo)
    {
        _rifleAmmoTxt.text = rifleAmmo.ToString();
    }

    public void ShottyAmmoUpdate(int shottyAmmo)
    {
        _shottyAmmoTxt.text = shottyAmmo.ToString();
    }

    public void ImageWeaponUpdate(int index, bool isActive)
    {
        _weaponImg[index].gameObject.SetActive(isActive);
    }

    public void CurrentWeaponAmmoUpdate(int currAmmo, int maxAmmo)
    {
        _currentWeaponAmmoTxt.text = currAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}
