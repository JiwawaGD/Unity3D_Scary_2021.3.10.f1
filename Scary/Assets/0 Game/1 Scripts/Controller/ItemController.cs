using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("¹CÀ¸¨Æ¥ó")]
    public GlobalDeclare.GameEvent gameEvent;

    [HideInInspector]
    public bool b_isOutline;

    Outline outline;
    GameManager gameManager;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        if (gameManager = null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (b_isOutline)
            outline.OutlineWidth = 10;
        else
            outline.OutlineWidth = 0;
    }

    public void SendGameEvent()
    {
        gameManager.GameEvent(gameEvent);
    }
}
