using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    #region Variables
    // Change your CHOSEN word here
    public string word = "ANVIL";
    public Row[] myRows;
    //
    public Color hot, warm, cold;
    #endregion

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CompareWords();
        }
    }
    #endregion

    #region Notes
    /*  
     * Compare words will find the word in row[0]
     * and compare it with our answer word
     * TODO: change row[0] to any row
     */
    #endregion

    #region CompareWords
    public void CompareWords()
    {
        string guess = myRows[0].ReturnWord();

        for (int iter = 0; iter < 5; iter++)
        {
            if (word[iter] == guess[iter])
                myRows[0].PushColour(iter, hot);
            else if (word.Contains(guess[iter]))
                myRows[0].PushColour(iter, warm);
            else
                myRows[0].PushColour(iter, cold);
        }
    }
    #endregion
}
