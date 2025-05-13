using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;
    public static bool IsInstance => _instance != null;

    private static bool applicationIsQutting = false;
    public static T Instance
    {
        get
        {
            if (applicationIsQutting)
            {
                return null;
            }
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    T component = obj.GetComponent<T>();
                    if (component == null)
                    {
                        component=obj.AddComponent<T>();
                    }
                    _instance = component;
                }
                if(_instance is GameManager gameManager)
                {
                    gameManager.Player = GameObject.FindWithTag(Define.PlayerTag).GetComponent<PlayerController>();
                }
                DontDestroyOnLoad(_instance);
            }
          
            return _instance;
        }
    }

    void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    protected void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        Clear();
    }

    protected virtual void Clear()
    {
        
    }

    private void OnDestroy()
    {
        applicationIsQutting = true;
    }
}