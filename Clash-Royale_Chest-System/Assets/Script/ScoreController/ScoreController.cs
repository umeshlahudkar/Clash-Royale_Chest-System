using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int coinCount = 200;
    private int gemCount = 40;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;

    private void Start()
    {
        EventService.Instance.OnChestUnlocked += UpdateCoinCount;
        EventService.Instance.OnChestUnlocked += UpdateGemCount;

        UpdateCoinCount(null);
        UpdateGemCount(null);
    }

    public void UpdateCoinCount(ChestController chestController)
    {
        if(chestController != null)
        {
            coinCount = coinCount + chestController.GetRewardCoin();
        }
        coinText.text = coinCount.ToString();
    }

    public void UpdateGemCount(ChestController chestController)
    {
        if (chestController != null)
        {
            gemCount = gemCount + chestController.GetRewardGems() - chestController.GetUnlockGems();
        }
        gemText.text = gemCount.ToString();
    }
}
