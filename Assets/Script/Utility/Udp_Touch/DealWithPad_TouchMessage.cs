using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealWithPad_TouchMessage : MonoBehaviour
{
    private string dataTest;
    // Start is called before the first frame update
    public void MessageManage(string _data)
    {
        if (_data != "" && _data != null)
        {
            dataTest = _data;

            Debug.Log(dataTest);

            SendUPDData.instance.udp_Send(dataTest, ValueSheet.ScreenIp, ValueSheet.ScreenPort);

            if (ValueSheet.Dic_UPD_TargetDes.ContainsKey(dataTest))
            {
                ValueSheet.DesIDCheck = ValueSheet.UDPS.IndexOf(dataTest);

            }else if (dataTest == ValueSheet.ReturnUdp)
            {
                ValueSheet.DesIDCheck = -1;
            }
        }
    }
}
