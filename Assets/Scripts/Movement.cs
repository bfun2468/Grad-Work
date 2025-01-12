using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private List<FootIkTarget> legs = new List<FootIkTarget>();

    [SerializeField] private Transform skeleton;
    [SerializeField] private Transform rig;

    [SerializeField] private AnimationCurve _yMovementCurve;
    private float _speed = 4;

    void Start()
    {
        FootIkTarget[] targets = GetComponentsInChildren<FootIkTarget>();
        foreach (FootIkTarget target1 in targets)
        {
            legs.Add(target1);
        }
        foreach (FootIkTarget target in legs)
        {
            target._yMovementCurve = _yMovementCurve;
        }

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = transform.position + Vector3.forward * _speed * Time.deltaTime;
            Debug.Log("Forward");
        }
    }
}
