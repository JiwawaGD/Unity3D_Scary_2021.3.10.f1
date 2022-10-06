using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemDialog : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text textLable;
    public Image faceImage;

    [Header("文字文件")]
    public TextAsset textFile;
    [Header("編號")]
    public int index;

    //定義一個列表
    List<string> textList = new List<string>();

    /// <summary>
    /// 序列畫
    /// 讓文字框一開始不是空白的
    /// 並且這方法會在Start之前調用 所以將Start方法改為Awake
    /// </summary>
    private void OnEnable()
    {
        textLable.text = textList[index];
        index++;
    }
    private void Awake()
    {
        GetTextFormFile(textFile);
    }

    private void Update()
    {
        //如果(按下R健 並且 行數已經來到文字的最後一行了) 表示文字結束了
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);        //關閉文字框
            index = 0;                          //並確保下次也是從0開始
            return;                             //傳回
        }

        //按下R健便開始輸出文字
        if (Input.GetKeyDown(KeyCode.R))
        {
            textLable.text = textList[index];
            index++;
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


}
