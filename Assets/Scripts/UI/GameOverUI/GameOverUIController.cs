using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : SingletonMonobehavior<GameOverUIController>
{
	[SerializeField]
	private GameObject m_GameOverUI;
	[SerializeField]
	private Button m_RestartButton;
	[SerializeField]
	private Button m_QuitButton;

	protected override void Awake()
	{
		base.Awake();
		
		m_RestartButton.onClick.AddListener(OnRestartButtonClicked);
		m_QuitButton.onClick.AddListener(OnQuitButtonClicked);
		
		m_GameOverUI.SetActive(false);
	}
	
	public void Show()
	{
		m_GameOverUI.SetActive(true);
	}
	
	private void Hide()
	{
		m_GameOverUI.SetActive(false);
	}

	private void OnQuitButtonClicked()
	{
		Application.Quit();
	}

	private void OnRestartButtonClicked()
	{
		Hide();
		SceneManager.LoadScene(1);
	}
}
