﻿using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void IvkShowGrandmaFaceUI()
    {
        imgUIBackGround.color = new Color(1, 1, 1, 1);
        Sprite GrandmaFaceSprite = Resources.Load<Sprite>("Sprites/GrandmaFace");
        imgUIBackGround.sprite = GrandmaFaceSprite;
        Invoke(nameof(IvkShowEndView), 5f);
    }

    public void IvkShowEndView()
    {
        imgUIBackGround.color = new Color(1, 1, 1, 1);
        Sprite EndViewSprite = Resources.Load<Sprite>("Sprites/EndView");
        imgUIBackGround.sprite = EndViewSprite;
        m_bReturnToBegin = true;
        playerCtrlr.m_bCanControl = true;
    }

    public void IvkShowDoorKey()
    {
        GameObject GrandmaRoomKey = GameObject.Find("Grandma_Room_Key");
        GrandmaRoomKey.transform.position = new Vector3(-6.8f, 0.8f, -14f);
        ShowHint(HintItemID.S1_Grandma_Room_Key);
    }

    public void IvkProcessPlayerFallingAnimator()
    {
        ProcessPlayerAnimator("Player_Falling_In_Bathroom");
        Invoke(nameof(IvkProcessPlayerWakeUpSecondTime), 4f);

        Light playerFlashlight = playerCtrlr.tfPlayerCamera.GetComponent<Light>();
        playerFlashlight.enabled = false;

        ShowHint(HintItemID.S2_Room_Door);
        ShowHint(HintItemID.S2_Light_Switch);
    }

    public void IvkProcessPlayerWakeUpSecondTime()
    {
        ProcessPlayerAnimator("Player_Wake_Up_SecondTime");
    }

    public void IvkShowS2DoorKey()
    {
        GameObject S2_Grandma_Room_Key = GameObject.Find("S2_Grandma_Room_Key");
        S2_Grandma_Room_Key.transform.position = new Vector3(-6.8f, 0.3f, 37.4f);
        ShowHint(HintItemID.S2_Room_Key);
    }

    public void IvkS2_Grandma_Pass_Door()
    {
        S2_Grandma_Ghost_Obj.GetComponent<Animator>().applyRootMotion = true;
        ShowHint(HintItemID.S2_Toilet_Door);
    }

    public void IvkS2_Shocked_By_Toilet()
    {
        S2_Toilet_Door_GhostHead_Obj.GetComponent<Animator>().SetTrigger("S2_Toilet_Door_GhostHead_Scared");
    }

    public void IvkS2_SlientAfterPhotoFrame()
    {
        // 紹威 (Word 檔 - 聲音大約出現 2-3 秒後安靜下來
        AUDManager.instance.GrandmaStrangeVoiceStop();

        // 加在這兩個註解中間
        ProcessPlayerAnimator("Player_S2_Shocked_After_PhotoFrame");

        Invoke(nameof(IvkS2_PlayGrandmaVideo), 15f);

        Invoke(nameof(IvkS2_SlientAfterPhotoFrameForRecord), 22f);
    }

    public void IvkS2_PlayGrandmaVideo()
    {
        videoPlayer.clip = GrandmaVP;
        videoPlayer.Play();
        AUDManager.instance.BodyTwistingSound();
        Invoke(nameof(IvkS2_ShowVideoPlayerMesh),1f);
    }

    public void IvkS2_ShowVideoPlayerMesh()
    {
        videoPlayer.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void IvkS2_SlientAfterPhotoFrameForRecord()
    {
        videoPlayer.gameObject.GetComponent<MeshRenderer>().enabled = false;
        FinalUI.SetActive(true);
    }
}