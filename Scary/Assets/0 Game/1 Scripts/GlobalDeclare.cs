using UnityEngine;

public class GlobalDeclare : MonoBehaviour
{
    public static float f_deltaTime;

    public static Vector3 v3_zero;

    void Awake()
    {
        f_deltaTime = Time.deltaTime;

        v3_zero = Vector3.zero;
    }
}
