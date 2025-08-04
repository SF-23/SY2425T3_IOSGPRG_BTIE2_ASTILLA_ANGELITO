using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currHealth;
    [SerializeField] private bool _isAlive;

    public float GetCurrentHP()
    {
        return currHealth;
    }

    public float SetCurrentHP(float value)
    {
        currHealth = value;
        return currHealth;
    }

    public bool GetIsAlive
    { 
        get { return _isAlive; } 
    }

    // Start is called before the first frame update
    private void Awake()
    {
        currHealth = maxHealth;
        _isAlive = true;
    }

    public void ThisTakeDmg(float dmg)
    {
        currHealth -= dmg;
        currHealth = Mathf.Max(currHealth, 0);

        if (currHealth <= 0)
        {
            _isAlive = false;
        }
    }

    public void HealPlayer(float healAmt)
    {
        currHealth += healAmt;

        Mathf.Clamp(currHealth, currHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currHealth = maxHealth;
    }

}
