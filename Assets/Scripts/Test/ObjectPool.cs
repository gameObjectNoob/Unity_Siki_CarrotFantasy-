using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject monster;

    private Stack<GameObject> monsterPool;
    private Stack<GameObject> activeMonsterList;

    private void Start()
    {
        monsterPool = new Stack<GameObject>();
        activeMonsterList = new Stack<GameObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject itemGo = GetMonster();
            itemGo.transform.position = Vector3.one;
            activeMonsterList.Push(itemGo);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (activeMonsterList.Count>0)
            {
                PushMonster(activeMonsterList.Pop());
            }
        }
    }

    private GameObject GetMonster() {
        GameObject monsterGo = null;
        if (monsterPool.Count <= 0)
        {
            monsterGo = Instantiate(monster);
        }
        else
        {
            monsterGo = monsterPool.Pop();
            monsterGo.SetActive(true);
        }
        return monsterGo;
    }

    private void PushMonster(GameObject monsterGo) {
        monsterGo.transform.SetParent(transform);
        monsterGo.SetActive(false);

        monsterPool.Push(monsterGo);
    }

}
