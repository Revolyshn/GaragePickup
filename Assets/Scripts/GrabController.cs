using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabPoint;
    public float grabDistance = 2f;
    public LayerMask grabLayer;

    private GameObject grabbedObject;
    private Rigidbody grabbedObjectRigidbody;

    public void TryGrab()
    {
        if (grabbedObject == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, grabDistance, grabLayer))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    grabbedObject = hit.collider.gameObject;
                    grabbedObjectRigidbody = rb;
                    grabbedObjectRigidbody.isKinematic = true;
                    grabbedObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
        else
        {
            ReleaseObject();
        }
    }

    private void ReleaseObject()
    {
        grabbedObjectRigidbody.isKinematic = false;
        grabbedObject.GetComponent<Collider>().enabled = true;
        grabbedObject = null;
        grabbedObjectRigidbody = null;
    }

    private void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            MoveGrabbedObject();
        }
    }

    private void MoveGrabbedObject()
    {
        grabbedObject.transform.position = Vector3.Lerp(
            grabbedObject.transform.position,
            grabPoint.position,
            Time.fixedDeltaTime * 10f
        );

        
        PreventWallClipping();
    }

    private void PreventWallClipping()
    {
        Ray ray = new Ray(Camera.main.transform.position, grabPoint.position - Camera.main.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(Camera.main.transform.position, grabPoint.position)))
        {
            grabbedObject.transform.position = hit.point - (grabPoint.position - Camera.main.transform.position).normalized * 0.1f;
        }
    }
}