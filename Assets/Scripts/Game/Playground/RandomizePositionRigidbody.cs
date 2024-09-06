using System.Collections;
using UnityEngine;

public class RandomizePositionRigidbody : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private Vector3 minPosition, maxPosition;

    protected Vector3 defaultPosition;

    protected virtual void Awake()
    {
        defaultPosition = _rigidbody.position;
    }

    public void SetRandomPosition()
    {
        _rigidbody.position = Utility.RandomVector3(minPosition, maxPosition);
    }

    public void SetDefaultPosition()
    {
        _rigidbody.position = defaultPosition;
    }
}