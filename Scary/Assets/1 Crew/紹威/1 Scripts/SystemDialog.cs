using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace WEI
{
    /// <summary>
    /// 對話系統
    /// </summary>
    public class SystemDialog : MonoBehaviour
    {
        #region 欄位
        [Header("UI")]
        public TMP_Text textLable;

        [Header("文字文件")]
        public TextAsset textFile;
        [Header("編號")]
        public int index;
        [Header("文字速度")]
        public float txetSpeed;

        bool textFinished;

        //定義一個列表
        List<string> textList = new List<string>();
        #endregion

        /// <summary>
        /// 序列畫
        /// 讓文字框一開始不是空白的
        /// 並且這方法會在Start之前調用 所以將Start方法改為Awake
        /// </summary>
        private void OnEnable()
        {
            //textLable.text = textList[index];
            //index++;
            textFinished = true;
            StartCoroutine(SetTextUI());
        }

        private void Awake()
        {
            GetTextFormFile(textFile);
        }

        private void Update()
        {
            Reading();
        }

        /// <summary>
        /// 鍵盤按下R鍵開始閱讀
        /// </summary>
        private void Reading()
        {
            //如果(按下R健 並且 行數已經來到文字的最後一行了) 表示文字結束了
            if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
            {
                gameObject.SetActive(false);        //關閉文字框
                index = 0;                          //並確保下次也是從0開始
                return;                             //傳回
            }

            //按下R健便開始輸出文字
            if (Input.GetKeyDown(KeyCode.R) && textFinished)
            {
                //textLable.text = textList[index];
                //index++;
                StartCoroutine(SetTextUI());
            }
        }

        /// <summary>
        /// 從文字資料夾獲得文字
        /// </summary>
        private void GetTextFormFile(TextAsset file)
        {
            textLable.ClearMesh();          //清空列表
            index = 0;

            //字符型 LineDate = 文字文件.文字.轉型(換行);
            var lineDate = file.text.Split('\n');

            foreach (var line in lineDate)
            {
                //將文字新增到列表當中
                textList.Add(line);
            }
        }

        /// <summary>
        /// 文字偕同程序
        /// </summary>
        /// <returns></returns>
        IEnumerator SetTextUI()
        {
            textFinished = false;
            textLable.text = "";    // 清空一開始的文字列

            switch (textList[index])
            {
                case "A":
                    //faceImage.sprite = face01;
                    index++;
                    break;
                case "B":
                    //faceImage.sprite = face02;
                    index++;
                    break;
            }
            for (int i = 0; i < textList[index].Length; i++)
            {
                textLable.text += textList[index][i];
                yield return new WaitForSeconds(txetSpeed);
            }
            textFinished = true;
            index++;
        }
    }
}