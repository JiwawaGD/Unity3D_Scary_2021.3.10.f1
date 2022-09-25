using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ty_player : MonoBehaviour
{
    float f_speed;
    float f_deltatime;

    readonly Vector3 v3_zero;
    Vector3 v3_pos;
    Vector3 v3_target;

    Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        f_speed = 180;
        f_deltatime = Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        v3_target = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        v3_pos.x = v3_target.x * f_deltatime * f_speed;
        v3_pos.z = v3_target.z * f_deltatime * f_speed;

        if (v3_pos != v3_zero)
            rig.velocity = v3_pos;
        else
            rig.velocity = v3_zero;
    }
}
