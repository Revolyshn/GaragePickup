using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -9.81f; // Сила гравитации
    

    private CharacterController characterController;
    private Vector3 velocity; // Скорость падения

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Движение по горизонтали
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Гравитация
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Небольшая сила прижатия к земле
        }

        // Применяем гравитацию
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}