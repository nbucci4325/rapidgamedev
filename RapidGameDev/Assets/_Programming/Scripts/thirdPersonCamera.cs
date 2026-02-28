using System.Collections;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    //public Transform lookAt;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //if(inputDir != Vector3.zero)
        //    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        //Vector3 lookDir = lookAt.position - new Vector3(transform.position.x, lookAt.position.y, transform.position.z);
        //orientation.forward = lookDir.normalized;

        playerObj.forward = viewDir.normalized;
    }
}
