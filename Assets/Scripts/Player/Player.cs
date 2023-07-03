using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using System.Collections;
using Cloth;

public class Player : Singleton<Player>
{
    [SerializeField, BoxGroup("References")] public Animator animator;
    [SerializeField, BoxGroup("References")] public CharacterController characterController;
    [SerializeField, BoxGroup("References")] public List<FlashColor> flashColors;
    [SerializeField, BoxGroup("References")] public List<Collider> colliders;
    [SerializeField, BoxGroup("References")] public HealthBase healthBase;    
    [SerializeField, BoxGroup("References")] public ClothChanger _clothChanger;

    [SerializeField, BoxGroup("Character config")] public float speed = 1f;
    [SerializeField, BoxGroup("Character config")] public float turnSpeed = 1f;
    [SerializeField, BoxGroup("Character config")] public float gravity = 9.8f;

    [SerializeField, BoxGroup("Character Jump config")] public float jumpForce = 15f;

    [SerializeField, BoxGroup("Character Run config")] public float speedRun = 1.5f;    

    [ShowNonSerializedField, BoxGroup("Keys")] private KeyCode KeyJump = KeyCode.Space;
    [ShowNonSerializedField, BoxGroup("Keys")] private KeyCode KeyRun = KeyCode.LeftShift;

    [ShowNonSerializedField, BoxGroup("Animation config")] private bool _isAlive = true;
    [ShowNonSerializedField, BoxGroup("Animation config")] private bool _isJumping = false;


    private float vSpeed = 0;

    private void Start()
    {
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
        Spawn();
    }

    private void OnValidate()
    {
        if(healthBase == null) GetComponent<HealthBase>();
    }

    #region Life
    private void OnKill(HealthBase h)
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }        
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    private void Revive()
    {
        _isAlive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    public void Damage(HealthBase h)
    {        
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 direction)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    
    #endregion
    
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded)
        {
            if (_isJumping)
            {
                _isJumping = false;                
                animator.SetTrigger("Land");
            }

            vSpeed = 0;
            if (Input.GetKeyDown(KeyJump))
            {
                vSpeed = jumpForce;

                if (!_isJumping)
                {
                    _isJumping = true;                    
                    animator.SetTrigger("Jump");
                }
                
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
    public void Spawn()
    {
        if(SaveManager.Instance.lastCheckpoint != 0)
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint(SaveManager.Instance.lastCheckpoint);
        }
    }

    [Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HaveCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speedModificator, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speedModificator, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }    

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);        
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }
}
