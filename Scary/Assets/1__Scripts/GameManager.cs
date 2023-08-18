using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEditor.Rendering.CameraUI;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public partial class GameManager : MonoBehaviour
{
    [Header("全域變數")] public Volume postProcessVolume;
    [Header("濾鏡效果種類")] public VolumeProfile[] profile;
    [Header("可查看觸發物件")] public GameObject[] itemObj;
    [Header("物件位置")] public GameObject itemObjTransform;

    [SerializeField] [Header("玩家")] PlayerController playerCtrlr;
    //[SerializeField] [Header("UI 圖片庫")] Sprite[] UISprite;
    [SerializeField] [Header("Flowchart")] GameObject[] flowchartObjects;
    [SerializeField] [Header("設定頁面")] public GameObject settingObjects;
    //[SerializeField] [Header("音效撥放清單")] AudioClip[] audioClip;
    //[SerializeField] [Header("音效撥放器")] AudioSource[] audioSources;
    //[SerializeField] [Header("GM 欄位腳本")] GMField gmField;

    int m_iGrandmaRushCount;

    Transform tfGrandmaGhost;
    Scene currentScene;

    ItemController TempItem;

    #region Canvas Zone
    [SerializeField] GameObject goCanvas;
    [SerializeField] Image imgUIBackGround;
    //[SerializeField] Image imgUIDisplay;
    //[SerializeField] Image titleImg;
    [SerializeField] Text txtTitle;

    [SerializeField] Image imgInstructions;
    [SerializeField] Text txtInstructions;
    //[SerializeField] Image imgScendInstructions;
    //[SerializeField] Image imgIntroduceBackground;
    [SerializeField] Text txtIntroduce;

    [SerializeField] Button ExitBtn;
    [SerializeField] Text txtEnterGameHint;
    [SerializeField] Button EnterGameBtn;
    #endregion

    #region Light Zone
    public GameObject goPhotoFrameLight;
    #endregion

    #region Static Boolean Zone
    public static bool m_bInUIView = false;
    public static bool m_bIsEnterGameView = false;
    public static bool m_bShowPlayerAnimate = false;
    public static bool m_bShowItemAnimate = false;
    public static bool m_bShowDialog = false;

    public static bool m_bPhotoFrameLightOn = false;
    public static bool m_bGrandmaRush = false;
    public static bool m_bReturnToBegin = false;
    public static bool m_bPlayLotusEnable = false;
    public static bool m_bToiletGhostHasShow = false;
    #endregion

    bool isPaused = false;
    bool isMouseEnabled = false;
    bool bTriggerFlashlight = false;
    bool bTriggerGrandmaDoorLock = false;
    public bool isUIOpen = false;

    void Awake()
    {
        if (playerCtrlr == null)
            playerCtrlr = GameObject.Find("Player").GetComponent<PlayerController>();

        if (goCanvas == null)
            goCanvas = GameObject.Find("UI Canvas");

        imgUIBackGround = goCanvas.transform.GetChild(0).GetComponent<Image>();
        //imgUIDisplay = goCanvas.transform.GetChild(1).GetComponent<Image>();
        //titleImg = goCanvas.transform.GetChild(2).GetComponent<Image>();
        txtTitle = goCanvas.transform.GetChild(2).GetComponent<Text>();

        imgInstructions = goCanvas.transform.GetChild(3).GetComponent<Image>();
        txtInstructions = goCanvas.transform.GetChild(3).GetComponentInChildren<Text>();
        //imgScendInstructions = goCanvas.transform.GetChild(2).GetComponentInChildren<Image>();

        //imgIntroduceBackground = goCanvas.transform.GetChild(4).GetComponent<Image>();
        txtIntroduce = goCanvas.transform.GetChild(4).GetComponentInChildren<Text>();

        ExitBtn = goCanvas.transform.GetChild(5).GetComponent<Button>();

        txtEnterGameHint = goCanvas.transform.GetChild(6).GetComponent<Text>();
        EnterGameBtn = goCanvas.transform.GetChild(7).GetComponent<Button>();

        TempItem = null;

        currentScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        GameEvent(GameEventID.Close_UI);

        ExitBtn.onClick.AddListener(() => ButtonFunction(ButtonEventID.UI_Back));
        EnterGameBtn.onClick.AddListener(() => ButtonFunction(ButtonEventID.Enter_Game));

        ShowHint(HintItemID.S1_Light_Switch);
        ShowHint(HintItemID.S1_Grandma_Room_Door_Lock);
    }

    void Update()
    {
        KeyboardCheck();

        if (isPaused && isMouseEnabled)
        {
            MouseCheck();
        }

        if (isUIOpen && Input.GetKeyDown(KeyCode.R))
        {
            ButtonFunction(ButtonEventID.Enter_Game);
        }

        //if (m_bInUIView && Input.GetKeyDown(KeyCode.N))
        //    imgIntroduceBackground.gameObject.SetActive(true);
    }

    public void GameEvent(GameEventID r_eventID)
    {
        switch (r_eventID)
        {
            case GameEventID.Close_UI:
                UIState(UIItemID.Empty, false);
                ShowEnterGame(false);
                AUDManager.instance.PlayerGameEventSFX();

                // UI 返回後執行玩家動畫
                if (m_bShowPlayerAnimate)
                    ProcessPlayerAnimator(GlobalDeclare.GetPlayerAnimateType().ToString());

                // UI 返回後執行 Item 動畫
                if (m_bShowItemAnimate)
                    ProcessAnimator(GlobalDeclare.GetItemAniObject(), GlobalDeclare.GetItemAniName());

                // UI 返回後執行 Fungus 對話
                if (m_bShowDialog)
                    ProcessDialog(GlobalDeclare.GetDialogObjName());

                GameStateCheck();
                break;
            case GameEventID.S1_Photo_Frame:
                UIState(UIItemID.S1_Photo_Frame, true);
                //flowchartObjects[9].gameObject.SetActive(true);

                /* 1.0 版相框後續處理
                Transform tfPlayer = playerCtrlr.transform;
                tfPlayer.position = new Vector3(-4.5f, 0.8f, 1f);
                tfPlayer.rotation = Quaternion.Euler(0, 180, 0);
                tfPlayer.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);

                // Show grandma
                tfGrandmaGhost = GameObject.Find("Grandma_Ghost").transform;
                ParticleSystem psMist_Partical = GameObject.Find("Mist_Partical").GetComponent<ParticleSystem>();
                tfGrandmaGhost.Translate(0f, 100f, 0f);
                psMist_Partical.Play();

                m_bGrandmaRush = true;
                m_bShowPlayerAnimate = true;
                GlobalDeclare.SetPlayerAnimateType(PlayerAnimateType.Player_Turn_After_Photo_Frame);

                GameObject LieGrandmaObj = GameObject.Find("Lie_Grandma_Body");
                Destroy(LieGrandmaObj);*/

                // 相框UI(紹威)


                // 人形黑影
                ProcessAnimator("Toilet_Door_Ghost", "Toilet_Door_Ghost_In");
                m_bToiletGhostHasShow = true;
                playerCtrlr.m_bLimitRotation = true;
                playerCtrlr.m_fHorizantalRotationRange.x = 150f;
                playerCtrlr.m_fHorizantalRotationRange.y = 200f;
                playerCtrlr.tfTransform.localEulerAngles = Vector3.up * 169f;
                break;
            case GameEventID.S1_Grandma_Door_Open:
                ProcessAnimator("Grandma_Room_Door", "DoorOpen");
                AUDManager.instance.PlayerDoorOpenSFX();
                ShowHint(HintItemID.S1_Rice_Funeral);
                flowchartObjects[4].gameObject.SetActive(true);
                break;
            case GameEventID.S1_Lotus_Paper:
                UIState(UIItemID.S1_Lotus_Paper, true);
                ShowEnterGame(true);
                AUDManager.instance.PlayerLotusPaperSFX();
                //if (r_bEnable && isUIOpen && Input.GetKeyDown(KeyCode.E))
                //{
                //    ButtonFunction(ButtonEventID.Enter_Game);
                //}
                break;
            case GameEventID.S1_Grandma_Dead_Body:
                UIState(UIItemID.S1_Grandma_Dead_Body, true);
                flowchartObjects[6].gameObject.SetActive(true);

                GameObject RiceFuneralObj = GameObject.Find("Rice_Funeral");
                Destroy(RiceFuneralObj);

                Object RiceFuneralSpilled = Resources.Load<GameObject>("Prefabs/Rice_Funeral_Spilled");
                GameObject RiceFuneralSpilledObj = Instantiate(RiceFuneralSpilled) as GameObject;
                RiceFuneralSpilledObj.transform.position = new Vector3(-4.4f, 0.006f, 11.8f);
                RiceFuneralSpilledObj.name = "Rice_Funeral_Spilled";

                ShowHint(HintItemID.S1_Rice_Funeral_Spilled);
                break;
            case GameEventID.S1_White_Tent:
                ProcessAnimator("Filial_Piety_Curtain", "Filial_piety_curtain Open");
                BoxCollider curtain = GameObject.Find("Filial_Piety_Curtain").GetComponent<BoxCollider>();
                curtain.enabled = false;
                ShowHint(HintItemID.S1_Lie_Grandma_Body);
                AUDManager.instance.PlayerWhiteTentSFX();
                break;
            case GameEventID.S1_Photo_Frame_Light_On:
                goPhotoFrameLight.SetActive(true);
                m_bPhotoFrameLightOn = false;
                break;
            case GameEventID.S1_Grandma_Rush:
                InvokeRepeating(nameof(GrandMaRush), 0f, 0.025f);
                AUDManager.instance.PlayerGrandmaRushSFX();
                playerCtrlr.m_bCanControl = false;
                Animator AniGrandma = tfGrandmaGhost.GetComponent<Animator>();
                AniGrandma.SetBool("Grandma_Attack", true);
                m_bGrandmaRush = false;
                break;
            case GameEventID.S1_Light_Switch:
                flowchartObjects[2].gameObject.SetActive(true);
                AUDManager.instance.PlayerLightSwitchSFX();
                ShowHint(HintItemID.S1_Flashlight);
                break;
            case GameEventID.S1_Flashlight:
                bTriggerFlashlight = true;
                ShowHint(HintItemID.S1_Desk_Drawer);
                Light playerFlashlight = playerCtrlr.tfPlayerCamera.GetComponent<Light>();
                playerFlashlight.enabled = true;
                AUDManager.instance.PlayerLightSwitchSFX();
                GameObject FlashLight = GameObject.Find("Flashlight");
                Destroy(FlashLight);
                break;
            case GameEventID.S1_Desk_Drawer:
                BoxCollider DrawerCollider = GameObject.Find("grandpa_desk/Desk_Drawer").GetComponent<BoxCollider>();
                DrawerCollider.enabled = false;
                ProcessAnimator("grandpa_desk/Desk_Drawer", "DrawerWithKey_Open");
                Invoke(nameof(IvkShowDoorKey), 1.2f);
                AUDManager.instance.OpenTheDrawerSFX();
                break;
            case GameEventID.S1_GrandmaRoomKey:
                ShowHint(HintItemID.S1_Grandma_Room_Door);
                AUDManager.instance.GetTheKeySFX();
                flowchartObjects[3].gameObject.SetActive(true);
                GameObject GrandmaRoomKeyObj = GameObject.Find("Grandma_Room_Key");
                Destroy(GrandmaRoomKeyObj);
                break;
            case GameEventID.S1_Grandma_Room_Door_Lock:
                bTriggerGrandmaDoorLock = true;
                ShowHint(HintItemID.S1_Desk_Drawer);
                flowchartObjects[1].gameObject.SetActive(true);
                AUDManager.instance.PlayerDoorLockSFX();
                break;
            case GameEventID.S1_Rice_Funeral_Spilled:
                // 查看腳尾飯後的行為
                // 1. 亮蠟燭
                ShowHint(HintItemID.S1_Lotus_Paper);
                m_bPlayLotusEnable = true;
                flowchartObjects[8].gameObject.SetActive(true);
                break;
            case GameEventID.S1_Rice_Funeral:
                ShowHint(HintItemID.S1_Filial_Piety_Curtain);
                flowchartObjects[11].gameObject.SetActive(true);
                UIState(UIItemID.S1_Grandma_Dead_Body, true);
                ShowObj(ObjItemID.S1_Rice);
                break;
            case GameEventID.S1_Toilet_Door_Lock:
                Debug.Log("廁所門被鎖住了");
                break;
            case GameEventID.S1_Toilet_Door_Open:
                ProcessAnimator("Toilet_Door_Ghost", "Toilet_Door_Open");
                ShowHint(HintItemID.S1_Photo_Frame);
                BoxCollider ToiletDoorCollider = GameObject.Find("Toilet_Door_Ghost").GetComponent<BoxCollider>();
                ToiletDoorCollider.enabled = false;
                break;
            case GameEventID.S1_Toilet_Ghost_Hide:
                ProcessAnimator("Toilet_Door_Ghost", "Toilet_Door_Ghost_Out");
                m_bToiletGhostHasShow = false;
                playerCtrlr.m_bLimitRotation = false;
                break;
        }
    }

    // 顯示眼睛 Hint 圖示
    public void ShowHint(HintItemID _ItemID)
    {
        switch (_ItemID)
        {
            case HintItemID.S1_Light_Switch:
                TempItem = GameObject.Find("Light_Switch").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Grandma_Room_Door:
                TempItem = GameObject.Find("Grandma_Room_Door").GetComponent<ItemController>();
                TempItem.gameObject.layer = LayerMask.NameToLayer("InteractiveItem");
                TempItem.eventID = GameEventID.S1_Grandma_Door_Open;
                break;
            case HintItemID.S1_Flashlight:
                TempItem = GameObject.Find("Flashlight").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Desk_Drawer:
                if (!bTriggerGrandmaDoorLock || !bTriggerFlashlight)
                    return;

                TempItem = GameObject.Find("Desk_Drawer").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Grandma_Room_Key:
                TempItem = GameObject.Find("Grandma_Room_Key").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Filial_Piety_Curtain:
                TempItem = GameObject.Find("Filial_Piety_Curtain").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Lie_Grandma_Body:
                TempItem = GameObject.Find("Lie_Grandma_Body").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Rice_Funeral:
                TempItem = GameObject.Find("Rice_Funeral").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Lotus_Paper:
                TempItem = GameObject.Find("Lotus_Paper").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Grandma_Room_Door_Lock:
                TempItem = GameObject.Find("Grandma_Room_Door").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Rice_Funeral_Spilled:
                TempItem = GameObject.Find("Rice_Funeral_Spilled").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Photo_Frame:
                TempItem = GameObject.Find("Photo_Frame").GetComponent<ItemController>();
                break;
            case HintItemID.S1_Toilet_Door:
                TempItem = GameObject.Find("Toilet_Door_Ghost").GetComponent<ItemController>();
                break;
        }

        TempItem.bActive = true;
        TempItem.SetHintable(true);
    }

    /// <summary>
    /// 旋轉物件
    /// </summary>
    /// <param name="O_ItemID"></param>
    public void ShowObj(ObjItemID O_ItemID)
    {
        switch (O_ItemID)
        {
            case ObjItemID.S1_Rice:
                Instantiate(itemObj[0], itemObjTransform.transform.position, itemObjTransform.transform.rotation);
                break;
        }
    }

    public void UIState(UIItemID r_ItemID, bool r_bEnable)
    {
        m_bInUIView = r_bEnable;
        playerCtrlr.m_bCanControl = !r_bEnable;
        playerCtrlr.SetCursor();

        goCanvas.SetActive(r_bEnable);
        ExitBtn.gameObject.SetActive(r_bEnable);
        imgUIBackGround.color = r_bEnable ? new Color(0, 0, 0, .02f) : new Color(0, 0, 0, 0);
        //imgUIDisplay.color = r_bEnable ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        imgInstructions.color = r_bEnable ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);
        int iItemID = (int)r_ItemID;

        //imgUIDisplay.sprite = UISprite[iItemID];
        txtTitle.text = GlobalDeclare.UITitle[iItemID];

        txtIntroduce.text = GlobalDeclare.UIIntroduce[iItemID];
        txtInstructions.text = GlobalDeclare.TxtInstructionsmage[iItemID];

        //titleImg.color = r_bEnable ? new Color(63, 0, 0, .18f) : new Color(63, 0, 0, 0);
        //imgScendInstructions.color = r_bEnable ? new Color(255, 255, 255, 1) : new Color(255, 255, 255, 0);
        //imgIntroduceBackground.color = r_bEnable ? new Color(108, 106, 106, 1) : new Color(108, 106, 106, 0);
        //imgIntroduceBackground.gameObject.SetActive(false);
    }

    public void ProcessAnimator(string r_sObject, string r_sTriggerName)
    {
        if (r_sObject.Contains("null") || r_sTriggerName.Contains("null"))
            return;

        GameObject obj = GameObject.Find(r_sObject);
        Animator ani = obj.transform.GetComponent<Animator>();
        ani.SetTrigger(r_sTriggerName);
        obj.transform.GetComponent<ItemController>().SetHintable(false);
        obj.transform.GetComponent<ItemController>().bActive = false;

        GlobalDeclare.SetItemAniObject("Empty");
        GlobalDeclare.SetItemAniName("Empty");
        m_bShowItemAnimate = false;
    }

    public void ProcessPlayerAnimator(string r_sAnimationName)
    {
        Animation am = playerCtrlr.GetComponent<Animation>();
        am.Play(r_sAnimationName);
        m_bShowPlayerAnimate = false;
        GlobalDeclare.SetPlayerAnimateType(PlayerAnimateType.Empty);
    }

    public void ProcessDialog(string sDialogObjName)
    {
        if (sDialogObjName.Contains("Empty"))
            return;

        GameObject dialog = GameObject.Find(sDialogObjName);
        dialog.gameObject.SetActive(true);
        m_bShowDialog = false;
        GlobalDeclare.SetDialogObjName("Empty");
    }

    public void ShowEnterGame(bool r_bEnable)
    {
        isUIOpen = r_bEnable;
        EnterGameBtn.gameObject.SetActive(r_bEnable);
        txtEnterGameHint.gameObject.SetActive(r_bEnable);
        txtEnterGameHint.text = r_bEnable ? "---- R進入遊戲 ----" : "";
    }

    public void ButtonFunction(ButtonEventID _eventID)
    {
        switch (_eventID)
        {
            case ButtonEventID.UI_Back:
                GameEvent(GameEventID.Close_UI);
                break;
            case ButtonEventID.Enter_Game:
                if (isUIOpen)
                {
                    // 執行相應的程式碼
                    GlobalDeclare.bLotusGameComplete = true;
                    GameEvent(GameEventID.Close_UI);
                    playerCtrlr.m_bCanControl = false;
                    playerCtrlr.tfPlayerCamera.gameObject.SetActive(false);
                    SceneManager.LoadScene(3, LoadSceneMode.Additive);
                }
                break;
        }
    }

    public void GrandMaRush()
    {
        tfGrandmaGhost.Translate(0f, 0f, 0.3f);
        m_iGrandmaRushCount++;

        if (m_iGrandmaRushCount >= 10)
        {
            CancelInvoke(nameof(GrandMaRush));
            playerCtrlr.m_bCanControl = false;
            goCanvas.SetActive(true);
            imgUIBackGround.color = new Color(0, 0, 0, 0.95f);
            Invoke(nameof(IvkShowGrandmaFaceUI), 0.5f);
        }
    }

    public void ExitLotusGame()
    {
        m_bPlayLotusEnable = false;
        playerCtrlr.m_bCanControl = true;
        playerCtrlr.tfPlayerCamera.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(3);

        Object Lotus_state_Final = Resources.Load<GameObject>("Prefabs/Lotus_state_Final");
        GameObject LotusObj = Instantiate(Lotus_state_Final) as GameObject;
        LotusObj.transform.position = new Vector3(-5.2f, 0.6f, -2.4f);

        GameObject LotusDestory = GameObject.Find("Lotus_Paper");
        LotusDestory.transform.position = new Vector3(-5f, -2f, -2f);

        m_bPhotoFrameLightOn = true;
        //ShowHint(HintItemID.S1_Photo_Frame);

        //ProcessDialog("Flowchart (3)");
        flowchartObjects[5].gameObject.SetActive(true);

        TempItem = GameObject.Find("Toilet_Door").GetComponent<ItemController>();
        TempItem.bAlwaysActive = false;
        TempItem.eventID = GameEventID.S1_Toilet_Door_Open;
    }

    void KeyboardCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_bInUIView)
            {
                GameEvent(GameEventID.Close_UI);
                //GameObject[] itemsToDelete = GameObject.FindGameObjectsWithTag("ItemObj");
                //// 刪除每個物件Tag為ItemObj的物件
                //foreach (GameObject item in itemsToDelete)
                //{
                //    Destroy(item);
                //}
                ///////////////////////////
            }
            else
            {
                SetGameState();
            }
        }

        if (m_bReturnToBegin)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                playerCtrlr.SetCursor();
                SceneManager.LoadScene(0);
                m_bReturnToBegin = false;
            }
        }
    }

    void MouseCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 在此處理滑鼠點擊事件
            // 可以使用 EventSystem 或 Raycasting 等方法進行 UI 按鈕的選擇處理
        }
    }

    public void SetGameState()
    {
        playerCtrlr.SetCursor();
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        settingObjects.SetActive(isPaused);
        isMouseEnabled = isPaused;
    }

    public void GameStateCheck()
    {
        if (!GlobalDeclare.bLotusGameComplete &&
             m_bPlayLotusEnable &&
             currentScene.name == "2 Grandma House")
        {
            ShowHint(HintItemID.S1_Lotus_Paper);
        }
    }

    public void ShowItemObject(GameObject objToShow)
    {
        // 將指定的物件顯示在 itemObjTransform 的位置
        // 假設你想將該物件作為子物件添加到 itemObjTransform 下
        // 你可以使用 Instantiate 或 SetParent 方法
        //Instantiate(objToShow, itemObjTransform.position, itemObjTransform.rotation, itemObjTransform);
        // 如果需要調整物件的位置、縮放等屬性，可以在這裡進行設置
        //}
    }
}