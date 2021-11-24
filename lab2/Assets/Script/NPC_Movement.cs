using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    public Rigidbody NPCrb;

    public float speed = 1.0f;

    public float movementX = 0;
    public float movementZ = 0;

    public Vector3 NPCmove;

    // Start is called before the first frame update
    void Awake()
    {
        movementX = Random.Range(-180, 180);
        movementZ= Random.Range(-180, 180);
        NPCmove = new Vector3(movementX, 0, movementZ);
        NPCmove.Normalize();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(NPCmove.magnitude <= speed)
        {
            NPCrb.AddForce(NPCmove * speed);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("NPC"))
        {
            NPCmove = Vector3.Reflect(NPCmove,NPCmove);
        }
    }
}
