using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float cooldown = 0.6f;
    public Color teamColour;
    private float timer;


    private void Update() {
        timer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && timer <= 0) {
            timer = cooldown;
            Transform cameraTransform = GetComponent<PlayerMovement>().cameraController.transform;
            GameObject bulletGO = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(new Vector3(transform.forward.x, cameraTransform.forward.y, transform.forward.z)));
            bulletGO.GetComponent<Projectile>().Initialse(shootPoint.position, teamColour, 17.5f);
        }
    }
}
