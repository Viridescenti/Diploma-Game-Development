using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    #region Variables
    private Cell[] myCells;
    public game myGame;
    #endregion

    #region Awake
    private void Awake()
    {
        myCells = GetComponentsInChildren<Cell>();
    }
    #endregion

    #region Notes
    /* 
     * ReturnWord() will yse its Cells to assemble
     * all of the letters into a String. Return to caller 
     */
    #endregion

    #region Return Words
    public string ReturnWord() // Returns to whoever calls it - communicates with the row we're working with
    {
        string word = "";
        //The letter in every single cell
        foreach (Cell cell in myCells)
        {
            // Add the letter in the cell to the word
            word += cell.ReturnLetter();
        }
        print(word);
        return word;
    }
    #endregion

    #region Notes
    /* 
    * PushColor() will change the colour of one of my Cells
    * using a given index 
    */
    #endregion

    #region Grid Colours
    public void PushColour(int index, Color newColor)
    {
        myCells[index].ChangeColor(newColor);
    }
    #endregion
}
