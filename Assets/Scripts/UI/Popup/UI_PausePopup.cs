using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PausePopup : UI_Popup
{
    enum Images
    {
        Background,
    }

    enum Buttons
    {
        BacktoMainMenuBtn,
        ExitBtn,
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Managers.Audio.StopBGM();
        GetImage((int)Images.Background).gameObject.AddUIEvent(ClosePopupUI);
        GetButton((int)Buttons.BacktoMainMenuBtn).gameObject.AddUIEvent(ClosePopupUI);
        GetButton((int)Buttons.ExitBtn).gameObject.AddUIEvent(BacktoMainMenu);

        Time.timeScale = 0.0f;     // 일시정지
    }

    public override void ClosePopupUI(PointerEventData action)
    {
        Time.timeScale = 1.0f;
        
        base.ClosePopupUI(action);
    }
    
    void BacktoMainMenu(PointerEventData action)
    {
        ClearDontDestroyOnLoad();
        Managers.Scene.ChangeScene(Define.Scene.LobbyScene);
        Time.timeScale = 1.0f;
    }

    private void ClearDontDestroyOnLoad()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.scene.name == "DontDestroyOnLoad")
            {
                // 특정 객체를 제외하고 삭제
                if (go.name== "AudioManager"|| go.name=="@Managers") // Managers는 유지할 경우
                {
                    GameObject.Destroy(go);
                }
            }
        }
    }


    public void ExitGame(PointerEventData action)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
        Time.timeScale = 1.0f;
    }
    
}
