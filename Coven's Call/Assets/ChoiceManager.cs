using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI questionText;
    public Button leftChoiceButton;
    public Button rightChoiceButton;
    public Image timerCircle;

    private float timerDuration = 5f; // seconds
    private float timer;
    private bool isTiming = false;

    private System.Action onLeftAction;
    private System.Action onRightAction;
    private bool choiceMade = false;

    void Update()
    {
        if (isTiming && !choiceMade)
        {
            timer -= Time.deltaTime;
            timerCircle.fillAmount = timer / timerDuration;

            if (timer <= 0)
            {
                isTiming = false;
                int randomChoice = Random.Range(0, 2);
                if (randomChoice == 0)
                    OnLeftChoice();
                else
                    OnRightChoice();
            }
        }
    }

    public void ShowChoices(string question, string leftText, string rightText, System.Action leftAction, System.Action rightAction)
    {
        dialogPanel.SetActive(true);
        questionText.text = question;
        leftChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = leftText;
        rightChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = rightText;

        onLeftAction = leftAction;
        onRightAction = rightAction;

        choiceMade = false;
        StartTimer();

        if (CursorManager.Instance != null)
        {
            CursorManager.Instance.ShowCursor();
        }
    }

    private void StartTimer()
    {
        timer = timerDuration;
        timerCircle.fillAmount = 1f;
        isTiming = true;
    }

    public void OnLeftChoice()
    {
        if (choiceMade) return;
        choiceMade = true;

        isTiming = false;
        dialogPanel.SetActive(false);
        onLeftAction?.Invoke();

        if (CursorManager.Instance != null)
        {
            CursorManager.Instance.HideCursor();
        }
    }

    public void OnRightChoice()
    {
        if (choiceMade) return;
        choiceMade = true;

        isTiming = false;
        dialogPanel.SetActive(false);
        onRightAction?.Invoke();

        if (CursorManager.Instance != null)
        {
            CursorManager.Instance.HideCursor();
        }
    }
}
