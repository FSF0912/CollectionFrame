
using System.Text;
using FSF.CollectionFrame;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GalgameSelector : MonoBehaviour
{
    public GameObject firstButton;
    public Text result;
    public Animation animation2;
    bool first = true;

    public void select(){
        if(first){
            firstButton.SetActive(false);
            first = false;
        }
        animation2.Stop();
        animation2.Play();
        string path = Application.streamingAssetsPath + "/shabi/gallist.csv";
        var result = CSVReader.ReadCSV(path, Encoding.UTF8);
        string[] shower = result[Random.Range(1, result.Count)];
        string showed = 
        $"游戏名称:{shower[3]}({shower[2]})\n制作公司:{shower[0]}";
        this.result.text = showed;
    }

    public void getResult(){
        GUIUtility.systemCopyBuffer = result.text;
    }
}
