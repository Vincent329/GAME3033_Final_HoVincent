using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    private PlayerMovement player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<PlayerMovement>();
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

    public override void Heal(float healValue)
    {
        base.Heal(healValue);
        player.GetAudioSource.clip = player.GetAudioClips[2];
        player.GetAudioSource.Play();
    }

    public override void Destroy()
    {
        
        Debug.Log("Game Over");
        SceneLoadManager.Instance.LoadLoseScreen();
    }
}
