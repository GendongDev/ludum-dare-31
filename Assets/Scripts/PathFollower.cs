using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    public PathNode Target;
    public float Speed;

    float waitTimer;
    bool frozen;
    bool paused;

    void FixedUpdate()
    {
        waitTimer = Mathf.Max(waitTimer - /*Time.deltaTime*/ Time.fixedDeltaTime, 0);
        if(waitTimer <= 0 && !frozen && !paused) 
        {
            MoveAlongPath(Speed * /*Time.deltaTime*/ Time.fixedDeltaTime);
        }
    }

    void MoveAlongPath(float distance)
    {
        Vector3 toTarget = Target.transform.position - transform.position;
        float toTargetDistance = toTarget.magnitude;

        if (distance >= toTargetDistance)
        {
            transform.position = Target.transform.position;

            if(Target.WaitTime != 0)
            {
                waitTimer = Target.WaitTime;
                Target = Target.Next;
                paused = (waitTimer < 0);
            }
            else
            {
                Target = Target.Next;
                MoveAlongPath(distance - toTargetDistance);
            }
        }
        else
        {
            Vector3 toTargetDirection = toTarget.normalized;
            //transform.position = transform.position + (toTargetDirection * distance);
            rigidbody.MovePosition(transform.position + (toTargetDirection * distance));
        }
    }

    void Freeze()
    {
        frozen = true;
    }

    public void MoveToNextNode()
    {
        paused = false;
    }

}
