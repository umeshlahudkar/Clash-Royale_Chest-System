using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestService : GenericSingleton<ChestService>
{
    [SerializeField] private ChestView chestView;
    [SerializeField] private int chestUnlockingLimit;
    [SerializeField] private ChestSlot[] slots;
    [SerializeField] private ChestConfigurationSO[] chestsSO;
    [SerializeField] private Queue<ChestSlot> unlockingChestSlots = new Queue<ChestSlot>();

    public void CreateChest()
    {
        ChestSlot slot = GetChestSlot();

        if (slot == null) 
        {
            EventService.Instance.InvokeBroadCastMgdEvent("Slots Full");
            return;
        }

        ChestConfigurationSO chestSO = chestsSO[UnityEngine.Random.Range(0, (chestsSO.Length - 1))];
        ChestModel chestModel = new ChestModel(chestSO, slot);
        ChestController chestController = new ChestController(chestModel, chestView, slot.slotTransform);
        slot.chestController = chestController;
        slot.IsUsed = true;
    }

    public void UnlockUsingGems(ChestController chestController)
    {
        chestController.UnlockUsingGems();
        EventService.Instance.InvokeOnChestUnlockedEvent(chestController);
    }

    public void UnlockUsingTimer(ChestController chestController)
    {
        if (unlockingChestSlots.Count >= chestUnlockingLimit)
        {
            EventService.Instance.InvokeBroadCastMgdEvent("Unlocking Limit Over");
            return;
        }

        if (unlockingChestSlots.Count >= 1 )
        {
            unlockingChestSlots.Enqueue(chestController.chestModel.chestSlot);
            chestController.UpdateStatus(ChestStastus.Waiting);
            EventService.Instance.InvokeBroadCastMgdEvent("Added to Waiting List");
        }
        else
        {
            chestController.UnlockUsingTimer();
            unlockingChestSlots.Enqueue(chestController.chestModel.chestSlot);
            EventService.Instance.InvokeBroadCastMgdEvent("Added to Unlocking List");
        }
    }

    public void startUnlockingNextChest(ChestController chestController)
    {
        if(unlockingChestSlots.Count > 0 && chestController == unlockingChestSlots.Peek().chestController)
        {
            unlockingChestSlots.Dequeue();
            if (unlockingChestSlots.Count > 0)
            {
                unlockingChestSlots.Peek().chestController.UnlockUsingTimer();
            }
        }
    }

    private ChestSlot GetChestSlot()
    {
        ChestSlot slot = Array.Find(slots, item => item.IsUsed == false);
        if(slot != null)
        {
            return slot;
        }
        return null;
    }
}

[Serializable]
public class ChestSlot
{
    public RectTransform slotTransform;
    public ChestController chestController = null;
    public bool IsUsed;
}

