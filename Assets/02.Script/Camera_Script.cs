using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Script : MonoBehaviour
{  

    public Vector3 offset;
    public float followSpeed = 0.15f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera_Pos = player.transform.position + offset;
        Vector3 lerp_Pos = Vector3.Lerp(transform.position, camera_Pos, followSpeed);
        transform.position = lerp_Pos;
    }
}
