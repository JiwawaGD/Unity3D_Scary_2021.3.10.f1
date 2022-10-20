using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Header("玩家")] PlayerController player;

    [SerializeField] [Header("戶內傳送點")] Transform t_indoorPos;

    [SerializeField] [Header("所有可互動物件")] ItemController[] items;

    Transform t_player;

    void Start()
    {
        Init();
    }

    void Init()
    {
        t_player = player.transform;
    }

    public void GameEvent(GameEventID _eventID)
    {
        switch (_eventID)
        {
            case GameEventID.S1Move_To_Indoor:
                t_player.position = t_indoorPos.position;
                break;
        }
    }
}
