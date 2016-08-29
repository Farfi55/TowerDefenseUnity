using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    private EnemyHealth health;

    private float damage = 3.5f;
    public float speed = 80f;
    public GameObject bullectHitEffect;

    Vector3 dir;
    float distanceThisFrame;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = Random.Range((_damage - _damage * 0.1f), (_damage + _damage * 0.1f));
    }
    
	void Start ()
    {
        speed += (float)WaveSpawner.Wave;
        health = target.GetComponent<EnemyHealth>();
    }
	
	void Update ()
    {
        if (target == null)
        {
            dir.y = -9.81f / 10;
            distanceThisFrame = speed * Time.deltaTime * GameSpeed.speed;

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            Destroy(gameObject, 1.5f * GameSpeed.speed);
            return;
        }

        dir = target.position - transform.position;
        dir.y = 0;
        distanceThisFrame = speed * Time.deltaTime * GameSpeed.speed;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

    
    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(bullectHitEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f * GameSpeed.speed);

        health.Damage(damage);        

        Destroy(gameObject);
        return;
    }


}
