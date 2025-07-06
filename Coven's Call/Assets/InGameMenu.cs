using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject exitButton;
    public GameObject menuPanel;

    private void Start()
    {
        menuPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            menuPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        menuPanel.SetActive(false);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}
