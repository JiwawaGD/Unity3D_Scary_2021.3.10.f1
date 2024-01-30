﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] [Header("下個載入場景的 ID")] int iNextSceneID;

    [SerializeField] [Header("進入遊戲 按鈕")] Button Btn_EnterGame;
    [SerializeField] [Header("遊戲設定 按鈕")] Button Btn_GameSetting;
    [SerializeField] [Header("製作人員 按鈕")] Button Btn_Team;
    [SerializeField] [Header("離開按鈕")] Button Btn_EndGame;
    [SerializeField] [Header("設定目前選擇按鈕")] GameObject GameSettingCurrentChoose;
    [SerializeField] [Header("製作人員目前選擇按鈕")] GameObject TeamCurrentChoose;

    void Start()
    {
        if (Btn_EnterGame == null)
            Btn_EnterGame = GameObject.Find("MenuCanvas/EnterGame").GetComponent<Button>();

        if (Btn_GameSetting == null)
            Btn_GameSetting = GameObject.Find("MenuCanvas/Setting/SettingBtn").GetComponent<Button>();

        if (Btn_Team == null)
            Btn_Team = GameObject.Find("MenuCanvas/Team/TeamBtn").GetComponent<Button>();

        if (Btn_EndGame == null)
            Btn_EndGame = GameObject.Find("MenuCanvas/EndGame").GetComponent<Button>();
    }

    public void ShowAllBtn()
    {
        Btn_EnterGame.gameObject.SetActive(true);
        Btn_GameSetting.gameObject.SetActive(true);
        Btn_Team.gameObject.SetActive(true);
        Btn_EndGame.gameObject.SetActive(true);
    }

    public void HideAllBtn()
    {
        Btn_EnterGame.gameObject.SetActive(false);
        Btn_GameSetting.gameObject.SetActive(false);
        Btn_Team.gameObject.SetActive(false);
        Btn_EndGame.gameObject.SetActive(false);
    }

    public void ResetCurrentChoose(string mode)
    {
        if(mode == "team") EventSystem.current.SetSelectedGameObject(TeamCurrentChoose);
        else EventSystem.current.SetSelectedGameObject(GameSettingCurrentChoose);
    }

    public void SetCurrentChoose(string mode)
    {
        if (mode == "team") TeamCurrentChoose = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        else GameSettingCurrentChoose = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; ;
    }
}