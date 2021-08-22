using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    [SerializeField] private Transform objectToRotateAround;

    private Camera mainCam;
    private Vector3 mousePosition=Vector3.zero;

    private void Start()
    {
        mainCam=Camera.main;
    }

    void RotateAroundObject()
    {
        Vector3 currentVector = transform.position - objectToRotateAround.position;
        float currentAngle = Vector3.SignedAngle(Vector3.right, currentVector, Vector3.up);
        if (currentAngle < 0) currentAngle += 360;

        Vector3 targetVector = mousePosition - objectToRotateAround.position;
        targetVector = new Vector3(targetVector.x, 0, targetVector.z);
        float targetAngle=Vector3.SignedAngle(Vector3.right, targetVector, Vector3.up);
        if (targetAngle < 0) targetAngle += 360;

        float finalAngle = targetAngle - currentAngle;
        transform.RotateAround(objectToRotateAround.position, Vector3.up, finalAngle);
    }

    private void OnMouseDrag()
    {
        GetMousePosition();
        RotateAroundObject();
    }

    private void GetMousePosition()
    {
        Plane plane = new Plane(Vector3.up, 0);

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            mousePosition = ray.GetPoint(distance);
        }
    }
}
