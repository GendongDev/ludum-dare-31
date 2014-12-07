using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour
{
    public PathNode Next;
    public float WaitTime;

    void OnDrawGizmos()
    {
        if (Next != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Next.transform.position);
        }
    }


}
