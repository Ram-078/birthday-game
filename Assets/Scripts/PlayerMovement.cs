using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
            if (SaveManager.Instance != null &&
                SaveManager.Instance.playerPosition != Vector3.zero)
            {
                transform.position = SaveManager.Instance.playerPosition;
                MapTransition.RestoreCameraBoundary();
            }
        
    }

    void Update()
    {
        if (PauseController.isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            StopFootsteps();
            return;
        }
        rb.linearVelocity= movement * speed;
        animator.SetBool("isWalking",rb.linearVelocity.magnitude>0);

        if (rb.linearVelocity.magnitude > 0 && !playingFootsteps)
        {
            StartFootsteps();
        }
        else if (rb.linearVelocity.magnitude == 0)
        {
            StopFootsteps();
        }

    }

    public void Move(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
        }
        
        movement = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
    }
    
    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootsteps));
    }

    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootsteps), 0f, footstepSpeed);
    }

    void PlayFootsteps()
    {
        SoundFxManager.Play("FootStep");

    }
    
    
    
}
