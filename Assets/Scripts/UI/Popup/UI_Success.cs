using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Success : UI_Popup
{
    enum Buttons
    {
        MainBtn,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Managers.Audio.StopBGM();

        GetButton((int)Buttons.MainBtn).gameObject.AddUIEvent(NewGame);

    }

    void NewGame(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        Managers.Scene.ChangeScene(Define.Scene.LobbyScene);
    }
}
