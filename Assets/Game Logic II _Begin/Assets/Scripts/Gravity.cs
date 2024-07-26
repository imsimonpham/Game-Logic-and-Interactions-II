using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float _pullingForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyLocalForce();
        }
    }

    void ApplyLocalForce()
    {
        ConstantForce constantForce = GetComponent<ConstantForce>();
        constantForce.force = new Vector3(0f, _pullingForce, 0f);
    }
}
