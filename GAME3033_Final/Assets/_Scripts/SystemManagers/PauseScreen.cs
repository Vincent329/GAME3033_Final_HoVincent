using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button MenuButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuButton.onClick.AddListener(OnMenuPressed);
    }

    private void OnMenuPressed()
    {
        SceneLoadManager.Instance.LoadMainMenu();
    }
}
