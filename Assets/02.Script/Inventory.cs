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
            Debug.Log("�Լ�����");
            if(slots[i].item != null)
            {
                Debug.Log("���̾ƴ�");
                slots[i].item = getted_item; //���Լӿ� �������� �־���
                slots[i].image.sprite = getted_item.itemImage; //���Լ��� �̹����� �������̹����� �ٲ���
                slots[i].image.gameObject.SetActive(true); //���ϼ��� �̹����� Ȱ��ȭ������
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
