using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalHitTriggerOrientation
{
    Vertical, Horizontal
}

public class GoalHitTrigger : MonoBehaviour
{
    [SerializeField] private GoalHitTriggerOrientation orientation;
    [SerializeField] private Collider leftBorder, rightBorder, topBorder, bottomBorder;

    private bool ballIsTriggering = false, hit = false;
    private Transform triggeringCollider;

    void Update()
    {
        if (ballIsTriggering && !hit)
        {
            Vector3 pos = triggeringCollider.position;

            if(pos.x < leftBorder.transform.position.x)
            {
                return;
            }
            if (pos.x > rightBorder.transform.position.x)
            {
                return;
            }

            if (bottomBorder != null)
            {
                if (orientation == GoalHitTriggerOrientation.Horizontal && pos.z < bottomBorder.transform.position.z)
                {
                    return;
                }

                if (orientation == GoalHitTriggerOrientation.Vertical && pos.y < bottomBorder.transform.position.y)
                {
                    return;
                }
            }

            if (orientation == GoalHitTriggerOrientation.Horizontal && pos.z > topBorder.transform.position.z)
            {
                return;
            }
            if (orientation == GoalHitTriggerOrientation.Vertical && pos.y > topBorder.transform.position.y)
            {
                return;
            }

            if (orientation == GoalHitTriggerOrientation.Horizontal && pos.y > transform.position.y)
            {
                return;
            }
            if (orientation == GoalHitTriggerOrientation.Vertical && pos.z < transform.position.z)
            {
                return;
            }

            hit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballIsTriggering = true;
            triggeringCollider = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (hit)
            {
                GameController.Instance.StopRound(true);
            }

            ballIsTriggering = false;
            hit = false;
        }
    }
}
