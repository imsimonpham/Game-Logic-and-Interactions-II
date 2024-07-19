using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirmailPackage : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public void Init(Vector3 velocity)
    {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }
}
