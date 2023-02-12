using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public GameObject[] hangMan;
    public Text wordToFindField;
    public GameObject wintext;
    public GameObject loseText;
    public GameObject replay;
    public GameObject replayButton;
    private float time;
    //private string[] wordsLocal = {"MATT", "JOANNE", "ROBERT", "MARRY JANE", "DENIS", "RAJ", "CHANDNI", "ANKITA", "KRISHNA", "TAVISHI", "RUTVIK", "jAYSHREE", "HARI",};
    //private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    //private string[] wordsLocal = File.ReadAllLines(@"Assets/words2.txt");
    private string[] wordsLocal = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "words2.txt"));
    private string chosenWord;
    public string hiddenWord;
    private int fails;
    private bool gameEnd = false;

  

    // Start is called before the first frame update
    void Start()
    {
        hiddenWord = "";
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];
        chosenWord = chosenWord.ToUpper();
        System.Console.WriteLine(chosenWord);
        Debug.Log(chosenWord);
        for (int i = 0; i < chosenWord.Length; i++)
        {
            if (char.IsWhiteSpace(chosenWord[i]))
                hiddenWord += " ";
            else
                hiddenWord += "_";
        }
        //int initialLength = chosenWord.Length;
        //for (int i = 0; i < initialLength; i++)
        //{
        //    if (char.IsWhiteSpace(chosenWord[i]))
        //        chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);
        //    else
        //        chosenWord = chosenWord.Substring(0, i) + " " + chosenWord.Substring(i + 1);
        //}
        wordToFindField.text = hiddenWord;
        fails = 0;
        for (int i = 0; i < hangMan.Length; i++)
            hangMan[i].SetActive(false);
        time = 0;
        wintext.SetActive(false);
        loseText.SetActive(false);
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnd == false)
        {
            time += Time.deltaTime;
            timeField.text = time.ToString();
        }
        
    }

    private void OnGUI()
    {
        Event e = Event.current;
        
        //pressing ctrl + clicking on a class or enum will show all its properties

        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            //Debug.Log(e.keyCode);
            string pressedLetter = e.keyCode.ToString();
            if(chosenWord.Contains(pressedLetter))
            {
                
                int index = chosenWord.IndexOf(pressedLetter);
                
                //hiddenWord[index] = pressedLetter;
                while(index != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, index) + pressedLetter + hiddenWord.Substring(index + 1); // update hidden word
                    Debug.Log("hidden word: " + hiddenWord);
                    chosenWord = chosenWord.Substring(0, index) + "_" + chosenWord.Substring(index + 1); // update chosen word
                    Debug.Log("chosen word: " + chosenWord);
                    index = chosenWord.IndexOf(pressedLetter);
                }

                wordToFindField.text = hiddenWord;
            }
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
                
            }

            if(fails == hangMan.Length)
            {
                loseText.SetActive(true);
                gameEnd = true;
                //replay.SetActive(true);
                replayButton.SetActive(true);
            }
            if(hiddenWord.Contains("_") == false)
            {
                wintext.SetActive(true);
                gameEnd = true;
                //replay.SetActive(true);
                replayButton.SetActive(true);
            }
                
            
                
        }
        else if(Input.GetKeyDown(KeyCode.Space) && gameEnd == true)//(e.type == EventType.ScrollWheel && gameEnd == true)
            {
                Start();
            }
    }
}
