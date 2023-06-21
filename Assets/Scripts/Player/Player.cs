using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField, BoxGroup("References")] public Animator animator;
    [SerializeField, BoxGroup("References")] public CharacterController characterController;
    [SerializeField, BoxGroup("References")] public List<FlashColor> flashColors;

    [SerializeField, BoxGroup("Character config")] public float speed = 1f;
    [SerializeField, BoxGroup("Character config")] public float turnSpeed = 1f;
    [SerializeField, BoxGroup("Character config")] public float gravity = 9.8f;

    [SerializeField, BoxGroup("Character Jump config")] public float jumpForce = 15f;

    [SerializeField, BoxGroup("Character Run config")] public float speedRun = 1.5f;

    [ShowNonSerializedField, BoxGroup("Keys")] private KeyCode KeyJump = KeyCode.Space;
    [ShowNonSerializedField, BoxGroup("Keys")] private KeyCode KeyRun = KeyCode.LeftShift;

    private float vSpeed = 0;

    #region Life
    public void Damage(float damage)
    {
        Debug.Log("Flash player");
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 direction)
    {
        flashColors.ForEach(i => i.Flash());
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(KeyJump))
            {
                vSpeed = jumpForce;
            }
        }        

        var isWalking = inputAxisVertical != 0;
        if(isWalking)
        {
            if(Input.GetKey(KeyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            } else
            {
                animator.speed = 1;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", isWalking);
        
    }
}
