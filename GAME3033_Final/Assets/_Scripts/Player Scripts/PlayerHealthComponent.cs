using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.InvokeOnHealthInitialized(this);
    }

    private void Update()
    {
        TakeDamage(Time.deltaTime);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Destroy()
    {
        Debug.Log("Game Over");
        SceneLoadManager.Instance.LoadLoseScreen();
    }
}
