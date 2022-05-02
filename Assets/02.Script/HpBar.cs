using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject m_goProfab = null;

    List<Transform> transform_List = new List<Transform>();
    List<GameObject> hpBarList = new List<GameObject>();
    List<GameObject> target_Objects_List = new List<GameObject>();
    Camera m_cam;
    
    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        set_hpbar();
    }

    public void set_hpbar()
    {
        target_Objects_List.Clear();
        transform_List.Clear();
       
        Debug.Log("hpBarList: " + hpBarList.Count);
        GameObject[] target_Objects_Array = GameObject.FindGameObjectsWithTag("Monster");
        target_Objects_List.AddRange(target_Objects_Array); // 배열을 리스트로 넘김

        for (int i = 0; i < target_Objects_List.Count; i++)
        {
            if(target_Objects_List[i].tag == "Monster")
            {
                if (target_Objects_List[i].GetComponent<Monster>().is_die == true)
                {
                    target_Objects_List.RemoveAt(i);
                }
            }
           
        }

       
        target_Objects_List.Add(GameObject.FindGameObjectWithTag("Player"));
        

        for (int i = 0; i < target_Objects_List.Count; i++)
        {
            
            transform_List.Add(target_Objects_List[i].transform);

            if (target_Objects_List[i].tag == "Player")
            {
                if(target_Objects_List[i].GetComponent<Player>().is_have_hpbar == false) // hp 바를 가지고 있지않을때만 생성됨
                {
                    GameObject target_hpbar = Instantiate(m_goProfab, target_Objects_List[i].transform.position, Quaternion.identity, transform);
                    hpBarList.Add(target_hpbar);
                }
            }
            else if (target_Objects_List[i].tag == "Monster")
            {
                if (target_Objects_List[i].GetComponent<Monster>().monster_is_have_hpbar == false)
                {
                    GameObject target_hpbar = Instantiate(m_goProfab, target_Objects_List[i].transform.position, Quaternion.identity, transform);
                    hpBarList.Add(target_hpbar);
                }
            }

            if(target_Objects_List[i].tag == "Player")  
            {
                target_Objects_List[i].GetComponent<Player>().is_have_hpbar = true;
            }
            else if (target_Objects_List[i].tag == "Monster")
            {
                target_Objects_List[i].GetComponent<Monster>().monster_is_have_hpbar = true;
            }
             
        }
        hpBarList.Remove(null);
       
    }

    float maxhp_per_curhp_monster = 0;
    float maxhp_per_curhp_player = 0;
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < transform_List.Count; i++)
        {
           
                    
               

                if (target_Objects_List[i].tag == "Monster")
                {
                    if(target_Objects_List[i].GetComponent<Monster>().is_die == true){ // 몬스터가 죽어있다면
                        Destroy(hpBarList[i]); // 해당 체력바를 삭제시킴
                        transform_List.RemoveAt(i);
                        hpBarList.RemoveAt(i);
                        target_Objects_List.RemoveAt(i);
                    // 몬스터죽은자리의 리스트를 비움
                    break; // 몬스터가 죽었으니 for문을 끝냄
                    }
                    maxhp_per_curhp_monster = (target_Objects_List[i].GetComponent<Monster>().monster_currentHP / target_Objects_List[i].GetComponent<Monster>().monster_maxHp); //몬스터의 HP비율을 구함
                    hpBarList[i].transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(40 * maxhp_per_curhp_monster, 6);//HP바에 비율을 반영
                }
                else if (target_Objects_List[i].tag == "Player")
                {
                    maxhp_per_curhp_player = (target_Objects_List[i].GetComponent<Player>().currentHP / target_Objects_List[i].GetComponent<Player>().maxHp);//플레이어 HP비율을 구함
                    hpBarList[i].transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(40 * maxhp_per_curhp_player, 6);//HP바에 비율을 반영
                }

                hpBarList[i].transform.position = m_cam.WorldToScreenPoint(transform_List[i].position + new Vector3(0, -0.7f, 0));

        }
    }  
}
