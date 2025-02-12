using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Получаем компонент Animator
        animator = GetComponent<Animator>();

        // Запускаем анимацию
        animator.SetTrigger("Open");
    }
}