using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = Camera.main.transform;
    }

    void Update()
    {
       transform.LookAt( transform.position + target.transform.rotation * Vector3.forward, target.transform.rotation * Vector3.up);
    }
}
