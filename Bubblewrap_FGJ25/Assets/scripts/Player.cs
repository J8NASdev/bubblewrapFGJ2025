using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float jumpForce;

    Transform inBubble;

    public float dieOffset;
    float deathYPos;
    public CameraMovement cameraMovement;

   

    // Start is called before the first frame update
    void Start()
    {
        deathYPos = transform.position.y + dieOffset;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (!inBubble) return;

        rbody.constraints = RigidbodyConstraints2D.None;
        rbody.velocity = Vector2.zero;
        rbody.AddForce(Vector2.up * jumpForce);

        inBubble = null;
    }


    public void SetBubble(Transform bubble)
    {
        transform.position = bubble.position;
        inBubble = bubble;
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        cameraMovement.SetNewHeight(bubble.position.y);

        deathYPos = bubble.position.y+ dieOffset;
    }


    void KillPlayer()
    {
        Debug.Log("Player Died");
    }

}
