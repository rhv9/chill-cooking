using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerClock;

    public void Update()
    {
        timerClock.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
