using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button MenuButton;
    // Start is called before the first frame update
    void Start()
    {
        RetryButton.onClick.AddListener(OnRetryPressed);
        MenuButton.onClick.AddListener(OnMenuPressed);
        AppEvents.InvokeOnMouseCursorEnable(true);
    }

    private void OnRetryPressed()
    {
        SceneLoadManager.Instance.LoadGame();
    }
    private void OnMenuPressed()
    {
        SceneLoadManager.Instance.LoadMainMenu();
    }
}
