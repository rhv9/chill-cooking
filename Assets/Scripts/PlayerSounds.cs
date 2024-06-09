using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimerMax = .1f;
    private float footstepTimer;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Update()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
