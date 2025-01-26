using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        collider.gameObject.GetComponent<Player>().SetBubble(transform);
        transform.parent.GetComponent<BubbleMover>().PlyerMovedHere();
    }
}
