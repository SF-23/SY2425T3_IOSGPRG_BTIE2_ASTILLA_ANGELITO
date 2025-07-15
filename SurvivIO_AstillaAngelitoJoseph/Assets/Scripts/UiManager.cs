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
    [SerializeField] private GameObject[] _weaponImg;

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
}
