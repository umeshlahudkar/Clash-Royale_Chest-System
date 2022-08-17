using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestView : MonoBehaviour
{ 
    public ChestController chestController { get; private set; }
    [SerializeField] private Image chestSprite;
    [SerializeField] private GameObject timer;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI chestStastueText;
    [SerializeField] private TextMeshProUGUI chestName;
    public bool startTimer = false;

    private void Start()
    {
        InitializeChest();
    }
    private void Update()
    {
        if(startTimer && chestController.chestModel.unlockTime >= 0)
        {
            chestController.chestModel.unlockTime -= Time.deltaTime;
            DisplayTime(chestController.chestModel.unlockTime);
        }
    }
    public void InitializeChest()
    {
        timer.SetActive(false);
        chestSprite.sprite = chestController.chestModel.chestSprite;
        DisplayChestName();
        DisplayChestStatus(ChestStastus.Locked);
    }

    public void OnChestClick()
    {
        chestController.OnChestClick();
    }

    public void Diable()
    {
        Destroy(gameObject);
    }

    public void DisplayChestName()
    {
        chestName.text = chestController.chestModel.chestType.ToString();
    }

    public void startUnlockingTimer()
    {
        timer.SetActive(true);
        startTimer = true;
    }

    public void StopTimer()
    {
        timer.SetActive(false);
        startTimer = false;
        chestController.OnUnlockingTimerEnd();
    }

    private void DisplayTime(float time)
    {
        if(time <= 0)
        {
            StopTimer();
            return;
        }

        int minute = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00} : {1 : 00}", minute, second);
    }

    public void DisplayChestStatus(ChestStastus stastus)
    {
        chestStastueText.text = stastus.ToString();
    }
    public void SetChestController(ChestController chestController)
    {
        this.chestController = chestController;
    }
}
