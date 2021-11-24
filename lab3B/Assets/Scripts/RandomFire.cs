using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFire : MonoBehaviour
{
    public GameObject m_BulletObject;

    public Transform shotSpawn;

    public float timeOffset1;
    public float timeOffset2;


    private float m_FireTime = 0.0f;

    private void Update()
    {
        if (Time.time > this.m_FireTime)
        {
            this.m_FireTime = Time.time + (float)Random.Range(timeOffset1, timeOffset2);
            Instantiate(m_BulletObject, shotSpawn.position, shotSpawn.rotation);
        }        
    }
}
