using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{

    public float maxSpeed = 25f;
    public float sprintMultiplier = 2f;

    public float scrollSpeed = 2f;

    private Camera cam;
    float FOV;
    private float maxFOV = 90f;
    private float minFOV = 25f;

    private Transform parent;

    private Vector3 movementThisFrame;

    void Awake()
    {
        parent = transform.parent;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            movementThisFrame.x = Input.GetAxis("Horizontal");
            movementThisFrame.z = Input.GetAxis("Vertical");

            movementThisFrame *= Time.deltaTime * maxSpeed;
            if (Input.GetButton("Sprint"))
            {
                movementThisFrame *= sprintMultiplier;
            }
            transform.Translate(movementThisFrame, Space.World);
            movementThisFrame = Vector3.zero;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed * 1000;
            FOV = cam.fieldOfView + scroll;

            if (FOV < minFOV)
                FOV = minFOV;
            else if (FOV > maxFOV)
                FOV = maxFOV;

            cam.fieldOfView = FOV;
        }

    }
}
