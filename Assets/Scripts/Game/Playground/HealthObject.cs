using System.Collections;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameController.Instance.AddHealth(1);
            gameObject.SetActive(false);
        }
    }
}