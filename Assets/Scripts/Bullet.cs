using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    private float speed = 20f;
    public GameObject hitEffect;
	
	// Update is called once per frame
	void Update () {
		if (!target)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = target.position - transform.position;
        float distanceCurrFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceCurrFrame) // Will hit target this frame
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceCurrFrame, Space.World);
    }

    public void chase(Transform t)
    {
        target = t;
    }

    private void HitTarget()
    {
        GameObject effect = (GameObject) Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
