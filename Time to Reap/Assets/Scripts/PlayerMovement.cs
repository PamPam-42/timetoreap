using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private BoxCollider2D boxCollider;
	private Animator anim;
	private SpriteRenderer sprite;
	
	[SerializeField] private LayerMask groundLayer;
	
	private float dirX = 0f;
	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;
	private bool grounded = true;
	private bool canDoubleJump = false;
	
	private AudioSource audioSource;
	[SerializeField] AudioClip jumpSoundClip;
	[SerializeField] AudioClip attackSoundClip;
	
    // Start is called before the first frame update
    void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();   
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		dirX = Input.GetAxisRaw("Horizontal");
		rb2d.velocity = new Vector2(dirX * moveSpeed, rb2d.velocity.y);
        
		//Update Animation
		if (dirX > 0f)
        {
			anim.SetBool("playerMoving", true);
			sprite.flipX = true;
        }
        else if (dirX < 0f) {
			anim.SetBool("playerMoving", true);
			sprite.flipX = false;
		}
        else {
            anim.SetBool("playerMoving", false);
		}
		
		if (Input.GetButtonDown("Jump")) {
			grounded = isGrounded();
			if (grounded) {
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
				audioSource.PlayOneShot(jumpSoundClip);
				canDoubleJump = true;				
			}
			else if (canDoubleJump) {
				//rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
				audioSource.PlayOneShot(jumpSoundClip);
				canDoubleJump = false;
			}
		}
		
		if (Input.GetButtonDown("Attack")) {
			anim.SetTrigger("attackPressed");
			audioSource.PlayOneShot(attackSoundClip);
		}
	}
	
	private bool isGrounded() {
		return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
	}
}
