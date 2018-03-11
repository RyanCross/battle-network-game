using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanCross.BattleNetworkGame
{
    public class Weapon : MonoBehaviour
    {

        public float fireRate = 0f;
        public float damage = 5f;
        public LayerMask whatToHit; // needs to be set to opposite player in inspector or this wont hit the right player.
        public PlayerType playerShooting { get; private set; }

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
        void Start()
        {
            playerShooting = GetComponentInParent<Player>().playerType;
        }

        // Update is called once per frame
        void Update()
        {
            if (fireRate == 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                }
            }
        }

        void Shoot()
        {
            Debug.Log("Shoot button pressed");
            Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
            Vector3 shootDirection = (playerShooting == PlayerType.Player1) ? Vector3.forward : Vector3.back;
            Color debugColor = (playerShooting == PlayerType.Player1) ? Color.cyan : Color.yellow;

            RaycastHit hit;
            Physics.Raycast(firePointPosition, shootDirection, out hit, 100, whatToHit);
            Debug.DrawLine(firePointPosition, shootDirection * 100, debugColor);
        }
    }
}
