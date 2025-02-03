using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일한 인스턴스를 담을 변수.
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 인스턴스를 참조하는 property

    private GameManager _game = new GameManager();
    private UIManager _ui = new UIManager();
    private ResourceManger _resource = new ResourceManger();
    private PoolManager _pool = new PoolManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private AudioManager _audio = new AudioManager();

    public static GameManager Game => Instance._game;
    public static UIManager UI => Instance._ui;
    public static ResourceManger Resource => Instance._resource;
    public static PoolManager Pool => Instance._pool;
    public static SceneManagerEx Scene => Instance._scene;
    public static AudioManager Audio => Instance._audio;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject); // 중복된 Managers 제거
            return;
        }

        s_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Init();
    }

    public static void Init()
    {
        if (s_instance != null) return;

        GameObject go = GameObject.Find("@Managers");

        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            s_instance = go.AddComponent<Managers>();
        }
        else
        {
            s_instance = go.GetComponent<Managers>();
        }

        if (!go.activeInHierarchy)
        {
            Debug.LogWarning("@Managers 오브젝트가 비활성화 상태입니다. 활성화 처리합니다.");
            go.SetActive(true);
        }

        DontDestroyOnLoad(go);
    }


    /// <summary>
    /// Scene을 이동할 때 호출해야 하는 함수.
    /// </summary>
    public static void Clear()
    {

    }
}
