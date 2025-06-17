using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUIController : MonoBehaviour
{
    [SerializeField] private UI_ClearPopup _popup;

    private void OnEnable()
    {
        StageManager.Instance.OnStageClear += Play;
    }

    private void OnDisable()
    {
        StageManager.Instance.OnStageClear -= Play;
    }


    private void Play()
    {
        StartCoroutine(PlaySequence());
    }
    private IEnumerator PlaySequence()
    {
        yield return _popup.FadeInCoroutine();             // 1. FadeIn �Ϸ� ���
        yield return _popup.ToastCoroutine();               // 2. Toast �ִϸ��̼� �Ϸ� ���
        yield return _popup.StartCreditScrollCoroutine(); // 3. ũ���� ��ũ�� �Ϸ� ���
    }

}
