
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
}

public class DialogueManager : MonoBehaviour
{
    public DialogueLine[] dialogueLines;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image characterSprite;   
    public Button nextButton;

    private int currentLine = 0;

    void Start()
    {
        nextButton.onClick.AddListener(NextLine);
        ShowLine();
    }

    void ShowLine()
    {
        if (currentLine >= dialogueLines.Length)
        {
            nameText.text = "";
            dialogueText.text = "The End";
            nextButton.gameObject.SetActive(false);
            characterSprite.enabled = false;
            return;
        }

        nameText.text = dialogueLines[currentLine].characterName;
        dialogueText.text = dialogueLines[currentLine].dialogueText;
        characterSprite.sprite = dialogueLines[currentLine].characterSprite;
        characterSprite.enabled = characterSprite.sprite != null;    
    }

    void NextLine()
    {
        currentLine++;
        ShowLine();
    }
}
