using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestConfigSO", menuName = "Scriptable Object/ChestConfiguration")]
public class ChestConfigurationSO : ScriptableObject
{
    public ChestType chestType;
    public Sprite chestSprite;
    public float unlockTime;
    public float gemsToUnlock;
    public float coinsToUnlock;
    public float rewardMinCoins;
    public float rewardMinGems;
    public float rewardMaxCoins;
    public float rewardMaxGems;
}
