using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float Radius;
    public Transform Target;

    public Vector3 Angles;

    void Start()
    {
    }

    void Update()
    {
        //float v = Input.GetAxis("Camera Vertical");
        //float h = Input.GetAxis("Camera Horizontal");

        float h = 0;
        float v = 0;
        if (Input.GetMouseButton(0))
        {
            h = Input.GetAxis("Mouse X") * 200 * Time.deltaTime;
            v = Input.GetAxis("Mouse Y") * -200 * Time.deltaTime;
        }

        Angles.x = Mathf.Clamp(Angles.x + v, -30, 89);
        Angles.y = Mathf.Repeat(Angles.y + h, 360);

        Quaternion rotation = Quaternion.Euler(Angles.x, Angles.y, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -Radius) + Target.position;

        transform.localRotation = rotation;
        transform.localPosition = position;
    }
}
