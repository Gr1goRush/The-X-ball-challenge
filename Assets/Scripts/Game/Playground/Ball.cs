using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : RandomizePositionRigidbody
{
    [Space]
    [SerializeField] private GameObject trail;

    [SerializeField] private float forceMultiplier = 5f, torgueMultiplier = 1f;
    [SerializeField] private Vector3 minBound, maxBound;

    public bool Throwing { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Throwing = false;
        _rigidbody.useGravity = false;
    }

    private void Update()
    {
        if (!Throwing)
        {
            return;
        }    

        if(AnyAxisIsLower(transform.position, minBound))
        {
            GameController.Instance.StopRound(false);
            return;
        }
        if (AnyAxisIsBigger(transform.position, maxBound))
        {
            GameController.Instance.StopRound(false);
            return;
        }
    }

    public void Throw(Vector3 force)
    {
        if (Throwing)
        {
            return;
        }

        Throwing = true;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(force * forceMultiplier, ForceMode.Impulse);

        float torgueDirection = Mathf.Sign(force.x);
        float torgueForce = force.magnitude;

        _rigidbody.AddTorque(new Vector3(0,0, -torgueDirection * torgueForce * torgueMultiplier), ForceMode.Impulse);

        trail.SetActive(false);
    }

    public void SetDefault()
    {
        Throwing = false;
        trail.SetActive(true);

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.position = defaultPosition;
        _rigidbody.rotation = Quaternion.identity;
    }

    public Vector3 GetPosition()
    {
        return _rigidbody.position;
    }

    private static bool AnyAxisIsLower(Vector3 a, Vector3 b)
    {
        if(a.x < b.x)
        {
            return true;
        }
        if (a.y < b.y)
        {
            return true;
        }
        if (a.z < b.z)
        {
            return true;
        }

        return false;
    }

    private static bool AnyAxisIsBigger(Vector3 a, Vector3 b)
    {
        if (a.x > b.x)
        {
            return true;
        }
        if (a.y > b.y)
        {
            return true;
        }
        if (a.z > b.z)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce();
    }

    private void OnTriggerEnter(Collider other)
    {
        Bounce();
    }

    private void Bounce()
    {
        GameSoundsController.Instance.PlayBounce();
    }
}
