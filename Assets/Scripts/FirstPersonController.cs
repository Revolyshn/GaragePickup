using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 10f; // Скорость движения
    public float lookSpeed = 100f; // Скорость поворота камеры

    private Vector2 moveInput; // Ввод для движения
    private Vector2 lookInput; // Ввод для поворота камеры

    public Transform cameraTransform; // Ссылка на камеру

    private void Awake()
    {
        // Проверяем, есть ли камера с тегом MainCamera
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else if (cameraTransform == null)
        {
            Debug.LogError("Camera not assigned and MainCamera not found in the scene!");
        }

        // Блокируем курсор мыши в центре экрана
        //Cursor.lockState = CursorLockMode.Locked;
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

        // Поворачиваем персонажа по горизонтали
        transform.Rotate(Vector3.up * lookX);

        // Поворачиваем камеру по вертикали
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