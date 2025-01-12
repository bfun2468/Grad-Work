using System;
using System.Collections;
using UnityEngine;

public class FootIkTarget : MonoBehaviour
{
    public Vector3 _previousLocation;
    private float _speed = 4f;
    public AnimationCurve _yMovementCurve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previousLocation = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.position = transform.position + Vector3.forward * _speed * Time.deltaTime;
            Debug.Log("forward");
        }
        if (Input.GetKey(KeyCode.R))
        {
            ResetMovement();
            this.transform.localPosition = _previousLocation;
            Debug.Log("Reset");
        }

    }

    private void ResetMovement()
    {
        
    }
}
