using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Enemy enemy;
    public RectTransform healthBar;
    public Money money;

    private int killValue;

    private bool _alive;
    public bool alive { get { return _alive; } }
    
    private float MaxHealth = 9.5f;

    private float health;

    public float Health
    {
        get{ return health; }
    }

    private void OnHealthChange()
    {
        if (health > MaxHealth)
            health = MaxHealth;
        else if (health < 0)
            health = 0;

        healthBar.localScale = new Vector3 ( health / MaxHealth, 1, 1);
        if (health <= 0)
        {
            death();
        }
        else if (health > 0 && !_alive)
            _alive = true;
    }


    private void death()
    {
        if (WaveSpawner.enemyToSpawn <= 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            WaveSpawner.canSpawn = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy.activeInHierarchy && enemy != gameObject)
                    WaveSpawner.canSpawn = false;
            }
        }

        if(money != null)
            money.Add(killValue);

        Destroy(gameObject);
        return;
    }


    public void Damage(float value)
    {
        if (value > 0)
        {
            health -= value;
            OnHealthChange();
        }
    }


    void Start()
    {
        MaxHealth = 10f + (WaveSpawner.Wave * 0.75f);
        health = MaxHealth;
        _alive = true;
        money = GameObject.FindGameObjectWithTag("GameController").GetComponent<Money>();
        enemy = GetComponent<Enemy>();
        killValue = enemy.MoneyValue;
    }
	
}
