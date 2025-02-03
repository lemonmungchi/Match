using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{

    enum Buttons
    {
        EscapeButton,
    }

    enum Texts
    {
        TimeOutText,
    }

    enum GameObjects
    {
        TimeOutSlider,
    }

    private float gameTime = 60f; // 총 게임 시간


    public override void Init()
    {
        base.Init(); // 상위 클래스의 초기화 메서드 호출

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.TimeOutSlider).GetComponent<Slider>().maxValue = gameTime;
        GetObject((int)GameObjects.TimeOutSlider).GetComponent<Slider>().value = gameTime;
        GetButton((int)Buttons.EscapeButton).gameObject.AddUIEvent(PauseOrResume);

        Managers.Game.OnTimeUpdated += UpdateTimeUI;
    }

    void PauseOrResume(PointerEventData eventData)
    {
        // 1. 뭐든지 열려있으면 다 닫기
        // 2. 아무것도 없으면 열기

        if (Managers.UI.GetStackSize() > 0)
            Managers.UI.CloseAllPopupUI();
        else
            Managers.UI.ShowPopupUI<UI_PausePopup>();
    }

    private void UpdateTimeUI(float time)
    {
        GetText((int)Texts.TimeOutText).text = Mathf.CeilToInt(time).ToString();
        GetObject((int)GameObjects.TimeOutSlider).GetComponent<Slider>().value = time; 
        UpdateTimeColor(time);
    }


    private void UpdateTimeColor(float time)
    {
        float normalizedTime = time / gameTime;
        Color startColor = new Color(0.96f, 0.55f, 0.0f);
        Color endColor = new Color(1.0f, 0.0f, 0.0f);
        Color timeColor = Color.Lerp(endColor, startColor, normalizedTime);

        GetText((int)Texts.TimeOutText).color = timeColor;
        GetObject((int)GameObjects.TimeOutSlider).GetComponent<Slider>().fillRect.GetComponent<Image>().color = timeColor;
    }

    private void OnDisable()
    {
        Managers.Game.OnTimeUpdated -= UpdateTimeUI;
    }
}
