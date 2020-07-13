using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public Transform partToRotate;
    public float rotateSpeed = 10f;

    [Header("Unity Setup Fields")]
    
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        // Invoke repeating function
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        // Get the array of the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;    // Transform is the gameObject
            // The head is responsible for shooting...?

        }else if(nearestEnemy == null)
        {
            nearestEnemy = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // It will be reset for the first time, and won't shoot until target is valid
        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }

        Debug.Log("Shoot");
    }

    private void OnDrawGizmosSelected() // Drawn when selected
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
