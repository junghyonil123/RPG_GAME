using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image image;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        
        
    }
}
