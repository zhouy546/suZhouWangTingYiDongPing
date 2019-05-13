using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private bool isDisplay = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(int id)
    {
        if (!isDisplay)
        {
            animator.SetInteger("Show",id);
                isDisplay = true;
        }

    }

    public void Hide(int id) {
        if (isDisplay)
        {
            animator.SetInteger("Show", id);
            isDisplay = false;
        }
    }
}
