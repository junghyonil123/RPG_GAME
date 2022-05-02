using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject backGround_Pre;
    public GameObject player_In_Background;
    private GameObject player;

    public GameObject control_canvas;
    public GameObject inventory_canvas;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void inventory_exit()
    {
        inventory_canvas.SetActive(false);
    }
    public void inventory_on()
    {
        inventory_canvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
