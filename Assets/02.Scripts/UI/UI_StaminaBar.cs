using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetStaminaRatio(float curvalue)
    {
        //���׹̳� Ȯ���ϰ� ������ �۵��ϴ��� �𸣰���
        float ratio = Mathf.Clamp01(curvalue / StageManager.Instance.PlayerController.GetComponent<PlayerCondition>().stamina.maxValue);
        float scaledRatio = ratio * clampratio;
        GetImage((int)Images.StaminaBar).fillAmount = scaledRatio;

        DOTween.Kill(GetImage((int)Images.StaminaDamageBar));

        GetImage((int)Images.StaminaDamageBar).DOFillAmount(scaledRatio, 1f).SetEase(Ease.OutQuad);
    }

}
