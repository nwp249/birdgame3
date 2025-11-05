using UnityEngine;

public class WindZone : MonoBehaviour
{
    public Vector3 windDirection = Vector3.right;
    public float windStrength = 10f;
    
    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(windDirection.normalized * windStrength, ForceMode.Force);
        }
    }
    
    void OnDrawGizmos()
    {
        // Visualize wind zone in editor
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, windDirection.normalized * 10);
    }
}
