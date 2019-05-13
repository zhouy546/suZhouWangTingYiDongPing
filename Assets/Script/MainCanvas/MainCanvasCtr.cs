using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasCtr : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    // Start is called before the first frame update
    void Awake() {
        EventCenter.AddListener<int>(EventDefine.ShowUI, Show);
        EventCenter.AddListener<int>(EventDefine.HideUI, Hide);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(int id)
    {
        nodes[id].Show(id);

    }

    public void Hide(int id) {
        nodes[id].Hide(-1);
    }

}
