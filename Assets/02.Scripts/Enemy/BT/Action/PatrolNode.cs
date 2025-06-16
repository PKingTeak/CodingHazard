using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : ActionNode
{
    Vector3 startpos = new Vector3(5, 0, 0);
    Vector3 endPos = new Vector3(-5, 0, 0);
    Vector3 targetPos;
    private bool goToEnd = true;
    public override BT.State Run(EnemyBase enemy)
    {
        //Debug.Log("���� ��ġ��");
        enemy.animator.SetBool(enemy.aniPara.RunParaHash, false);
        //�̰� ��� ������ �� ���� ��
        enemy.animator.SetBool(enemy.aniPara.walkParaHash, true);
      
        if (targetPos == new Vector3(0, 0, 0))
        {
            targetPos = endPos;
            
        }
        Vector3 dir = (targetPos - enemy.transform.position).normalized;
   
        Vector3 forward = enemy.transform.forward;
        dir.y = 0;
        forward.y = 0;
        if (Vector3.Angle(dir, forward) > 5f)
        {
            //���ڸ� ȸ���� �� �����ε�...

            enemy.Controller.Look(dir);
            return BT.State.Running;
        }

        enemy.Controller.Look(dir);
        enemy.Controller.MoveTo(dir,true);
        var a = enemy.transform.position;
        a.y = 0;
        var b = targetPos;
        b.y = 0;
        float distance = Vector3.Distance(b,a );
        
        if (distance < 0.1f)
        {
            if (goToEnd)
                targetPos = startpos;
            else
                targetPos = endPos;
            goToEnd = !goToEnd;
        }
        return BT.State.Running;
    }
}
