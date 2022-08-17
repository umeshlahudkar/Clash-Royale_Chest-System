using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModel 
{
    public Sprite chestSprite { get; private set; }
    public ChestType chestType { get; private set; }
    public ChestStastus chestStastus { get; set; }
    public float unlockTime { get; set; }
    public float initialUnlockTime { get; private set; }
    public float minCoinRewards { get; private set; }
    public float minGemRewards { get; private set; }
    public float maxCoinRewards { get; private set; }
    public float maxGemRewards { get; private set; }
    public float gemsToUnlock { get; private set; }
    public ChestSlot chestSlot { get; private set; }

    public ChestModel(ChestConfigurationSO chestSO, ChestSlot chestSlot)
    {
        chestSprite = chestSO.chestSprite;
        chestType = chestSO.chestType;
        chestStastus = ChestStastus.Locked;
        unlockTime = initialUnlockTime = chestSO.unlockTime;
        minCoinRewards = chestSO.rewardMinCoins;
        maxCoinRewards = chestSO.rewardMaxCoins;
        minGemRewards = chestSO.rewardMinGems;
        maxCoinRewards = chestSO.rewardMaxGems;
        gemsToUnlock = chestSO.gemsToUnlock;
        this.chestSlot = chestSlot;
    }
}
