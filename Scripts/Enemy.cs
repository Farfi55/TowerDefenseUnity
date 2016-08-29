using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour {

    private float DefaultSpeed = 7.5f;
    private float speed;
    private int damage = 10;

    private int _moneyValue;
    public int MoneyValue { get { return _moneyValue; } }

    private Health health;

    private Transform target;
    private int wavePointIndex = 0;
    private Transform ToLookAt;
    private Quaternion dirToLookAt;
    private EnemyHealth enemyHealth;

    void Start()
    {
        tag = "Enemy";
        _moneyValue = 10;
        target = Waypoints.waypoints[wavePointIndex];
        health = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
        speed = DefaultSpeed + ((float)WaveSpawner.Wave * 0.75f);
        ToLookAt = transform.FindChild("RotateTo");
        ToLookAt.LookAt(target);
        dirToLookAt = ToLookAt.rotation;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < speed * Time.deltaTime * GameSpeed.speed)
        {
            transform.position = target.position;            
            GetNextWaypoint();
            ToLookAt.LookAt(target);
            dirToLookAt = ToLookAt.rotation;
        }
        else
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * GameSpeed.speed);        
        transform.rotation = Quaternion.Lerp(transform.rotation, dirToLookAt , 0.1f);
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.waypoints.Length - 1)
        {
            OnTarget();
            return;
        }
        wavePointIndex++;
        target = Waypoints.waypoints[wavePointIndex];
    }


    private void OnTarget()
    {
        if(health != null)
            health.Damage(damage);

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

        Destroy(gameObject);
        return;
        
    }
}
