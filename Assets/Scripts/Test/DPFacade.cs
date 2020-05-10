using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFacade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Principal principal = new Principal();
        principal.OrderTeacherToDoTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//上层管理
public class Principal {
    private Teacher teacher = new Teacher();
    public void OrderTeacherToDoTask() {
        teacher.OrderStudentsToSummary();
    }
}
//外观角色
public class Teacher {
    private Moniteor moniteor = new Moniteor();
    private LeagueSecretary leagueSecretary = new LeagueSecretary();
    public void OrderStudentsToSummary() {
        moniteor.WriteSummary();
        leagueSecretary.WriteSummary();
    }
}
//下次实现
public class Moniteor
{
    public void WriteSummary()
    {
        Debug.Log("班长的总结");
    }
}
//下层实现
public class LeagueSecretary {
    public void WriteSummary() {
        Debug.Log("团支书的总结");
    }
}