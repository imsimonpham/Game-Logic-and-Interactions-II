using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheelColliders;   

    private void FixedUpdate()
    {
        foreach(var wheelCollider in _wheelColliders)
        {
            Transform wheel = wheelCollider.transform.GetChild(0);
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheel.position = pos;
            wheel.rotation = rot;
        }
    }
}
