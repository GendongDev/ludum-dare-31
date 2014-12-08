using UnityEngine;
using System.Collections.Generic;

public class HeroController : MonoBehaviour
{
    public Transform MovementRelative;
    public float Speed;
    public float RunSpeed;
    public AudioClip ItemPickupClip;

    CharacterController controller;
    Transform oldParent;
    bool frozen;
    AudioManager am;
    GameObject follow;
    Vector3 followPositionPrev;

    List<string> itemsPickedup;

    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        controller = GetComponent<CharacterController>();
        itemsPickedup = new List<string>();
    }

    void Update()
    {
        if (frozen)
            return;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if(controller.isGrounded && (!Mathf.Approximately(v, 0) || !Mathf.Approximately(h, 0)))
        {
            Vector3 forward = MovementRelative.forward;
            Vector3 right = MovementRelative.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 heading = (forward * v) + (right * h);

            transform.localRotation = Quaternion.LookRotation(heading);

            float s = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) ? RunSpeed : Speed;

            controller.Move(transform.forward * s * Time.deltaTime);
        }

        if (follow != null)
        {
            Vector3 d = follow.transform.position - followPositionPrev;
            controller.Move(d);
            followPositionPrev = follow.transform.position;
        }

        controller.Move(Physics.gravity * Time.deltaTime);
    }

    void PushParent(GameObject go)
    {
        //oldParent = transform.parent;
        //transform.parent = go.transform;
        follow = go;
        followPositionPrev = follow.transform.position;
    }

    void PopParent(GameObject go)
    {
        //transform.parent = oldParent;
        follow = null;
    }

    void ItemPickup(GameObject go)
    {
        am.Play(ItemPickupClip);

        NamedValues nv = go.GetComponent<NamedValues>();

        int l = nv.GetAsInt("Level");
        int i = nv.GetAsInt("Item");

        //PlayerPrefs.SetInt("Level" + l + "Item" + i, 1);
        itemsPickedup.Add("Level" + l + "Item" + i);

        Destroy(go);
    }

    void Freeze()
    {
        frozen = true;
    }

    void GoalReached(GameObject go)
    {
        foreach (string item in itemsPickedup)
        {
            PlayerPrefs.SetInt(item, 1);
        }

        SendMessageUpwards("HeroReachedGoal");
    }

    void EnemyContact(GameObject go)
    {
        SendMessageUpwards("HeroDied");
    }
}
