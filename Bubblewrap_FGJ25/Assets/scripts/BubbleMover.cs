using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleMover : MonoBehaviour
{
    public Transform bubble;
    public float speed;
    public Transform pointParent;

    public Transform endPoint;


    int target = 0;
    bool goToEnd = false;
    float endTimer = 5f;
    // Update is called once per frame
    void Update()
    {
        

        if(goToEnd)
        {

            
            if (Vector2.Distance((Vector2)bubble.position, (Vector2)endPoint.position) > 0.5f)
            {
                bubble.Translate((endPoint.position - bubble.position).normalized * speed * Time.deltaTime, Space.World);
                return;
            }

            endTimer -= Time.deltaTime;

            if (endTimer <= 0f) SceneManager.LoadScene(0);

            return;
        }


        bubble.Translate((pointParent.GetChild(target).position - bubble.position).normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance((Vector2)bubble.position, (Vector2)pointParent.GetChild(target).position) < 0.5f)
        {
            target++;
            target %= pointParent.childCount;
        }
    }

    public void PlyerMovedHere()
    {
        Debug.Log("Played moved to " + gameObject.name);
        if (!endPoint) return;

        goToEnd = true;
        speed *= 4f;
    }
}
