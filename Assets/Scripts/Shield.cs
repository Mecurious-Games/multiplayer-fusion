using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return; // Shield already collected

        var player = other.GetComponent<HealthAndScore>();
        if (player != null)
        {
            player.CollectShield();
            isCollected = true;
            Destroy(gameObject); // Destroy the shield object
        }
    }
}