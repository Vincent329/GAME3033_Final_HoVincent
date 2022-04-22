using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnHealthInitializeEvent(HealthComponent healthComponent);

    public static event OnHealthInitializeEvent OnHealthInitialized;

    public static void InvokeOnHealthInitialized(HealthComponent healthComponent)
    {
        OnHealthInitialized?.Invoke(healthComponent);
    }
}
