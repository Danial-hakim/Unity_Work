using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{
    private float movementX;
    public float speed;

    Rigidbody rb;

    public Boundary boundary;
    public float tilt;

    private float nextFire;
    public float fireRate;

    public GameObject shot;
    public Transform shotSpawn;
    bool isBoosted;

    private bool isInvincible;
    int health;
    public float invincibleDuration;
    public float invincibleDeltaTime;

    public GameObject model;

    public GameObject playerExplosion;

    public AudioSource audioData;

    public GameObject GameOverText;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isBoosted = false;
        isInvincible = false;
        health = 3;

        GameOverText.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movmentVector = movementValue.Get<Vector2>();

        movementX = movmentVector.x;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * speed);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            0.0f);

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioData.Play(0);

        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp"))
        {
            isBoosted = true;
            Destroy(other.gameObject);
            StartCoroutine(stayBoosted());
        }

        if(other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Enemy1"))
        {
            LoseHealth(1);
        }
    }
    IEnumerator stayBoosted()
    {
        if (isBoosted)
        {
            fireRate = 0.0f;
        }

        yield return new WaitForSeconds(3.0f);
        isBoosted = false;
        fireRate = 0.5f;
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;

        for (float i = 0; i < invincibleDuration; i += invincibleDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }

        ScaleModelTo(Vector3.one);
        isInvincible = false;
    }

    public void LoseHealth(int amount)
    {
        if (isInvincible) return;

        health -= amount;
        GameController.instance.playerTakeDamage();

        // The player died
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player is dead");
            gameObject.SetActive(false);
            Instantiate(playerExplosion, transform.position, transform.rotation);
            GameOverText.SetActive(true);

            return;
        }

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
}
