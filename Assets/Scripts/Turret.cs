﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    public Transform rotationTransform;
    private float rotationSpeed = 10f;
    public GameObject bullet;
    public Transform firePoint;

    [Header("Attributes")]  // Vary for each turrent

    private float range = 15f;
    private float fireRate = 15f;
    private float fireCountdown = 0f;

	// Use this for initialization
	void Start () {
        // Search a target right at the beginning & every 0.5 sec after
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    // Update is called once per frame
    void Update() {
        if (!target) return;
        Vector3 direction = target.position - transform.position;
        Quaternion lr = Quaternion.LookRotation(direction);
        Vector3 rotationAngle = Quaternion.Lerp(rotationTransform.rotation, lr, rotationSpeed * Time.deltaTime).eulerAngles;
        rotationTransform.rotation = Quaternion.Euler(0f, rotationAngle.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
	}

    // 
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && minDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    private void Shoot()
    {
        GameObject go = (GameObject) Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet b = go.GetComponent<Bullet>();
        if (b)
        {
            b.chase(target);
        }
    }

    // Draw the range of the turret on selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
