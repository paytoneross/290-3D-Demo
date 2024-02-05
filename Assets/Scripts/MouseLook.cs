using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private bool isInverted = true;
    public enum RotationAxes
    {
        MouseXAndY = 0, MouseX = 1, MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 5.0f;
    public float sensitivityVert = 5.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // horizontal rotation here
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor , 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // vertical rotation here
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
        else
        {
            // both horizontal and vertical rotation here
            if (isInverted)
            {
                verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert * -1;
            }
            else
            {
                verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            }

            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);           
        }
    }
}
