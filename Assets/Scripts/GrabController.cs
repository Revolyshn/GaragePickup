using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabPoint; // Точка, куда будет перемещаться предмет
    public float grabDistance = 2f; // Дистанция захвата
    public LayerMask grabLayer; // Слой для предметов, которые можно захватить

    private GameObject grabbedObject; // Захваченный объект
    private Rigidbody grabbedObjectRigidbody; // Физический компонент захваченного объекта

    public void TryGrab()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        if (grabPoint == null)
        {
            Debug.LogError("Grab point not assigned!");
            return;
        }

        if (grabbedObject == null)
        {
            // Бросаем луч от камеры вперед
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, grabDistance, grabLayer))
            {
                // Проверяем, есть ли у объекта компонент Rigidbody
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Захватываем объект
                    grabbedObject = hit.collider.gameObject;
                    grabbedObjectRigidbody = rb;
                    grabbedObjectRigidbody.isKinematic = true; // Отключаем физику
                    grabbedObject.GetComponent<Collider>().enabled = false; // Отключаем коллизии
                }
            }
        }
        else
        {
            // Отпускаем предмет
            ReleaseObject();
        }
    }

    private void ReleaseObject()
    {
        // Включаем физику и коллизии
        grabbedObjectRigidbody.isKinematic = false;
        grabbedObject.GetComponent<Collider>().enabled = true;
        grabbedObject = null;
        grabbedObjectRigidbody = null;
    }

    private void FixedUpdate()
    {
        // Если предмет захвачен, перемещаем его
        if (grabbedObject != null)
        {
            MoveGrabbedObject();
        }
    }

    private void MoveGrabbedObject()
    {
        // Плавно перемещаем объект к точке захвата
        grabbedObject.transform.position = Vector3.Lerp(
            grabbedObject.transform.position,
            grabPoint.position,
            Time.fixedDeltaTime * 10f // Скорость перемещения
        );

        // Проверяем коллизии
        PreventWallClipping();
    }

    private void PreventWallClipping()
    {
        // Бросаем луч от камеры к объекту
        Ray ray = new Ray(Camera.main.transform.position, grabPoint.position - Camera.main.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(Camera.main.transform.position, grabPoint.position)))
        {
            // Если луч пересекает стену, перемещаем объект ближе к игроку
            grabbedObject.transform.position = hit.point - (grabPoint.position - Camera.main.transform.position).normalized * 0.1f;
        }
    }
}