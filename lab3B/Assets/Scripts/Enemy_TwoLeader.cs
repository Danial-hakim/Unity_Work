using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TwoLeader : MonoBehaviour
{
    // Start is called before the first frame update

    public float currentRotation;

    private float speed;

    Rigidbody rb;

    private float movementX;

    public GameObject m_BulletObject;

    private Transform shotSpawn;

    public Transform North;
    public Transform South;
    public Transform East;
    public Transform West;

    public int health;

    bool fireNow;

    public GameObject EnemyExplosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentRotation = 0;
        speed = 0.25f;
        movementX = 0.5f;
        fireNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRotation > 360)
        {
            currentRotation = 0;
        }

        checkDirection();
        
    }


    private void FixedUpdate()
    {
        currentRotation = currentRotation + 75 * Time.fixedDeltaTime;

        gameObject.transform.rotation = Quaternion.Euler(0, currentRotation, 0);

        if (fireNow)
        {
            Instantiate(m_BulletObject, shotSpawn.position, shotSpawn.rotation);
            fireNow = false;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * speed);
    }
    
    void checkDirection()
    {
        if (currentRotation >= 360.0f && currentRotation <= 1.0f)
        {
            shotSpawn = South;
            fireNow = true;
        }
        else if (currentRotation >= 90.0f && currentRotation <= 91.0f)
        {
            shotSpawn = East;
            fireNow = true;
        }
        else if (currentRotation >= 180.0f && currentRotation <= 181.0f)
        {
            shotSpawn = North;
            fireNow = true;
        }
        else if (currentRotation >= 270.0f && currentRotation <= 271.0f)
        {
            shotSpawn = West;
            fireNow = true;
        }
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
