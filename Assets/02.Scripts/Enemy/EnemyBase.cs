using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour
{
    //[SerializeField]
    private EnemyController controller;
    [SerializeField]
    private EnemyStatus status;
    [SerializeField]
    public EnemyAnimation aniPara;
    public EnemyController Controller {  get { return controller; } }
    public EnemyStatus Status { get { return status; } }
    public Animator animator;
    [SerializeField]
    private LayerMask targetMask;
    private BT bt;
    private GameObject target; //���� �𸣰����� �÷��̾� ��ũ��Ʈ�� ���� �ʿ�
    public GameObject Target { get { return target; } }
    void Awake()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
        bt = GetComponent<BT>();
        
    }
    private void Start()
    {
        aniPara.Init();
        controller.Init(status.MoveSpeed);
        bt.MakeBT();
        bt.StartBT(this);
    }
    public bool FindTarget()
    {
        Debug.Log("Ž��");
        Collider[] hits = Physics.OverlapSphere(transform.position, status.SightRange, targetMask);
        Debug.Log(hits.Length); 
        foreach(var hit in hits)
        {
            Debug.Log("����");
            target = hit.gameObject;
            return true;
        }
        target = null;
       return false;
       
    }
    public bool CanAttack()
    {
        if(target == null)
            return false;
        if(Vector3.Distance(target.transform.position,transform.position)<=status.AttackRange)
        {
          
            return true;
        }
      
        return false;
    }
    void Attack()
    {
        Debug.Log("����");
        animator.SetBool(aniPara.AttackParaHash, true);
    }
    IEnumerator AttackE()
    {
        while (true)
        {
            animator.SetBool(aniPara.AttackParaHash, true);
            Attack();
            animator.SetBool(aniPara.AttackParaHash, false);
            yield return new WaitForSeconds(status.AttackCoolTime);
        }
    }
    Coroutine attack=null;
    public void StartAttack()
    {
        Debug.Log("���� ����");
        animator.SetBool(aniPara.AttackParaHash, true);
        animator.SetBool(aniPara.RunParaHash, false);
        if (attack == null) {
            attack = StartCoroutine(AttackE());
          }
        
    }
    public void StopAttack()
    {
        Debug.Log("���� ����");
        animator.SetBool(aniPara.AttackParaHash, false);
        if (attack != null) { 
            StopCoroutine(attack);
        }
    }
    void OnDrawGizmos()
    {
        if (status == null) return;

        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, status.SightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, status.AttackRange);
    }
}
