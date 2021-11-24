using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject loseTextObject;
    public GameObject winTextObject;
    private Rigidbody rb;
    public int count = 0;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        if(transform.localScale.x > 3.0f)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        movement.y = 0.0f;

        if(count == 15)
        {
            winTextObject.SetActive(true);
        }

        changeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if(gameObject.transform.localScale.x > other.gameObject.transform.localScale.x)
            {
                other.gameObject.SetActive(false);
                gameObject.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
                count++;
            }
            else 
            {
                gameObject.SetActive(false);
                //lose condition here
                loseTextObject.SetActive(true);
            }
        }
    }

    void changeColor()
    {
        if (transform.localScale.x <= 4.0f )
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.6792453f, 0.2870992f, 0 , 1);
        }
        else if (transform.localScale.x > 4.0f && transform.localScale.x < 5.0f)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0.04085064f, 1,1);
        }
        else if (transform.localScale.x > 5.0f )
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.01098371f, 0.3207547f, 0, 1);
        }
    }
}
