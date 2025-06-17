using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCol : MonoBehaviour
{
    [SerializeField]
    DropPoint[] dp;
    [SerializeField] private string targetLayerName = "Player";


    private int targetLayer;
    private void Start()
    {
        targetLayer = LayerMask.NameToLayer(targetLayerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        if (other.gameObject.layer == targetLayer)
        {
            Debug.Log("��ȯ");
            foreach (DropPoint dp in dp) { 
                dp.Spawn(other.GetComponent<PlayerCondition>());
            }
        }
    }
}
