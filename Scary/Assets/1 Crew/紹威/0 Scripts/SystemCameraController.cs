using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace WEI
{
    /// <summary>
    /// 攝影機控制系統
    /// </summary>
    public class SystemCameraController : MonoBehaviour
    {
        public GameObject cameraMain;
        public GameObject camera_1;
        public GameObject text;

        private void Start()
        {
            //cameraMain = GameObject.Find("Player Camera");
            //camera_1 = GameObject.Find("Camera_1");
            cameraMain.SetActive(true);
            camera_1.SetActive(false);

        }
        private void Update()
        {
            EscClickObj();
        }
        public void ClickObj()
        {
            cameraMain.SetActive(false);
            camera_1.SetActive(true);
        }
        public void EscClickObj()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cameraMain.SetActive(true);
                camera_1.SetActive(false);
                text.SetActive(false);
            }
        }
    }
}
