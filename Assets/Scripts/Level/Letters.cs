using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letters : MonoBehaviour
{
    public static Letters instance;
    public GameObject herculesContainer;
    public TextMeshProUGUI[] HerculesLetters;
    public Dictionary<string, bool> lettersFound = new Dictionary<string, bool>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        herculesContainer.SetActive(false);
        lettersFound.Add("H", false);
        lettersFound.Add("E", false);
        lettersFound.Add("R", false);
        lettersFound.Add("C", false);
        lettersFound.Add("U", false);
        lettersFound.Add("L", false);
        lettersFound.Add("S", false);

    }
    // Start is called before the first frame update
    public void ColorLetter(string letter)
    {
        switch (letter)
        {
            case "H":
                HerculesLetters[0].richText = true;
                HerculesLetters[0].text = "<color=yellow>H</color>";
                break;
            case "E":
                HerculesLetters[1].richText = true;
                HerculesLetters[1].text = "<color=yellow>E</color>";

                HerculesLetters[6].richText = true;
                HerculesLetters[6].text = "<color=yellow>E</color>";
                break;
            case "R":
                HerculesLetters[2].richText = true;
                HerculesLetters[2].text = "<color=yellow>R</color>";
                break;
            case "C":
                HerculesLetters[3].richText = true;
                HerculesLetters[3].text = "<color=yellow>C</color>";
                break;
            case "U":
                HerculesLetters[4].richText = true;
                HerculesLetters[4].text = "<color=yellow>U</color>";
                break;
            case "L":
                HerculesLetters[5].richText = true;
                HerculesLetters[5].text = "<color=yellow>L</color>";
                break;
            case "S":
                HerculesLetters[7].richText = true;
                HerculesLetters[7].text = "<color=yellow>S</color>";
                break;
            default:
                break;
        }
    }

    public IEnumerator LetterFound(string letter)
    {
        ColorLetter(letter);
        if(!lettersFound[letter])
        {
            LevelManager.instance.lettersFound++;
            lettersFound[letter] = true;
        }
        Debug.Log(LevelManager.instance.lettersFound);
        herculesContainer.SetActive(true);
        yield return new WaitForSeconds(3);
        herculesContainer.SetActive(false);
    }
}
