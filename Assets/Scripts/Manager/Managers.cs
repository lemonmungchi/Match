using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // ������ �ν��Ͻ��� ���� ����.
    public static Managers Instance { get { Init(); return s_instance; } } // ������ �ν��Ͻ��� �����ϴ� property

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
            Destroy(gameObject); // �ߺ��� Managers ����
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
            Debug.LogWarning("@Managers ������Ʈ�� ��Ȱ��ȭ �����Դϴ�. Ȱ��ȭ ó���մϴ�.");
            go.SetActive(true);
        }

        DontDestroyOnLoad(go);
    }


    /// <summary>
    /// Scene�� �̵��� �� ȣ���ؾ� �ϴ� �Լ�.
    /// </summary>
    public static void Clear()
    {

    }
}
