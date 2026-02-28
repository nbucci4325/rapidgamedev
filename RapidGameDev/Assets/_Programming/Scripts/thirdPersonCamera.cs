using System.Collections;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    public float rotationSpeed;

    public Transform lookHere;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //rotate orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        //orientation.forward = viewDir.normalized;

        Vector3 dirToLookAt = lookHere.position - new Vector3(transform.position.x, lookHere.position.y, transform.position.z);
        orientation.forward = dirToLookAt.normalized;
        player.forward = dirToLookAt.normalized;
        playerObj.forward = dirToLookAt.normalized;


        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }
}
