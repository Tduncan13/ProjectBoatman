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

        // This is how you Lerp 
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // This is how you use Smooth Dampening. 
        //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);

    }
}
