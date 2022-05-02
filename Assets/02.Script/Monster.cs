using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monster_damage;
    public float monster_maxHp;
    public float monster_currentHP;
    public float monster_defense;

    private float monster_attack_curTime = 0;
    private float monster_attack_coolTime = 0.5f;

    private bool _monster_is_have_hpbar;
    public bool monster_is_have_hpbar
    {
        get { return _monster_is_have_hpbar; }
        set { _monster_is_have_hpbar = value; }
    }

    public float monster_weight = 120;

    public bool is_die;

    public Animator monster_animator;

    void Start()
    {
        drop_item_list.Add(Itemdatabase.instance.itemList[0]);
    }

    private void Awake()
    {
        monster_currentHP = monster_maxHp;
        is_die = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        if (monster_attack_curTime > 0)
        {
            monster_attack_curTime -= Time.deltaTime;
        }
        if(monster_currentHP <= 0)
        {
            is_die = true;
            monster_animator.SetTrigger("die");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (monster_attack_curTime <= 0 && collision.gameObject.tag == "Player")
        {
            if (monster_damage - collision.gameObject.GetComponent<Player>().defense <= 0)
            {
                collision.gameObject.GetComponent<Player>().currentHP -= 1;
            }
            else
            {
                collision.gameObject.GetComponent<Player>().currentHP -= (monster_damage - collision.gameObject.GetComponent<Player>().defense);
            }
            monster_attack_curTime = monster_attack_coolTime;
        }
    }

    public List<Item> drop_item_list = new List<Item>();

    public void dropItem()
    {
        for(int i = 0; i < drop_item_list.Count; i++)
        {
            Inventory.instance.get_item(drop_item_list[i]);
        }
    }

    public void monster_die_diestroy()
    {
        dropItem();
        Destroy(gameObject);
        GameObject.Find("Canvas").GetComponent<HpBar>().set_hpbar();
    }

    
} 
