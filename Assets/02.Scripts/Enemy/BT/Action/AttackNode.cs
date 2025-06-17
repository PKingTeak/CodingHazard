using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{
    public override BT.State Run(EnemyBase enemy)
    {
        enemy.Controller.StopMove();
        enemy.StartAttack();
       
        return BT.State.Sucess;
    }

   
}
//�̰ͺ��� ���ڷ����͸� ����ϴ� ��������...
public class StopAttackNode : ActionNode
{
    public override BT.State Run(EnemyBase enemy)
    {
        if (enemy.IsAttack)
            return BT.State.Sucess;
        else
            return BT.State.Failure;
    }


}
