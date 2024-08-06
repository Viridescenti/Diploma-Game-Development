using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    #region Variables
    private Row myRow;
    private TMP_InputField myField;
    private Image myImage;
    #endregion

    #region Awake
    void Awake()
    {
        myRow = GetComponentInParent<Row>();
        myField = GetComponent<TMP_InputField>();
        myImage = GetComponent<Image>();
    }
    #endregion

   /*
    *  Return the (first) letter currently stored in this Cell 
    */


    #region Return Letters
    public char ReturnLetter()
    {
        return myField.text[0];
    }
    #endregion
    /* 
     * Change this cell's coloir to a given colour 
     */

    #region Cell Colours
    public void ChangeColor(Color newColor)
    {
        myImage.color = newColor;

    }
    #endregion
}
