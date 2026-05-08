using UnityEngine;

public class Focus : MonoBehaviour
{
    public Transform lockPoint;

    private PlayerLook playerLook;

    void Start()
    {
        if (lockPoint == null)
            lockPoint = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered focus area");
            playerLook = other.GetComponent<PlayerLook>();
            if (playerLook != null)
            {
                playerLook.SetTarget(lockPoint);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerLook != null)
            {
                playerLook.ClearTarget(lockPoint);
            }
        }
    }
}