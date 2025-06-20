﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;


public class UI_GameScene : UI_Base
{
    #region Enum
  
    enum GameObjects
    {
        BulletObject,
    }
    enum Texts
    {
        ItemText,
        MaxBulletText,
        SpareBulletText
    }
    enum Images
    {
       PrimarySlot,
       MeleeSlot,
       OpenEye,
       CloseEye,
       BloodScreen,
    }
    #endregion

    private List<Image> weaponIcons = new List<Image>();    
    private Color whiteColor = Color.white;
    private Color choiceColor = new Color32(0x9C, 0xCF, 0x42, 0xFF);
    private const float fadeDuration = 5f;
    private float testhp = 100f;
    private float testStaminaF = 100f;
    [SerializeField] private UI_HPBar testHP;
    [SerializeField] private UI_StaminaBar testStamina;
    [SerializeField] private UI_EditSetting testSetting;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        #endregion

        GetImage((int)Images.MeleeSlot).gameObject.SetActive(false);
        GetImage((int)Images.PrimarySlot).gameObject.SetActive(false);
        GetImage((int)Images.OpenEye).gameObject.SetActive(false);

        weaponIcons.Add(GetImage((int)Images.PrimarySlot));
        weaponIcons.Add(GetImage((int)Images.MeleeSlot));

        return true;
    }

    private void Awake()
    {
        Init();
       
    }

    private void OnEnable()
    {
        PlayerEvent.Swap += UpdateQuickSlot;
        PlayerEvent.OnDetectMonster += DetectionEyeVisible;
        PlayerEvent.OnTakeDamaged += UpdateBloodScreen;
        PlayerEvent.OnItemCheck += SetItemText;
        PlayerEvent.OnUpdateBullet += SetInitBullet;
    }

    private void OnDisable()
    {
        PlayerEvent.Swap -= UpdateQuickSlot;
        PlayerEvent.OnDetectMonster -= DetectionEyeVisible;
        PlayerEvent.OnTakeDamaged -= UpdateBloodScreen;
        PlayerEvent.OnItemCheck -= SetItemText;
        PlayerEvent.OnUpdateBullet -= SetInitBullet;
    }

    private void Update()
    {
 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            testSetting.gameObject.SetActive(!testSetting.gameObject.activeSelf);
        }

    }

    #region Swap

    private Sequence currentSequence; 
    private void UpdateQuickSlot(int selectedWeaponIndex)
    {

        //총알 오브젝트 보이기 처리
        if (selectedWeaponIndex == 1)
            FadeOut();
        else
            FadeIn();



        if (currentSequence != null && currentSequence.IsActive())
        {
            currentSequence.Kill(); 
        }


        foreach (var icon in weaponIcons)
        {
            DOTween.Kill(icon); 
        }

     
        foreach (var icon in weaponIcons)
        {
            icon.gameObject.SetActive(true);
            icon.color = whiteColor;
            icon.DOFade(1f, 0f);
        }

        Image selectedIcon = weaponIcons[selectedWeaponIndex];
        selectedIcon.color = choiceColor;

        currentSequence = DOTween.Sequence();
        foreach (var icon in weaponIcons)
        {
            currentSequence.Join(icon.DOFade(0f, 5f));
        }

        currentSequence.OnComplete(() =>
        {
            foreach (var icon in weaponIcons)
            {
                icon.color = whiteColor;
                icon.DOFade(1f, 0f);
                icon.gameObject.SetActive(false);
            }
        });
    }

    void FadeOut()
    {
        GetObject((int)GameObjects.BulletObject).GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.Linear);
    }

    void FadeIn()
    {
        GetObject((int)GameObjects.BulletObject).GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.Linear);
    }

    #endregion

    private Tween pulseTween;
    private void DetectionEyeVisible(bool isVisible)
    {
        GetImage((int)Images.OpenEye).gameObject.SetActive(isVisible);
        GetImage((int)Images.CloseEye).gameObject.SetActive(!isVisible);

        if (isVisible)
        {
            
            if (pulseTween == null || !pulseTween.IsActive())
            {
                pulseTween = GetImage((int)Images.OpenEye).transform.DOScale(1.2f, 0.2f)
                    .SetLoops(-1, LoopType.Yoyo) //루프
                    .SetEase(Ease.InOutSine);   //속도
            }
        }
        else
        {
          
            if (pulseTween != null) pulseTween.Kill();
            pulseTween = null;
            GetImage((int)Images.OpenEye).transform.localScale = Vector3.one;
        }
    }


    private Tween _bloodTween;
    private void UpdateBloodScreen()
    {
        _bloodTween?.Kill();

        Color color = GetImage((int)Images.BloodScreen).color;
        color.a = 0f;
        GetImage((int)Images.BloodScreen).color = color;
        _bloodTween = GetImage((int)Images.BloodScreen).DOFade(0.2f, 0.2f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }



    private void SetItemText(IInteractable interactable)
    {
        GetText((int)Texts.ItemText).gameObject.SetActive(true);
        GetText((int)Texts.ItemText).text = "획득";
    }


    public void SetInitBullet(int maxBullet, int currentBullet)
    {
        GetText((int)Texts.MaxBulletText).text = "/ " + maxBullet.ToString();
        GetText((int)Texts.SpareBulletText).text = currentBullet.ToString();
    }

    public void UpdateCurrentBullet(int current)
    {
        GetText((int)Texts.SpareBulletText).text = current.ToString();
    }


}
