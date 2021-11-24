using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_OneController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public float delta;

    public int health ;

    public GameObject EnemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.position = new Vector3(-1.5f, 0.0f, 17.0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 v = rb.position;
        v.x += delta * Mathf.Sin(Time.time * speed);
        rb.velocity = transform.forward * speed;
        rb.transform.position = v;
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
