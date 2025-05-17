
using UnityEngine;

public class InitialDialogTrigger : MonoBehaviour
{
    public ChoiceManager choiceManager;

    void Start()
    {
        Invoke("ShowInitialDialog", 5f); // 5 secunde după startul scenei
    }

    void ShowInitialDialog()
    {
        choiceManager.ShowChoices(
            "Choose what to say to Ethan:",
            "I've been waiting for this trip forever.",
            "Let’s hope we don’t get eaten by bears.",
            OnFirstOption,
            OnSecondOption
        );
    }

    void OnFirstOption()
    {
        Debug.Log("Player: I've been waiting for this trip forever.");
        // Aici poți pune codul pentru afisarea replicii tip balon
    }

    void OnSecondOption()
    {
        Debug.Log("Player: Let’s hope we don’t get eaten by bears.");
        // La fel, aici cod pentru balon replică
    }
}
