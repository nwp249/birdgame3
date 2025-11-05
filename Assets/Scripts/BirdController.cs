using UnityEngine;

public class BirdController : MonoBehaviour
{
    [Header("Flight Settings")]
    public float flapForce = 15f;
    public float glideSpeed = 10f;
    public float turnSpeed = 100f;
    public float maxSpeed = 30f;
    public float stamina = 100f;
    public float staminaRegenRate = 10f;
    public float flapStaminaCost = 5f;
    
    [Header("Physics")]
    public float dragInAir = 0.5f;
    public float liftMultiplier = 2f;
    
    private Rigidbody rb;
    private float currentStamina;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = stamina;
        rb.linearDamping = dragInAir;
    }
    
public float maxPitchAngle = 45f;  // NEW
public float autoLevelSpeed = 2f;   // NEW

void Update()
{
    float pitch = Input.GetAxis("Vertical");
    float yaw = Input.GetAxis("Horizontal");
    bool flap = Input.GetKey(KeyCode.Space);
    
    // Turning (Y-axis only)
    transform.Rotate(0, yaw * turnSpeed * Time.deltaTime, 0, Space.World);
    
    // Get current pitch angle
    float currentPitch = transform.eulerAngles.x;
    if (currentPitch > 180) currentPitch -= 360;
    
    // Calculate target pitch
    float targetPitch = currentPitch + (-pitch * turnSpeed * 0.5f * Time.deltaTime);
    targetPitch = Mathf.Clamp(targetPitch, -maxPitchAngle, maxPitchAngle);
    
    // Auto-level when no pitch input
    if (Mathf.Abs(pitch) < 0.1f)
    {
        targetPitch = Mathf.Lerp(currentPitch, 0, autoLevelSpeed * Time.deltaTime);
    }
    
    // Apply pitch, keep yaw, zero out roll
    Vector3 currentRotation = transform.eulerAngles;
    transform.rotation = Quaternion.Euler(targetPitch, currentRotation.y, 0);
    
    // ... rest of flapping and movement code
        
        // Flapping (upward force)
        if (flap && currentStamina > 0)
        {
            rb.AddForce(transform.up * flapForce, ForceMode.Acceleration);
            currentStamina -= flapStaminaCost * Time.deltaTime;
        }
        else
        {
            // Regenerate stamina when not flapping
            currentStamina = Mathf.Min(stamina, currentStamina + staminaRegenRate * Time.deltaTime);
        }
        
        // Forward glide force
        rb.AddForce(transform.forward * glideSpeed, ForceMode.Acceleration);
        
        // Generate lift based on forward speed (like a real wing)
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);
        float lift = forwardSpeed * liftMultiplier;
        rb.AddForce(Vector3.up * lift, ForceMode.Force);
        
        // Speed limit
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        
        // Prevent backward flight
        if (forwardSpeed < 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
    
    void OnGUI()
    {
        // Simple stamina bar
        GUI.Box(new Rect(10, 10, 200, 30), $"Stamina: {currentStamina:F0}/{stamina}");
        GUI.Box(new Rect(10, 50, 200, 30), $"Speed: {rb.linearVelocity.magnitude:F1}");
        GUI.Box(new Rect(10, 90, 200, 30), $"Altitude: {transform.position.y:F1}m");
    }
}