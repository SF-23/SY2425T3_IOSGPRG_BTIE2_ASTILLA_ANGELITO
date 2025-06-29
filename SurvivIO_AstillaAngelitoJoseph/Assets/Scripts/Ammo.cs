using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    pistol,
    rifle,
    shotty
}

public class Ammo : MonoBehaviour
{
    [SerializeField] public AmmoType _ammoType;
}
