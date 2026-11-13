
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(2, 5)]
    public string dialogueText;
    
    public Sprite characterSprite;
    public Sprite currentBackground;
    public int path;
    public bool isQuestion;
}

[System.Serializable]
public class SingleChoice
{
    public string choiceText;
    public float scoreGiven;
}

[System.Serializable]
public class SplitChoice
{
    public SingleChoice choice1;
    public SingleChoice choice2;
    public SingleChoice choice3;
    public Sprite boxSprite;

}

public class DialogueManager : MonoBehaviour
{
    public DialogueLine[] dialogueLines;
    public SplitChoice[] splitChoices;
    public Image splitChoiceBox1;
    public Image splitChoiceBox2;
    public Image splitChoiceBox3;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text choiceText1;
    public TMP_Text choiceText2;
    public TMP_Text choiceText3;

    public Image characterSprite;   
    public Image currentBackground;
    public Button nextButton;
    public Button button1;
    public Button button2;
    public Button button3;

    private int currentLine = 0;
    private int currentSplitChoice = 0;
    private int chosenChoice = 0;
    private float currentGrade = 2;

    void Start()
    {
        nextButton.onClick.AddListener(NextLine);
        button1.onClick.AddListener(() => chooseOption(1, splitChoices[currentSplitChoice].choice1.scoreGiven));
        button2.onClick.AddListener(() => chooseOption(2, splitChoices[currentSplitChoice].choice2.scoreGiven));
        button3.onClick.AddListener(() => chooseOption(3, splitChoices[currentSplitChoice].choice3.scoreGiven));
        ShowLine();
    }

    void ShowLine()
    {
        if (currentLine >= dialogueLines.Length)
        {
            nameText.text = "Анатол Немски";
            if(currentGrade <= 4){
                dialogueText.text = "Справи се по-зле отколкото очаквах, но не се отчайвай вярвам в теб! Ще ти пиша " + currentGrade;
            }
            else dialogueText.text = "Справи се ЧУДЕСНО! Точно заради това си ми любимия ученик! Пиша ти " + currentGrade;

            nextButton.gameObject.SetActive(false); 
            return;
        }
        while((dialogueLines[currentLine].path != chosenChoice) && (dialogueLines[currentLine].path != 0))
            {
                currentLine++;
            }
            
        if (currentLine >= dialogueLines.Length)
        {
            nameText.text = "";
            dialogueText.text = "Благодарим ти, че игра демото на играта ни! \n Оценката ти е:" + currentGrade;
            nextButton.gameObject.SetActive(false); 
            characterSprite.enabled = false;
            return;
        }

        if(!dialogueLines[currentLine].isQuestion){
            nextButton.gameObject.SetActive(true);
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            splitChoiceBox1.enabled = false;
            splitChoiceBox2.enabled = false;
            splitChoiceBox2.enabled = false;
            choiceText1.text = "";
            choiceText2.text = "";
            choiceText2.text = "";
        }
        if (dialogueLines[currentLine].isQuestion)
        {
            nextButton.gameObject.SetActive(false);
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            splitChoiceBox1.enabled = true;
            splitChoiceBox2.enabled = true;
            splitChoiceBox2.enabled = true;
            splitChoiceBox1.sprite = splitChoices[currentSplitChoice].boxSprite;
            splitChoiceBox2.sprite = splitChoices[currentSplitChoice].boxSprite;
            splitChoiceBox3.sprite = splitChoices[currentSplitChoice].boxSprite;
            choiceText1.text = splitChoices[currentSplitChoice].choice1.choiceText;
            choiceText2.text = splitChoices[currentSplitChoice].choice2.choiceText;
            choiceText3.text = splitChoices[currentSplitChoice].choice3.choiceText;
        }
        nameText.text = dialogueLines[currentLine].characterName;
        dialogueText.text = dialogueLines[currentLine].dialogueText;
        characterSprite.sprite = dialogueLines[currentLine].characterSprite;
        characterSprite.enabled = characterSprite.sprite != null;
        currentBackground.sprite = dialogueLines[currentLine].currentBackground;
        
    }

    void NextLine()
    {
        if(!dialogueLines[currentLine].isQuestion){
        currentLine++;
        ShowLine();
        }   
    }
    void chooseOption(int pathTaken, float score)
    {
        if( dialogueLines[currentLine].isQuestion ){
        chosenChoice = pathTaken;
        currentGrade += score;
        currentLine++;
        currentSplitChoice++;
        ShowLine();
        }
    }

    

}
