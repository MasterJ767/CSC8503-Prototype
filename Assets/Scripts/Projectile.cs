using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask environment;
    private Vector3 startPosition;
    private Color colour;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Vector3.Distance(startPosition, transform.position) > 40) {
            Destroy(gameObject);
        }
    }

    public void Initialse(Vector3 startPosition, Color colour, float speed) {
        rb.velocity = transform.forward * speed;
        this.startPosition = startPosition;
        this.colour = colour;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Environment")) {
            World world = collision.gameObject.GetComponent<World>();
            world.ChangeCell(transform.position, colour);
            Destroy(gameObject);
        }
    }
}
