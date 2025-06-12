using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{
    public override BT.State Run(EnemyBase enemy)
    {
        enemy.StartAttack();
        return BT.State.Sucess;
    }

   
}
//�̰ͺ��� ���ڷ����͸� ����ϴ� ��������...
public class StopAttackNode : ActionNode
{
    public override BT.State Run(EnemyBase enemy)
    {
        enemy.StopAttack();
        return BT.State.Failure;
    }


}
