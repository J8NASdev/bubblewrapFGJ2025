using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float jumpForce;

    Transform inBubble;

    public float dieOffset;
    float deathYPos;
    public CameraMovement cameraMovement;
    public Animator animator;

    public bool gameStarted = false;

   

    // Start is called before the first frame update
    void Start()
    {
        deathYPos = transform.position.y + dieOffset;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", rbody.velocity.y);
        if(transform.position.y < deathYPos)
        {
            KillPlayer();
        }

        if(inBubble)
        {
            transform.position = inBubble.position; 
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (!gameStarted) return;
        if (!inBubble) return;
        animator.SetBool("jump", true);

        rbody.constraints = RigidbodyConstraints2D.None;
        rbody.velocity = Vector2.zero;
        rbody.AddForce(Vector2.up * jumpForce);

        inBubble = null;
    }


    public void SetBubble(Transform bubble)
    {
        animator.SetBool("jump", false);
        transform.position = bubble.position;
        inBubble = bubble;
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        cameraMovement.SetNewHeight(bubble);

        deathYPos = bubble.position.y+ dieOffset;
    }


    void KillPlayer()
    {
        Debug.Log("Player Died");
        gameStarted = false;
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
