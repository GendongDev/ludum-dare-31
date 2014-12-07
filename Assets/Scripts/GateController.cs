using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathFollower))]
public class GateController : MonoBehaviour
{
    PathFollower pf;

    void Start() 
    {
        pf = GetComponent<PathFollower>();
    }

    void SwitchPressed(GameObject go)
    {
        pf.MoveToNextNode();
    }
}
