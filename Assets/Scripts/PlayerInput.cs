using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public JoystickController moveJoystick; // Джойстик для движения
    public JoystickController lookJoystick; // Джойстик для поворота камеры
    public FirstPersonController firstPersonController;

    private void Update()
    {
        // Получаем ввод от джойстиков
        Vector2 moveInput = moveJoystick.GetInput();
        Vector2 lookInput = lookJoystick.GetInput();

        // Передаем ввод в FirstPersonController
        firstPersonController.OnMove(moveInput);
        firstPersonController.OnLook(lookInput);
    }
}