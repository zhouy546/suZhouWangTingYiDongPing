
//*********************❤*********************
// 
// 文件名（File Name）：	DealWithUDPMessage.cs
// 
// 作者（Author）：			LoveNeon
// 
// 创建时间（CreateTime）：	Don't Care
// 
// 说明（Description）：	接受到消息之后会传给我，然后我进行处理
// 
//*********************❤*********************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class DealWithUDPMessage : MonoBehaviour {


    public static DealWithUDPMessage instance;


    public List<Section> sections = new List<Section>();


    #region add value from port into Queue
    public Queue<Vector3> TargetPos = new Queue<Vector3>();
    private Vector3 currentPosition;

    #endregion
    // public GameObject wellMesh;

    #region easing value
    private string dataTest;

    private float temp;


    //public float Offset = 300;

    private float outputMin;
    public float OutputMin {
        get {
            return outputMin + ValueSheet.Offset;
        }
        set
        {
            outputMin = value;
        }
    }

    public Transform Locator;

    public float OutputMax {
        get
        {
            return Locator.position.x - ValueSheet.Offset;

        }
        set {
            Locator.position = new Vector3(value, Locator.position.y, Locator.position.z);
        }
    }

    #endregion 
    //private static bool isInScreenProtect=true;


    //public LogoWellCtr logoWellCtr;
    //private bool enterTrigger, exitTrigger;
    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="_data"></param>
    public void MessageManage(string _data)
    {
        if (_data != ""&&_data!=null)
        {


            dataTest = _data;

            Debug.Log(dataTest);

            temp = float.Parse(dataTest);

            CheckPosition(temp);
            //Debug.Log("inputMin" + ValueSheet.MinDes + "inputMax" + ValueSheet.MaxDes);
            //Debug.Log("OutputMin" + OutputMin + "OutputMax" + OutputMax);
            //mainUICTR.Check(temp);

            float Xvalue = UtilityFun.Maping(temp, ValueSheet.MinDes, ValueSheet.MaxDes, OutputMin, OutputMax, false);

            Debug.Log(Xvalue);
            Vector3 POS = new Vector3(Xvalue, Camera.main.transform.position.y, Camera.main.transform.position.z);
            TargetPos.Enqueue(POS);

         
        }

    }



    private void Awake()
    {

     
    }


    public IEnumerator Initialization() {
        if (instance == null)
        {
            instance = this;
        }
        foreach (float item in ValueSheet.Xval)
        {
            sections.Add(new Section(true, ValueSheet.Xval.IndexOf(item), item - ValueSheet.threshold, item + ValueSheet.threshold));
        }
        yield return new  WaitForSeconds(0.01f);
    }

    public void Start()
    {
        TargetPos.Enqueue(Camera.main.transform.position);
    }


    private void Update()
    {
        if (GetPosition() != Vector3.zero) {
            Vector3 tempPos = GetPosition();
            CameraMove(tempPos);
        }

      
        //Debug.Log("数据：" + dataTest);  
    }




    private float x,y,z;
    public float Scale;
    private void CameraMove(Vector3 vector3POS) {
        x = x + (vector3POS.x - x) * ValueSheet.easeingValue;
        //   y += (vector3POS.y - y) * .125F;
        //        z += (vector3POS.z - z) * 0.033f;

        Vector3 target = new Vector3(x * Scale, Camera.main.transform.position.y, Camera.main.transform.position.z);
       Camera.main.transform.position = target;

        //perviousX = vector3POS.x;
    }

    private Vector3 GetPosition() {
        if (TargetPos.Count > 0) {
            Vector3 temp = TargetPos.Dequeue();
            currentPosition = temp;
        }

        return currentPosition;
    }


    public void setEasingValue(float value)
    {
        ValueSheet.easeingValue = value;
    }

    private void CheckPosition(float _x) {
      
        foreach (Section section in sections)
        {
       
            if (_x > (section.MinNUM) && _x < (section.MaxNUM)) {

                if (section.isBlack)
                {
                    if(_x> sections[ValueSheet.DesIDCheck].MinNUM&&_x<sections[ValueSheet.DesIDCheck].MaxNUM)
                    {

                      //  Debug.Log(_x);
                        Debug.Log("Show UI");
                        section.isBlack = false;
                        EventCenter.Broadcast<int>(EventDefine.ShowUI, section.ID);
                    }
                }

            }
            else
            {
                if (!section.isBlack) {
                    Debug.Log("Hide UI");
                    section.isBlack = true;
                    EventCenter.Broadcast<int>(EventDefine.HideUI, section.ID);
                }
            }

        }
    }

}
[System.Serializable]
public class Section
{
    public bool isBlack;
    public int ID;
    public float MaxNUM;
    public float MinNUM;

    public Section(bool _isBlack, int _ID,float _minNUM, float _maxNUM)
    {
        isBlack = _isBlack;
        ID = _ID;
        MaxNUM = _maxNUM;
        MinNUM = _minNUM;
    }

}
