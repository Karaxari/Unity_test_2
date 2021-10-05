using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform point;
    public float speed;
    public float distance = 25f;
    Transform targetPoint = null;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPoint != null)
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        targetPoint.position = new Vector3(point.position.z, point.position.y, point.position.z - distance);
    }
}
