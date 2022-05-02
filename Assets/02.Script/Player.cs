using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    #region move
    public bool is_Left_Button_Click = false;
    public bool is_Right_Button_Click = false;
    public float move_speed = 5f;
    public bool is_See_left = false;

    public SpriteRenderer player_sprite;

    public void LeftButtonDown()
    {
        is_Left_Button_Click = true;
        is_See_left = true;
    }
    public void LeftButtonUp()
    {
        is_Left_Button_Click = false;
        is_See_left = true;
    }
    public void RightButtonDown()
    {
        is_Right_Button_Click = true;
        is_See_left = false;
    }
    public void RightButtonUp()
    {
        is_Right_Button_Click = false;
        is_See_left = false;
    }
    #endregion

    private bool _is_have_hpbar;
    public bool is_have_hpbar
    {
        get { return _is_have_hpbar; }
        set { _is_have_hpbar = value; }
    }

    private float _damage = 3;
    public float damega
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private float _maxHp = 100;
    public float maxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    private float _currentHP = 100;
    public float currentHP
    {
        get { return _currentHP; }
        set { _currentHP = value; }
    }

    private float _defense = 2;
    public float defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public float weight = 100;

    private float attack_curTime = 0;
    private float attack_coolTime = 0.5f;

    void Start()
    {
        player_sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        move();

        if(attack_curTime > 0)
        {
            attack_curTime -= Time.deltaTime;
        }
    }

    void move()
    {
        if (is_Left_Button_Click)
        {
            transform.Translate(Vector3.left * Time.deltaTime * move_speed);
        }
        else if (is_Right_Button_Click)
        {
            transform.Translate(Vector3.right * Time.deltaTime * move_speed);
        }

        if (!is_See_left)
        {
            player_sprite.flipX = false;
        }
        else
        {
            player_sprite.flipX = true;
        }
    }


    IEnumerator Knock_back(Monster monster)
    {
        if(weight - monster.monster_weight < 0)
        {
            //플레이어가 밀림
            Vector3 lerp_Pos = Vector3.Lerp(transform.position, transform.position + new Vector3((weight - monster.monster_weight) * 0.05f , 0, 0), 3);
            transform.position = lerp_Pos;
        }
        else if(weight - monster.monster_weight > 0)
        {
            Vector3 lerp_Pos = Vector3.Lerp(monster.transform.position, monster.transform.position + new Vector3(( - weight + monster.monster_weight) * 0.05f, 0, 0), 3);
            monster.transform.position = lerp_Pos;
        }
        yield return new WaitForSeconds(1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (attack_curTime <= 0 && collision.gameObject.tag == "Monster")
        {
            //공격
            if(_damage - collision.gameObject.GetComponent<Monster>().monster_defense <= 0)
            {
                collision.gameObject.GetComponent<Monster>().monster_currentHP -= 1;
            }
            else
            {
                collision.gameObject.GetComponent<Monster>().monster_currentHP -= (_damage - collision.gameObject.GetComponent<Monster>().monster_defense);
            }
            //밀려남
            Knock_back(collision.gameObject.GetComponent<Monster>());
           attack_curTime = attack_coolTime;
        }
    }


}
