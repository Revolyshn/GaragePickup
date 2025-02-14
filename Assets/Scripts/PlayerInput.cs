using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public JoystickController moveJoystick;
    public JoystickController lookJoystick;
    public FirstPersonController firstPersonController;

    private void Update()
    {
        Vector2 moveInput = moveJoystick.GetInput();
        Vector2 lookInput = lookJoystick.GetInput();

        firstPersonController.OnMove(moveInput);
        firstPersonController.OnLook(lookInput);
    }
}