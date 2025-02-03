using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        StartBtn,
        //CreditBtn,
    }


    public override void Init()
    {


        base.Init();


        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.StartBtn).gameObject.AddUIEvent(NewGame);
        //GetButton((int)Buttons.CreditBtn).gameObject.AddUIEvent(Credit);
    }


    void NewGame(PointerEventData data)
    {
        Managers.Scene.ChangeScene(Define.Scene.GameScene);
    }

    void Credit(PointerEventData data)
    {
       
    }

}