using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotate the coin
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is tagged correctly
        {
            GameManager.Instance.IncrementScore(); // Update score
            Destroy(gameObject); // Remove coin
        }
    }
}
