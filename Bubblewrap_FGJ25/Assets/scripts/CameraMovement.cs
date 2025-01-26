using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed;
    Vector3 offset;
    Transform followBubble;

    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!followBubble) return;
        Vector3 targetPos = new Vector3(0, followBubble.position.y, 0) + offset;
        if (Vector2.Distance(transform.position, targetPos) < 0.5f) return;
        transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);
    }

    public void SetNewHeight(Transform bubbleTrans)
    {
        followBubble = bubbleTrans;
    }

}
