using UnityEngine;

/// <summary>
/// 게임 씬을 관리하는 샘플 클래스.
/// BaseScene을 상속받아 게임 씬에 특화된 초기화 및 정리 로직을 구현.
/// </summary>
public class LobbyScene : BaseScene
{
    /// <summary>
    /// 씬의 초기화를 담당하는 메서드.
    /// BaseScene의 Init 메서드를 호출하여 기본적인 초기화를 수행한 후,
    /// 게임 씬에 필요한 추가적인 초기화 작업을 여기에서 수행.
    /// </summary>
    protected override void Init()
    {
        base.Init(); // 상위 클래스의 초기화 메서드 호출

        // 현재 씬의 타입을 게임 씬으로 설정.
        SceneType = Define.Scene.LobbyScene;
        Managers.UI.ShowSceneUI<UI_Lobby>();
        //DebugEx.Log("MainMenu");
    }

    /// <summary>
    /// 씬이 전환될 때 필요한 정리 작업을 수행하는 메서드.
    /// 게임 씬에서 사용된 리소스의 해제, 이벤트 리스너의 제거 등을 수행할 수 있음.
    /// 현재는 구체적인 내용이 구현되지 않음.
    /// </summary>
    public override void Clear()
    {
        // 게임 씬에서 필요한 정리 작업을 여기에 구현.
        // 예를 들어, 씬 전환 시에 특정 게임 오브젝트를 파괴하거나,
        // Managers 등을 통해 설정된 데이터를 초기화할 수 있음.
    }

}
