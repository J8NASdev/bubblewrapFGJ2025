using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed;
    Vector3 targetPos;
    Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPos + offset) < 0.5f) return;
        transform.Translate(((targetPos+offset) - transform.position) * speed * Time.deltaTime);
    }

    public void SetNewHeight(float yPos)
    {
        targetPos = new Vector3(targetPos.x, yPos, targetPos.z);
    }
}
