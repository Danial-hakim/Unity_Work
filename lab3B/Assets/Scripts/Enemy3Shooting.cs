using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Shooting : MonoBehaviour
{
    public GameObject m_BulletObject;

    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public Transform shotSpawn4;

    Transform rb;

    public float ProjetVelocity = 1f;



    public float timeOffset1;
    public float timeOffset2;


    private float m_FireTime = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Transform[] spawnlocation = { shotSpawn1, shotSpawn2, shotSpawn3, shotSpawn4 };

        if (Time.time > this.m_FireTime)
        {
            this.m_FireTime = Time.time + (float)Random.Range(timeOffset1, timeOffset2);
            for (int i = 0; i < 4; i++)
            {
                Instantiate(m_BulletObject, spawnlocation[i].position, spawnlocation[i].rotation);
            }            
        }        
    }
}
