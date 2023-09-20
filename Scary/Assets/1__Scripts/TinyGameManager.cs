using UnityEngine;

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
}