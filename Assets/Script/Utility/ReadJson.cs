using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System.IO;

using LitJson;

using UnityEngine.UI;

public class ReadJson : MonoBehaviour {


    public static ReadJson instance;

  //  public  Ntext ntext;

    private JsonData itemDate;

    private string jsonString;

 


    public IEnumerator initialization() {
        if (instance == null)
        {

            instance = this;

        }

     yield return   StartCoroutine(readJson());
    }

    IEnumerator readJson() {
        string spath = Application.streamingAssetsPath + "/information.json";

        Debug.Log(spath);

        WWW www = new WWW(spath);

        yield return www;

        jsonString = System.Text.Encoding.UTF8.GetString(www.bytes);

        JsonMapper.ToObject(www.text);

       itemDate = JsonMapper.ToObject(jsonString.ToString());


        for (int i = 0; i < itemDate["information"].Count; i++)

        {
            float DES;
            string UDP;

            DES = float.Parse(itemDate["information"][i]["Des"].ToString());//udp;

            UDP = itemDate["information"][i]["udp"].ToString();//udp;

            ValueSheet.Dic_UPD_TargetDes.Add(UDP, DES);
            ValueSheet.Xval.Add(DES);
            ValueSheet.UDPS.Add(UDP);
        }


        ValueSheet.ReturnUdp = itemDate["Setup"]["back_UDP"].ToString();
        ValueSheet.MinDes = float.Parse( itemDate["Setup"]["MinDes"].ToString());
        ValueSheet.MaxDes = float.Parse(itemDate["Setup"]["MaxDes"].ToString());
        ValueSheet.Offset = float.Parse(itemDate["Setup"]["Offset"].ToString());
        ValueSheet.threshold = float.Parse(itemDate["Setup"]["threshold"].ToString());
        ValueSheet.easeingValue = float.Parse(itemDate["Setup"]["easingVal"].ToString());


        ValueSheet.ScreenIp = itemDate["Setup"]["ScreenIP"].ToString();
        ValueSheet.ScreenPort= int.Parse(itemDate["Setup"]["ScreenPort"].ToString());

    }

}
