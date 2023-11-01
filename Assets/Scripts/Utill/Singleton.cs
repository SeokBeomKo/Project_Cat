using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();

    public static bool destroyOnLoad = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] 다수의 싱글톤 객체가 존재하므로 추가 인스턴스를 중지했습니다.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (!Singleton<T>.destroyOnLoad)
                        {
                            DontDestroyOnLoad(singleton);
                        }
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance != null && Equals(Instance, this))
        {
            _instance = null;
        }
    }
}