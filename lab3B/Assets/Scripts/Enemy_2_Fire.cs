using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Fire : MonoBehaviour
{
    public GameObject m_BulletObject;

    private Transform shotSpawn;

    public Transform North;
    public Transform South;
    public Transform East;
    public Transform West;

    public float timeOffset1;
    public float timeOffset2;


    private float m_FireTime = 0.0f;

    private float currentAngle;
    private void Start()
    {
        currentAngle = gameObject.transform.rotation.eulerAngles.y;
    }
    private void Update()
    {
        checkDirection();
        if (Time.time > this.m_FireTime)
        {
            this.m_FireTime = Time.time + (float)Random.Range(timeOffset1, timeOffset2);
            Instantiate(m_BulletObject, shotSpawn.position, shotSpawn.rotation);
        }
    }

    void checkDirection()
    {
        if(currentAngle == 0)
        {
            shotSpawn = South;
        }
        else if(currentAngle == 90)
        {
            shotSpawn = East;
        }
        else if (currentAngle == 180)
        {
            shotSpawn = North;
        }
        else if (currentAngle == 270)
        {
            shotSpawn = West;
        }
    }
}
