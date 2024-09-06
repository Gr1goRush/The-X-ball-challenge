using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawnSystem : MonoBehaviour
{
    [SerializeField] private Vector3 minPosition, maxPosition;
    [SerializeField] private HealthObject healthObject;

    public void Spawn()
    {
        healthObject.transform.position = GetRandomPosition();
        healthObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        healthObject.gameObject.SetActive(false);
    }

    private Vector3 GetRandomPosition()
    {
        return Utility.RandomVector3(minPosition, maxPosition);
    }
}
