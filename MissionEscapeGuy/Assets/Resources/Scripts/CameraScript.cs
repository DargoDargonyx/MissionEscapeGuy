using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform objectOfInterest;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectOfInterest);
    }
}
