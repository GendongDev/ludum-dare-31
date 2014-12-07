using UnityEngine;

public class SendMessageOnTriggerEvent : MonoBehaviour
{

    public string EnterMessage;
    public GameObject EnterParameter;
    public string ExitMessage;
    public GameObject ExitParameter;

    void OnTriggerEnter(Component other)
    {
        if(!string.IsNullOrEmpty(EnterMessage))
            other.gameObject.SendMessage(EnterMessage, (EnterParameter) ? EnterParameter : gameObject, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerExit(Component other)
    {
        if (!string.IsNullOrEmpty(ExitMessage))
            other.gameObject.SendMessage(ExitMessage, (ExitParameter) ? ExitParameter : gameObject, SendMessageOptions.DontRequireReceiver);
    }
}
