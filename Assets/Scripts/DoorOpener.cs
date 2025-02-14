using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetTrigger("Open");
    }
}