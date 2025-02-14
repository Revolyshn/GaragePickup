using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 100f;

    private Vector2 moveInput; 
    private Vector2 lookInput; 

    public Transform cameraTransform;

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else if (cameraTransform == null)
        {
            Debug.LogError("Camera not assigned and MainCamera not found in the scene!");
        }
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void Look()
    {
        float lookX = lookInput.x * lookSpeed * Time.deltaTime;
        float lookY = lookInput.y * lookSpeed * Time.deltaTime;

        
        transform.Rotate(Vector3.up * lookX);

        
        cameraTransform.Rotate(Vector3.right * -lookY);
    }

    public void OnMove(Vector2 input)
    {
        moveInput = input;
    }

    public void OnLook(Vector2 input)
    {
        lookInput = input;
    }
}
