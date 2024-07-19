using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private AirmailPackage _airmailPackagePrefab;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _container;
    [SerializeField] private LabComplete _labComplete;

    [SerializeField] private SimulatedPhysics _simulatedPhysics;

    private void FixedUpdate()
    {
        _simulatedPhysics.SimulatedTrajectory(_airmailPackagePrefab, transform.position, transform.forward * _force);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var spawned = Instantiate(_airmailPackagePrefab, transform.position, transform.rotation);
            spawned.transform.parent = _container.transform.root;
            spawned.Init(transform.forward * _force);
            
            _labComplete.CheckCriteria();
        }
    }
}
