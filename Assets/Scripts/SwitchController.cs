using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour
{
    public GameObject Reciever;

    public AudioClip Pressed;
    public AudioClip Released;

    Animator animator;
    AudioManager am;

    void Start()
    {
        animator = GetComponent<Animator>();
        am = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter(Component other)
    {
        bool pressed = animator.GetBool("isPressed");
        if (!pressed)
        {
            animator.SetBool("isPressed", true);
            am.Play(Pressed);
            if (Reciever != null)
                Reciever.SendMessage("SwitchPressed", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit(Component other)
    {
        bool pressed = animator.GetBool("isPressed");
        if (pressed)
        {
            animator.SetBool("isPressed", false);
            am.Play(Released);
            if (Reciever != null)
                Reciever.SendMessage("SwitchReleased", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
