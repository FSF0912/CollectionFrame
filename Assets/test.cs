using System;
using FSF.CollectionFrame;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public PopupInterface popupInterface;
    private void Start() {
        GetComponent<Button>().onClick.AddListener(()=>{
            popupInterface.OperatePanel(this.transform.position,true);
        });
    }
}
