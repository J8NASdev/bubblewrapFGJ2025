using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    public Transform bubble;
    public float speed;
    public Transform pointParent;


    int target = 0;


    // Update is called once per frame
    void Update()
    {
        bubble.Translate((pointParent.GetChild(target).position - bubble.position).normalized * speed * Time.deltaTime, Space.World);

        if(Vector2.Distance((Vector2)bubble.position, (Vector2)pointParent.GetChild(target).position) < 0.5f)
        {
            target++;
            target %= pointParent.childCount;
        }
    }
}
