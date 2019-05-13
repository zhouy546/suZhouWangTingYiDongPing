using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSend : MonoBehaviour
{
    public int id;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendUdp()
    {
        SendUPDData.instance.udp_Send(ValueSheet.UDPS[id], "127.0.0.1", 29011);

    }

    public void returnBtn(){
        SendUPDData.instance.udp_Send(ValueSheet.ReturnUdp, "127.0.0.1", 29011);

    }

    public void SetSliderValue() {
        float startVal = slider.value;

        LeanTween.value(startVal, (float)id, 0.5f).setOnUpdate((float val) =>
        {
            slider.value = val;
        });

    }
}
