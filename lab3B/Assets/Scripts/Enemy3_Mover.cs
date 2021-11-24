using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Mover : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    private float movementX;

    public int health;

    public GameObject EnemyExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementX = 0.5f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(-movementX, 0.0f, 0.0f);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            GameController.instance.playerAddScore();
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
                Instantiate(EnemyExplosion, transform.position, transform.rotation);

            }
        }
    }
}
