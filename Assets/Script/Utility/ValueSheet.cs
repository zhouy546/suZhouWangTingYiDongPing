using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSheet : MonoBehaviour
{
    public static List<float> Xval = new List<float>();

    public static List<string> UDPS = new List<string>();

    public static Dictionary<string, float> Dic_UPD_TargetDes = new Dictionary<string, float>();

    public static string ReturnUdp;

    public static float MinDes;
    public static float MaxDes; 
       public static float Offset;
    public static float threshold;

    public static float easeingValue;

    public static string ScreenIp;
    public static int ScreenPort;


    public static int DesIDCheck = -1;
}
