using UnityEngine;

/*单例MonoBehaviour组件*/
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;                                 //单例
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    GameObject obj = new(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()                              //初始化单例
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}