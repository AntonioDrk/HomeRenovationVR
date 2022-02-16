using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button lvl2Btn;
    [SerializeField] private CanvasGroup menuContainer;
    [SerializeField] private CanvasGroup levelsContainer;
    
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
    }


    private void Start()
    {
        if (PlayerPrefs.HasKey("lastLevel"))
        {
            continueBtn.interactable = true;
            if(PlayerPrefs.GetInt("lastLevel") == 2)
                lvl2Btn.interactable = true;
            startBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Start New";
        }
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowLevels()
    {
        menuContainer.alpha = 0f;
        menuContainer.interactable = false;
        menuContainer.blocksRaycasts = false;

        levelsContainer.alpha = 1f;
        levelsContainer.interactable = true;
        levelsContainer.blocksRaycasts = true;
    }

    public void ShowMenu()
    {
        menuContainer.alpha = 1f;
        menuContainer.interactable = true;
        menuContainer.blocksRaycasts = true;

        levelsContainer.alpha = 0f;
        levelsContainer.interactable = false;
        levelsContainer.blocksRaycasts = false;
    }
}
