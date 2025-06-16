using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : ActionNode
{
    //�̰� ���� �ӵ��� ��ȭ ��Ű�� ��Ʈ�Ѱ� ���� �ʿ�... ��Ʈ�� ��ü�� ���� �ʿ䵵 �־�� 
    public override BT.State Run(EnemyBase enemy)
    {
      
        enemy.animator.SetBool(enemy.aniPara.RunParaHash, true);
        enemy.animator.SetBool(enemy.aniPara.walkParaHash, false);
        Debug.Log("���� ������");
        Vector3 dir = (enemy.detection.Target.transform.position - enemy.transform.position).normalized;

        Debug.Log(enemy.transform.position);
        Debug.Log(dir);
        Vector3 forward = enemy.transform.forward;
        dir.y = 0;
        forward.y = 0;
         enemy.Controller.Look(dir);
            
        enemy.Controller.MoveTo(enemy.detection.Target.transform.position,true);
        float distance = Vector3.Distance(enemy.detection.Target.transform.position, enemy.transform.position);

        if (distance < 0.1f)
        {
            return BT.State.Sucess;
        }
        
        return BT.State.Sucess;
    }
}
