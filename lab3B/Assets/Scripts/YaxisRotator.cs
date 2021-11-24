using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaxisRotator : MonoBehaviour
{
    Transform rb;

    private void Awake()
    {
        rb = GetComponent<Transform>();
    }   
    private void FixedUpdate()
    {
        rb.Rotate(0.0f, 1.0f, 0.0f);

    }
}
