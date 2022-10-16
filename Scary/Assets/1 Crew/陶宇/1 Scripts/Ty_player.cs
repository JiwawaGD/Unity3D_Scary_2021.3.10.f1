using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ty_player : MonoBehaviour
{
    //  Can be setted by player
    float f_UDSensitivity;
    float f_RLSensitivity;

    //  Const value
    float f_moveSpeed;
    float f_deltatime;
    float f_lookRotation;
    float f_rayLength;

    int i_InteractiveLayer;

    //  Cursor Show
    bool b_isShow;

    Vector3 v3_zero;
    Vector3 v3_moveValue;
    Vector3 v3_movePos;

    [SerializeField] GameObject cam;
    Rigidbody rig;
    RaycastHit hit;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        cam = GameObject.Find("Player Camera");
    }

    void Start()
    {
        Cursor.visible = false;

        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
            SetCursor();
    }

    void FixedUpdate()
    {
        Move();
        View();
        RayHitCheck();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + (cam.transform.forward * f_rayLength));
    }

    //  Value Initialize
    void Init()
    {
        f_moveSpeed = 180;
        f_UDSensitivity = 120;
        f_RLSensitivity = 80;
        f_rayLength = 2;

        i_InteractiveLayer = 10;

        b_isShow = false;

        f_deltatime = GlobalDeclare.f_deltaTime;
        v3_zero = GlobalDeclare.v3_zero;
    }

    //  Cursor State
    void SetCursor()
    {
        b_isShow = !b_isShow;

        Cursor.visible = b_isShow;
    }

    //  View Function
    void View()
    {
        //  左右轉
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * f_RLSensitivity * f_deltatime);

        f_lookRotation += Input.GetAxis("Mouse Y") * f_UDSensitivity * f_deltatime;
        f_lookRotation = Mathf.Clamp(f_lookRotation, -75, 75);

        //  上下轉
        cam.transform.localEulerAngles = -Vector3.right * f_lookRotation;
    }

    //  Move Function
    void Move()
    {
        v3_moveValue = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        v3_movePos.x = v3_moveValue.x * f_deltatime * f_moveSpeed;
        v3_movePos.z = v3_moveValue.z * f_deltatime * f_moveSpeed;

        v3_movePos = transform.right * v3_movePos.x + transform.forward * v3_movePos.z;

        if (v3_movePos != v3_zero)
            rig.velocity = v3_movePos;
        else
            rig.velocity = v3_zero;
    }

    //  Ray check for item interact
    void RayHitCheck()
    {
        if (Physics.Raycast(cam.transform.position,     // Origin 
            cam.transform.forward,                      // Direction
            out hit,                                    // RaycastHit
            f_rayLength))                               // RayLength
        {
            if (hit.transform.gameObject.layer == i_InteractiveLayer)
            {
                hit.transform.gameObject.GetComponent<Outline>().OutlineWidth = 10;
            }
        }
    }
}
