using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currHealth;

    public float _getSetCurrentHP
    {
        get { return currHealth; }
        set { currHealth = value; }
    }

    public float _getSetMaxHP
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    // Start is called before the first frame update
    private void Start()
    {
        currHealth = maxHealth;
    }

    public void ThisTakeDmg(float dmg)
    {
        currHealth -= dmg;
        currHealth = Mathf.Max(currHealth, 0);

        if (currHealth <= 0)
        {

            Destroy(gameObject);
        }
    }

    public void HealPlayer(float healAmt)
    {
        currHealth += healAmt;

        Mathf.Clamp(currHealth, currHealth, maxHealth);
    }

}
