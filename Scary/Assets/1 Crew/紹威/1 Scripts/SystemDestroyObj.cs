using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WEI
{
    /// <summary>
    /// 預製物刪除
    /// </summary>
    public class SystemDestroyObj : MonoBehaviour
    {
        public SystemSpawnItem spawnItem;

        public float destroyTime = 1.5f;

        [Header("")]
        public int objIndex;

        public TextMeshProUGUI arrow;

        private void Start()
        {
            arrow = GameObject.FindObjectOfType<TextMeshProUGUI>();
            Destroy(gameObject, destroyTime);
        }
        private void Update()
        {
            TriggerAnInteractiveSwitch();
        }

        /// <summary>
        /// 觸發互動式物件開關
        /// </summary>
        private void TriggerAnInteractiveSwitch()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    arrow.text = "請勿同時按下";
                    return;
                }
                else
                {
                    if (objIndex == 0)
                    {
                        arrow.text = "正確";
                    }
                    else arrow.text = "不正確";
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    arrow.text = "請勿同時按下";
                    return;
                }
                else
                {
                    if (objIndex == 1)
                    {
                        arrow.text = "正確";
                    }
                    else arrow.text = "不正確";
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    arrow.text = "請勿同時按下";
                    return;
                }
                else
                {
                    if (objIndex == 2)
                    {
                        arrow.text = "正確";
                    }
                    else arrow.text = "不正確";
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    arrow.text = "請勿同時按下";
                    return;
                }
                else
                {
                    if (objIndex == 3)
                    {
                        arrow.text = "正確";
                    }
                    else arrow.text = "不正確";
                }
            }
        }
    }
}

    



