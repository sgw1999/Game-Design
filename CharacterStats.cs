using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
	public Slider healthbar;
    public Stats strength;
    public Stats fortitude;
    public Stats agility;
    public Stats charisma;
    public Stats faith;

    public int basehitPoints = 100;
    public int currenthitPoints;

	public static CharacterStats instance;

	void Start() //When player dies this runs, FIX THIS future Sebastian xoxoxo
    {
		if (fortitude.GetMod() >= 0)
		{
			basehitPoints += fortitude.GetMod();
		}
        currenthitPoints = basehitPoints;
		healthbar.value = CalcHealth();
    }

	void Awake()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
	}

	void Update()
    {
		healthbar.value = CalcHealth();
		if (currenthitPoints <= 0)
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= fortitude.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currenthitPoints -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage.");

    }

    float CalcHealth()
	{
		return currenthitPoints / basehitPoints;
	}

    public void Die()
    {
        Debug.Log(transform.name + "died.");
        transform.position = new Vector3(0, 0, 0);
        Start();
    }
}
