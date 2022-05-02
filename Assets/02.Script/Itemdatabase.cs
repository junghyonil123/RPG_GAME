using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemdatabase : MonoBehaviour
{
    public static Itemdatabase instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemList = new List<Item>();
}
