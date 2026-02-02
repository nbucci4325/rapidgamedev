using UnityEngine;

public class moveCam : MonoBehaviour
{
    public Transform cameraPosition;
    
    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
