using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public Slot[] slots;
    public Transform slotHolder;

    public void get_item(Item getted_item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log("함수들어옴");
            if(slots[i].item != null)
            {
                Debug.Log("널이아님");
                slots[i].item = getted_item; //슬롯속에 아이템을 넣어줌
                slots[i].image.sprite = getted_item.itemImage; //슬롯속의 이미지를 아이템이미지로 바꿔줌
                slots[i].image.gameObject.SetActive(true); //슬록속의 이미지를 활성화시켜줌
                break;
            }
        }
    }

    public void remove_item()
    {

    }

    

    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
