using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button StartGameButton;
    [SerializeField] private Button InstructionsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] private GameObject Instructions;
    [SerializeField] private GameObject Credits;

    bool toggleInstructions;
    bool toggleCredits;

    private void Start()
    {
        StartGameButton.onClick.AddListener(OnStartPressed);
        InstructionsButton.onClick.AddListener(OnInstructionsPressed);
        CreditsButton.onClick.AddListener(OnCreditsPressed);
        QuitButton.onClick.AddListener(OnQuitPressed);
        toggleCredits = false;
        toggleInstructions = false;
    }

    private void OnStartPressed()
    {
        SceneLoadManager.Instance.LoadGame();
    }
    private void OnQuitPressed()
    {
        Application.Quit();
    }

    private void OnInstructionsPressed()
    {
        if (toggleCredits)
        {
            toggleCredits = false;
            Credits.SetActive(toggleCredits);
        }
        toggleInstructions = !toggleInstructions;
        Instructions.SetActive(toggleInstructions);
    }

    private void OnCreditsPressed()
    {
        if (toggleInstructions)
        {
            toggleInstructions = false;
            Instructions.SetActive(toggleInstructions);
        }
        toggleCredits = !toggleCredits;
        Credits.SetActive(toggleCredits);
    }
}
