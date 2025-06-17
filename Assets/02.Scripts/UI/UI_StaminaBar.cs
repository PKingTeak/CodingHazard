using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StaminaBar : UI_Base
{
    private const float clampratio = 0.35f; //0.35 ~ 0 �� 1���� 0������ ���ߴ� ��
    enum Images
    {
        StaminaDamageBar,
        StaminaBar,
    }
    public void OnEnable()
    {
        PlayerEvent.OnStaminaChanged += SetStaminaRatio;
    }

    public void OnDisable()
    {
        PlayerEvent.OnStaminaChanged -= SetStaminaRatio;
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindImage(typeof(Images));
        return true;
    }

    float _lastFillAmount = -1;
    public void SetStaminaRatio(float curvalue)
    {
        float max = StageManager.Instance.PlayerController.GetComponent<PlayerCondition>().stamina.maxValue;
        float ratio = Mathf.Clamp01(curvalue / max);
        float scaledRatio = ratio * clampratio;

        Image bar = GetImage((int)Images.StaminaBar);
        Image dmgBar = GetImage((int)Images.StaminaDamageBar);

        // 1. ��� ���ŵǴ� �� ��
        bar.fillAmount = scaledRatio;

        // 2. ���� or ȸ�� �Ǵ�
        if (_lastFillAmount > scaledRatio)
        {
            // ���� ���̸� Tween ��� (õõ�� �������)
            DOTween.Kill(dmgBar);
            dmgBar.DOFillAmount(scaledRatio, 1f).SetEase(Ease.OutQuad);
        }
        else
        {
            // ȸ�� ���̸� �ٷ� ä������
            dmgBar.fillAmount = scaledRatio;
        }

        _lastFillAmount = scaledRatio;
    }

}
