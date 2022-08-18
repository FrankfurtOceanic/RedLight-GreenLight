using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelColorChange : MonoBehaviour
{
    [SerializeField] private Image myImage;

    [SerializeField] private  Color myColor1;
    [SerializeField] private  Color myColor2;
    private static int currentColor;
    // Start is called before the first frame update
    void Start()
    {
        myColor1.a = 1;
        myColor2.a = 1;
    }

    public void ChangeColor()
    {
        if (currentColor == 1) //if color 1, change to color 2
        {
            myImage.color = myColor2;
            currentColor = 2;
        }

        else //if color 2, change to color 1
        {
            myImage.color = myColor1;
            currentColor = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeColor();
        }
    }

}
