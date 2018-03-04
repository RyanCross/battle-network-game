using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0f;
    public float damage = 5f;
    public LayerMask whatToHit;

    public float timeToFire = 0f;
    Transform firePoint;

    private void Awake()
    {
        firePoint = this.GetComponent<Transform>();
        if (firePoint == null)
        {
            Debug.LogError("Firepoint transform is null");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
	}

    void Shoot() {
        Debug.Log("Shoot button pressed");
        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
        RaycastHit hit;
        Physics.Raycast(firePointPosition, Vector3.forward, out hit, 100, whatToHit);
        Debug.DrawLine(firePointPosition, Vector3.forward * 100, Color.cyan);
    }
}
