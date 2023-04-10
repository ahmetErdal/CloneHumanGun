using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    [Header("Panels")]
    [SerializeField] public GameObject startPanel;
    [SerializeField] public GameObject gameInPanel;
    [SerializeField] public GameObject retryPanel;
    [SerializeField] public GameObject nextLevelPanel;

    [Header("Money Variables")]
    public int moneyCount;
    public TextMeshProUGUI moneyNumText;
    Vector2 anchoredMoneyPos;
    [Header("References")]
    [SerializeField] Camera cam;

    private void Start()
    {
        moneyCount = PlayerPrefs.GetInt("Money",moneyCount);
        PanelController(true, false, false, false);
        moneyNumText.text = moneyCount.ToString();
    }
    public void PanelController(bool startPanelVal, bool gameInPanelVal, bool retryPanelVal, bool nextPanelVal)
    {
        startPanel.SetActive(startPanelVal);
        gameInPanel.SetActive(gameInPanelVal);
        retryPanel.SetActive(retryPanelVal);
        nextLevelPanel.SetActive(nextPanelVal);
    }

   
    public void UpdateMoneyText()
    {
        moneyCount++;
        PlayerPrefs.SetInt("Money", moneyCount);
        moneyNumText.text = PlayerPrefs.GetInt("Money").ToString();
    }

}
