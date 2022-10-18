using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Header("玩家")] PlayerController player;

    [SerializeField] [Header("室內傳送點")] Transform t_indoorPos;

    Transform t_player;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (player = null)
            player = GameObject.Find("player").GetComponent<PlayerController>();

        t_player = player.transform;
    }

    public void GameEvent(GlobalDeclare.GameEvent _gameEvent)
    {
        switch (_gameEvent)
        {
            case GlobalDeclare.GameEvent.S1Move_To_Indoor:
                t_player.position = t_indoorPos.position;
                break;
        }
    }
}
