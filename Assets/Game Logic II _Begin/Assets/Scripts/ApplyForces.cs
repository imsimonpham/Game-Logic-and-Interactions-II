using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForces : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _forceValue;
    [SerializeField] private float _torqueValue;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddRelativeForce(0f, _forceValue, _forceValue, ForceMode.Impulse);
        _rb.AddRelativeTorque(transform.right * _torqueValue, ForceMode.Force);
    }

    void FixedUpdate()
    {
        /*_rb.AddRelativeForce(0f, 0f, _forceValue, ForceMode.Impulse);
        _rb.AddRelativeTorque(transform.up * _torqueValue, ForceMode.Force);*/
    }
}
