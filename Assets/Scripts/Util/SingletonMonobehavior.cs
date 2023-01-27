using UnityEngine;

public class SingletonMonobehavior<T> : MonoBehaviour where T : SingletonMonobehavior<T> 
{
	public static T Instance { get; private set; }

	protected virtual void Awake()
	{
		if(Instance == null)
		{
			Instance = this as T;
			DontDestroyOnLoad(gameObject);
			gameObject.name = "[Singleton] " + Instance.GetType();
		}
		else
		{
			Destroy(gameObject);
		}
	}
}