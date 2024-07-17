using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private Vector3 _explosionPos;
    private float _explosionForce = 10.0f;
    private float _explosionRad = 3.0f;
    private float _upwardsModifier = 3.0f;
    private ForceMode _forceMode = ForceMode.Impulse;
    
    private void OnCollisionEnter(Collision collision)
    {
        _explosionPos = transform.position;
        if (collision.collider.CompareTag("Hazard"))
        {
            Collider[] colliders = Physics.OverlapSphere(_explosionPos, _explosionRad);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(_explosionForce, _explosionPos, _explosionRad, _upwardsModifier, _forceMode);
                }
            }
            
        }
    }
}
