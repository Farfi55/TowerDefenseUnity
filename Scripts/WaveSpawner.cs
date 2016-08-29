using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    
    public Transform enemyPrefab;
    public Transform startPoint;
    public GameObject spawnButton;
    public Text waveText;

    [HideInInspector]
    public static bool canSpawn = true;
        

    private float enemyCountDown;
    private float spawnRate = 1f;

    static private int waveNumber = 0;

    static public int Wave{ get { return waveNumber; } }

    public static int enemyToSpawn { get{ return _enemyToSpawn; } }

    private static int _enemyToSpawn;
    
    void Update()
    {
        if(canSpawn)
        {
            if (!spawnButton.activeInHierarchy)
            spawnButton.SetActive(true);
        }        
        else
        {
            if (spawnButton.activeInHierarchy)
                spawnButton.SetActive(false);

            if(_enemyToSpawn > 0)
            {
                enemyCountDown -= Time.deltaTime * GameSpeed.speed;

                if(enemyCountDown <= 0)
                {
                    SpawnEnemy();
                }

            }            

        }
    }


    public void SpawnWave()
    {
        if (canSpawn)
        {            
            canSpawn = false;
            spawnButton.SetActive(false);
            waveNumber++;
            waveText.gameObject.SetActive(true);

            if (waveText != null)
                waveText.text = "Wave: " + waveNumber;

            _enemyToSpawn = (int)(waveNumber * 2.5f) + 3;
            spawnRate = 1.5f - ((float)waveNumber * 0.01f);
            enemyCountDown = 0f;
        }
        else
        {
            Object[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length < 1 )
            {
                canSpawn = true;
                SpawnWave();
            }
        }

    }

    void SpawnEnemy()
    {
        if (_enemyToSpawn > 0)
        {
            Instantiate(enemyPrefab, startPoint.position, startPoint.rotation);
            enemyCountDown = spawnRate;
            _enemyToSpawn--;
        }
    }
    
}
