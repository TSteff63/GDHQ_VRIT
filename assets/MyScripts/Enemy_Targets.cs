using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Targets : MonoBehaviour
{
    [SerializeField]
    private int health = 1;

    public void TakeDamage(int newValue)
    {
        Debug.Log("Takind damage = " + newValue);
        health -= newValue;

        if(health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("dead");

        this.transform.eulerAngles = new Vector3((transform.position.x + 90), transform.position.y, transform.position.z);
    }
}
