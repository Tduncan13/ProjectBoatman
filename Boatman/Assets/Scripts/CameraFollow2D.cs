using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffSet;

    [SerializeField]
    Vector2 posOffSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Cameras current position
        Vector3 startPos = transform.position;

        // Players current position
        Vector3 endPos = player.transform.position;
        endPos.x += posOffSet.x;
        endPos.y += posOffSet.y;
        endPos.z += -10;

        // This is how you Lerp 
        transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);

        // This is how you use Smooth Dampening. 
        //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);

    }
}
