using UnityEngine;

public class Ty_ItemView : MonoBehaviour
{
    public GlobalDeclare.ItemMessage itemMessage;

    [HideInInspector]
    public Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
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
