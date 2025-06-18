using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUIController : MonoBehaviour
{
    [SerializeField] private UI_ClearPopup _popup;
    [SerializeField] private UI_FailPopup _failpopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StageManager.Instance.OnClearStage += Play;
        StageManager.Instance.OnFailStage += Fail;
    }

    private void OnDisable()
    {
        StageManager.Instance.OnClearStage -= Play;
        StageManager.Instance.OnFailStage -= Fail;
    }
    private void Play()
    {
        StartCoroutine(PlaySequence());
    }

    private void Fail()
    {
        StartCoroutine(FailSequence());
    }

    private IEnumerator FailSequence()
    {
        yield return _failpopup.FadeInCoroutine();             
        yield return _failpopup.ToastCoroutine();
        SceneManager.LoadScene(3);
    }
    private IEnumerator PlaySequence()
    {
        yield return _popup.FadeInCoroutine();             // 1. FadeIn �Ϸ� ���
        yield return _popup.ToastCoroutine();               // 2. Toast �ִϸ��̼� �Ϸ� ���
        yield return _popup.StartCreditScrollCoroutine(); // 3. ũ���� ��ũ�� �Ϸ� ���
    }



}
