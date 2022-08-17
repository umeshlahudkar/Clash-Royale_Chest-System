using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventService : GenericSingleton<EventService>
{
    public event Action<ChestController> OnChestUnlocked;
    public event Action<ChestController> OnChestButtonClicked;
    public event Action<string> BroadCastMsg;

    public void InvokeOnChestUnlockedEvent(ChestController chestController)
    {
        OnChestUnlocked?.Invoke(chestController);
    }

    public void InvokeOnChestButtonEvent(ChestController chestController)
    {
        OnChestButtonClicked?.Invoke(chestController);
    }

    public void InvokeBroadCastMgdEvent(string msg)
    {
        BroadCastMsg?.Invoke(msg);
    }
}
