using UnityEngine;

public class ChestController
{
    public ChestModel chestModel { get; private set; }
    public ChestView chestView { get; private set; }
    

    public ChestController(ChestModel chestModel, ChestView chestView, RectTransform spwanTransform)
    {
        this.chestModel = chestModel;
        this.chestView = GameObject.Instantiate<ChestView>(chestView, spwanTransform);

        this.chestView.SetChestController(this);
    }

    public void OnChestClick()
    {
        switch (chestModel.chestStastus)
        {
            case ChestStastus.Open:
                ChestService.Instance.UnlockUsingGems(this);
                break;

            default:
                EventService.Instance.InvokeOnChestButtonEvent(this);
                break;
        }
    }

    public void UnlockUsingGems()
    {
        ChestService.Instance.startUnlockingNextChest(this);
        chestModel.chestSlot.IsUsed = false;
        chestModel.chestSlot.chestController = null;
        chestView.Diable();
    }

    public void UnlockUsingTimer()
    {
        UpdateStatus(ChestStastus.Unlocking);
        chestView.startUnlockingTimer();
    }

    public void OnUnlockingTimerEnd()
    {
        UpdateStatus(ChestStastus.Open);
        ChestService.Instance.startUnlockingNextChest(this);
    }
    public void UpdateStatus(ChestStastus chestStastus)
    {
        chestModel.chestStastus = chestStastus;
        chestView.DisplayChestStatus(chestModel.chestStastus);
    }

    public int GetRewardCoin()
    {

        int rewardCoin = (int)Random.Range(chestModel.minCoinRewards, chestModel.maxCoinRewards);
        return rewardCoin;
    }

    public int GetRewardGems()
    {
        int rewardGems = (int)Random.Range(chestModel.minGemRewards, chestModel.maxGemRewards);
        return rewardGems;
    }

    public int GetUnlockGems()
    {
        if(chestModel.chestStastus == ChestStastus.Open)
        {
            return 0;
        }

        int gems = (Mathf.RoundToInt(chestModel.unlockTime / 10));
        return gems;
    }
}

