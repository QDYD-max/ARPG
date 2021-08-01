using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFlash : MonoBehaviour
{
    CanvasGroup cg;
    Tweener t;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        ToLittle();
    }
    
    private void ToBig()
    {
        t = cg.DOFade(1, 1).OnComplete(ToLittle);
    }
    private void ToLittle()
    {
        t = cg.DOFade(0, 1).OnComplete(ToBig);
    }
}
