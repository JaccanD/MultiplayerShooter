using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //SerializedFields
    [SerializeField][Range(0.1f, 5)] private float mouseSensitivity;
    [SerializeField][Range(40, 100)] private int maxRotationX;
    [SerializeField][Range(-40, -100)] private int minRotationX;
    //Private fields
    private float rotationX = 0;
    private float rotationY = 0;
    private GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        GetRotation();
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        parent.transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
    void GetRotation()
    {
        Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        rotationX -= mouse.y * mouseSensitivity;
        rotationY += mouse.x * mouseSensitivity;
    }

}
