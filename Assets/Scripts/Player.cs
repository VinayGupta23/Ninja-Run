using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb2D;
    private AudioSource audioSource;

    private int jumpHold = 0;
    private bool jumpInitiated = false;
    private int minJump = 5;
    private int maxJump = 10;
    private float distToGround = 0;

    public AudioClip runSound;
    public AudioClip jumpSound;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        distToGround = GetComponent<Collider2D>().bounds.extents.y;
        animator.SetBool("isRunning", false);
    }
	
    public void BeginRun()
    {
        animator.SetBool("isRunning", true);
        rb2D.AddForce(new Vector2(this.rb2D.mass * 15f, 0), ForceMode2D.Impulse);

        audioSource.loop = true;
        audioSource.PlayOneShot(runSound);
    }

	void Update () {

        // TimeScale = 0 does not stop calls to update, use flag from Manager instead?

        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            PlayerDead();
            return;
        }
#if UNITY_ANDROID
        if (Input.touchCount > 0 && jumpInitiated == false)
#else
        if (Input.GetKeyDown(KeyCode.Space) && jumpInitiated == false)
#endif
        {
            if (Physics2D.Raycast((Vector2)transform.position - new Vector2(0, distToGround + 0.01f), Vector2.down, 0.1f))
            {
                jumpInitiated = true;
            }
        }

        if (jumpInitiated)
        {
            jumpHold++;
        }

#if UNITY_ANDROID
        if (jumpInitiated == true && (Input.touchCount == 0 || jumpHold > maxJump))
#else
        if (jumpInitiated == true && (Input.GetKeyUp(KeyCode.Space) || jumpHold > maxJump))
#endif
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.PlayOneShot(jumpSound);
            rb2D.AddForce(new Vector2(0, 1.5f * rb2D.mass * Mathf.Min(Mathf.Max(minJump, jumpHold), maxJump)), ForceMode2D.Impulse);
            jumpInitiated = false;
            jumpHold = 0;
        }

        animator.SetFloat("vertSpeed", Mathf.Abs(rb2D.velocity.y));

        Manager.playerScore += (int)rb2D.velocity.x / 10;
    }

    public void OnRunStarted()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(runSound);
    }

    private void PlayerDead()
    {
        this.gameObject.SetActive(false);
        Manager.gameOver = true;
    }
}