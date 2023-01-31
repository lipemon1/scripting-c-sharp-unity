using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopControl : SingletonMonobehavior<GameLoopControl>
{
    [SerializeField] private GameObject m_LevelPrefab;
    private GameObject m_LevelInstance;

    [SerializeField] private GameObject m_PatrolPositionPrefab;
    private GameObject m_PatrolPositionInstance;
    private Transform[] m_PatrolPositions;

    protected override void Awake()
    {
        base.Awake();
        m_LevelInstance = Instantiate(m_LevelPrefab, this.transform, true);
        m_LevelInstance.gameObject.SetActive(false);
        
        m_PatrolPositionInstance = Instantiate(m_PatrolPositionPrefab, this.transform, true);
        m_PatrolPositionInstance.gameObject.SetActive(false);

        List<Transform> patrolPosition = new List<Transform>();
        foreach (Transform patrolChild in m_PatrolPositionInstance.transform)
        {
            patrolPosition.Add(patrolChild);
        }
        m_PatrolPositions = patrolPosition.ToArray();
        
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    public Transform[] GetPatrolPosition()
    {
        return m_PatrolPositions;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy)
            {
                enemy.GetComponent<EnemyStateMachine>().enabled = true;
                enemy.GetComponentInChildren<PlayerCloseDetectionBehavior>().PrepareGameOver(OnGameOver);
            }
            
            m_LevelInstance.gameObject.SetActive(true);
            m_PatrolPositionInstance.gameObject.SetActive(true);
        }
    }
    
    private void OnGameOver()
    {
        GameOverUIController.Instance.Show();
    }
}
