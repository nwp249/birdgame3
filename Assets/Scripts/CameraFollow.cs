using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 3f;
    public float lookAheadDistance = 3f;
    public float rotationSpeed = 5f;
    
    private Vector3 velocity = Vector3.zero;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Desired position behind and above the bird
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        
        // Use SmoothDamp instead of Lerp (much smoother!)
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position, 
            desiredPosition, 
            ref velocity, 
            1f / smoothSpeed
        );
        transform.position = smoothedPosition;
        
        // Look slightly ahead of the bird with smooth rotation
        Vector3 lookAtPoint = target.position + target.forward * lookAheadDistance;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
