using UnityEngine;

public class Ty_ItemView : MonoBehaviour
{
    public GlobalDeclare.ItemMessage itemMessage;

    [HideInInspector]
    public bool b_isOutline;

    Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (b_isOutline)
            outline.OutlineWidth = 10;
        else
            outline.OutlineWidth = 0;
    }

    public void ItemInteract(GlobalDeclare.ItemMessage _msg)
    {
        switch (_msg)
        {
            case GlobalDeclare.ItemMessage.test_box:
                transform.position += new Vector3(10, 0, 0);
                break;
        }
    }
}
