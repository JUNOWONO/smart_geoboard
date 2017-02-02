using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Buttons : MonoBehaviour
{
    public Text t1;
   // public Text t2;
   // public Text t3;
  //  public Text t4;

    public Text text;

    void start()
    {
        text.text = "";
    }
    
    public void OnClick()
    {
        if(t1.text.Equals("O")){
            text.text = "정답입니다~";
        }
        else if (t1.text.Equals("X"))
        {
            text.text = "오답입니다~";
        }

        else
        {
            text.text = "";
        }
 
    }

}
