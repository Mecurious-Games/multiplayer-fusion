using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool isActive = false;
    public float activationInterval = 5f; // Time interval between shield activations
    private float activationTimer = 0f; // Timer to track the time since the last activation

    private Renderer shieldRenderer; // Reference to the shield's renderer component
    public Material activeMaterial; // Material to use when the shield is active
    public Material inactiveMaterial; // Material to use when the shield is inactive
    private GameObject shieldFrame; // Reference to the shield's frame

    private void Awake()
    {
        shieldFrame = GameObject.Find("shieldFrame");
        shieldRenderer = shieldFrame.GetComponent<Renderer>();
        DeactivateShield(); // Deactivate the shield
    }

    private void Update()
    {
        // Check if the shield is currently inactive and the activation timer has exceeded the interval
        if (!isActive && activationTimer >= activationInterval)
        {
            ActivateShield(); // Activate the shield
            activationTimer = 0f; // Reset the activation timer
        }

        // Increment the activation timer
        activationTimer += Time.deltaTime;
    }

    private void ActivateShield()
    {
        isActive = true;
        shieldRenderer.material = activeMaterial; // Apply the active material to the shield
        Invoke("DeactivateShield", 3f); // Schedule the deactivation after 3 seconds
    }

    private void DeactivateShield()
    {
        isActive = false;
        shieldRenderer.material = inactiveMaterial; // Apply the inactive material to the shield
    }

    // If the player collides with the shield, collect it
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return; // Shield isn't active yet, ignore the collision

        var player = other.GetComponent<HealthAndScore>();
        if (player != null)
        {
            player.CollectShield();
            Destroy(gameObject); // Destroy the shield object
        }
    }
}