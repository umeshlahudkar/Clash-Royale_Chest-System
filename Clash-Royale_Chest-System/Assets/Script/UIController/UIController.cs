using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private ChestController chestController;

    [Header("MSg Display")]
    [SerializeField] private GameObject Display;
    [SerializeField] private TextMeshProUGUI MsgText1;
    [SerializeField] private TextMeshProUGUI MsgText2;


    [Header("Pop-Up Screen")]
    [SerializeField] private GameObject openSlotScreen;
    [SerializeField] private Button openButtonUsingCoin;
    [SerializeField] private Button openButtonUsingGems;
    [SerializeField] private TextMeshProUGUI chestNameText;
    [SerializeField] private TextMeshProUGUI chestStatusText;
    [SerializeField] private TextMeshProUGUI coinButtonText;
    [SerializeField] private TextMeshProUGUI gemButtonText;

    private void Start()
    {
        EventService.Instance.OnChestUnlocked += EnableRewardScreen;
        EventService.Instance.OnChestButtonClicked += OpenScreen;
        EventService.Instance.BroadCastMsg += DisplayMsg;
    }

    private void OnDisable()
    {
        EventService.Instance.OnChestUnlocked -= EnableRewardScreen;
        EventService.Instance.OnChestButtonClicked -= OpenScreen;
        EventService.Instance.BroadCastMsg -= DisplayMsg;
    }

    public void OpenScreen(ChestController chestController)
    {
        this.chestController = chestController;

        if (chestController.chestModel.chestStastus == ChestStastus.Locked)
        {
            EnableOpenSlotScreen();
        }
        else
        {
            EnableStatusScreen();
        }
    }

    private void EnableOpenSlotScreen()
    {
        EnableCoinButton();
        EnableGemButton();
        string msg = "Chest  : " + chestController.chestModel.chestType.ToString();
        EnableChestNameText(msg);
        msg = "Status : " + chestController.chestModel.chestStastus.ToString();
        EnableChestStatusText(msg);
        openSlotScreen.SetActive(true);
    }

    private void EnableStatusScreen()
    {
        string msg = "Chest  : " + chestController.chestModel.chestType.ToString();
        EnableChestNameText(msg);
        msg = "Status : " + chestController.chestModel.chestStastus.ToString();
        EnableChestStatusText(msg);
        openSlotScreen.SetActive(true);
        if(chestController.chestModel.chestStastus != ChestStastus.Waiting)
        {
            EnableGemButton();
        }
    }

    private void EnableRewardScreen(ChestController chestController)
    {
        this.chestController = chestController;
        MsgText1.text = "Reward Coin : " + chestController.GetRewardCoin().ToString();
        MsgText2.text = "Reward Gems : " + chestController.GetRewardGems().ToString();
        MsgText2.gameObject.SetActive(true);
        Display.SetActive(true);
    }

    private void DisplayMsg(string msg)
    {
        Display.gameObject.SetActive(true);
        MsgText2.gameObject.SetActive(false);
        MsgText1.text = msg;
    }

    public void OnCloseButtonClick()
    {
        openButtonUsingCoin.gameObject.SetActive(false);
        openButtonUsingGems.gameObject.SetActive(false);
        chestNameText.gameObject.SetActive(false);
        chestStatusText.gameObject.SetActive(false);
        openSlotScreen.SetActive(false);
        Display.SetActive(false);
    }

    private void EnableCoinButton()
    {
        openButtonUsingCoin.gameObject.SetActive(true);
        coinButtonText.text = chestController.chestModel.unlockTime.ToString() + " sec";
    }

    private void EnableGemButton()
    {
        openButtonUsingGems.gameObject.SetActive(true);
        gemButtonText.text = "Gem : "+ chestController.GetUnlockGems().ToString();
    }

    private void EnableChestNameText(string msg)
    {
        chestNameText.gameObject.SetActive(true);
        chestNameText.text = msg;
    }

    private void EnableChestStatusText(string msg)
    {
        chestStatusText.gameObject.SetActive(true);
        chestStatusText.text = msg;
    }

    public void OnOpenButtonUsingGemsClick()
    {
        ChestService.Instance.UnlockUsingGems(chestController);
        openSlotScreen.SetActive(false);
    }

    public void OnOpenButtonUsingTimerClick()
    {
        ChestService.Instance.UnlockUsingTimer(chestController);
        openSlotScreen.SetActive(false);
    }
}
