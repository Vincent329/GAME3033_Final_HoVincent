using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You Win");
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadWinScreen();
        }
    }
}
