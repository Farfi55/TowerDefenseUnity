using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private Transform target;   

    public int Cost { get { return cost; } }

    [SerializeField]
    private const int cost = 250;

    [Header("Attributes")]
    public float range = 13f;
    public float fireRate = 1.25f;
    private float fireCountdown;


    [Header("Unity Setup Fields")]
    private float countDown = 0.5f;
    public float TurnSpeed;
    private Transform partToRotate;

    public GameObject bulletPrefab;

    public Transform firePoint;
    private ParticleSystem ShootEffect;
    private Light muzzleFlash;


    void Start()
    {        
        partToRotate = transform.FindChild("PartToRotate");
        ShootEffect = GetComponentInChildren<ParticleSystem>();
        muzzleFlash = GetComponentInChildren<Light>();
    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);   
    }


    void UpdateTarget()
    {
        if (target == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float shortestDistance = range;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    countDown = 0f;
                    shortestDistance = distanceToEnemy;
                    target = enemy.transform;
                }
            }
        }        
    }
    
    		
	void Update ()
    {

        if (target != null && Vector3.Distance(transform.position, target.position) > range)
            target = null;


        if (target == null)
        {
            countDown -= Time.deltaTime * GameSpeed.speed;
            if(countDown < 0f)
            {
                countDown = 0.5f;
                UpdateTarget();                
            }
        }

        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed * GameSpeed.speed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = fireRate;
            }
            fireCountdown -= Time.deltaTime * GameSpeed.speed;

            
        }

        if (ShootEffect.isPlaying)
            ShootEffect.playbackSpeed = GameSpeed.speed;


    }

    void Shoot()
    {
        ShootEffect.Play();
        GameObject bulletGameObj = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObj.GetComponent<Bullet>();
        flash();

        Invoke("flash", (fireRate / 6f / GameSpeed.speed) );

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void flash()
    {
        muzzleFlash.enabled = !muzzleFlash.enabled;
    }

}
