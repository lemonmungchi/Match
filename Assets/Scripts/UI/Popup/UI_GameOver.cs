using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    
    enum Buttons
    {
        ReTryBtn,
    }

  

    public override void Init()
    {
        base.Init();
        
        Bind<Button>(typeof(Buttons));

        Managers.Audio.StopBGM();

        GetButton((int)Buttons.ReTryBtn).gameObject.AddUIEvent(NewGame);


        Time.timeScale = 0.0f;
    }

    void NewGame(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        Managers.Scene.ChangeScene(Define.Scene.LobbyScene);
    }

}
