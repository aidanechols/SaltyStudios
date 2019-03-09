using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public PlayerChoiceManager player;
    public Dialogue characterDialogue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Text choice1;
    public Text choice2;
    public Text choice3;
    public Queue<string> sentences;
    public Queue<string> branchSentences;
    public GameObject continueButton;
    public GameObject choiceButton1;
    public GameObject choiceButton2;
    public GameObject choiceButton3;
    public int branchesInBranchesIndex;
    public int count = 0;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        branchSentences = new Queue<string>();
    }

    public void StartDialogue()
    {
        nameText.text = characterDialogue.name;

        sentences.Clear();

        foreach (string sentence in characterDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        string[] temp = characterDialogue.sentences[0].Split('~');
        if (temp[0] != "Branch")
        {
            continueButton.SetActive(true);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (branchSentences.Count == 0)
        {
            string sentence = sentences.Dequeue();
            string[] dialogueOptionSentence = sentence.Split('~');

            if (dialogueOptionSentence[0].Equals("Branch"))
            {
                count++;
                continueButton.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogueOptionSentence[1]));
                choice1.text = dialogueOptionSentence[2];
                choice2.text = dialogueOptionSentence[3];
                choiceButton1.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -17.8f, 0f);
                choiceButton2.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -95f, 0f);
                SetButtonsActive(2);
            }
            else if (dialogueOptionSentence[0].Equals("Branch3"))
            {
                count++;
                continueButton.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogueOptionSentence[1]));
                choice1.text = dialogueOptionSentence[2];
                choice2.text = dialogueOptionSentence[3];
                choice3.text = dialogueOptionSentence[4];
                choiceButton1.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, 61f, 0f);
                choiceButton2.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -17.8f, 0f);
                SetButtonsActive(3);
            }
            else
            {
                count++;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogueOptionSentence[0]));
            }
        }
        else
        {
            string branchSentence = branchSentences.Dequeue();
            string[] splitSentence = branchSentence.Split('~');
            if (splitSentence.Length > 4)
            {
                if (splitSentence[4].Equals("Branch3"))
                {
                    continueButton.SetActive(false);
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(splitSentence[2]));
                    choice1.text = splitSentence[5];
                    choice2.text = splitSentence[6];
                    choice3.text = splitSentence[7];
                    choiceButton1.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, 61f, 0f);
                    choiceButton2.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -17.8f, 0f);
                    SetButtonsActive(3);
                    branchesInBranchesIndex = int.Parse(splitSentence[8]);
                }
                else if (splitSentence[4].Equals("Branch"))
                {
                    continueButton.SetActive(false);
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(splitSentence[2]));
                    choice1.text = splitSentence[5];
                    choice2.text = splitSentence[6];
                    choiceButton1.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -17.8f, 0f);
                    choiceButton2.GetComponent<RectTransform>().anchoredPosition = new Vector3(640f, -95f, 0f);
                    SetButtonsActive(2);
                    branchesInBranchesIndex = int.Parse(splitSentence[7]);
                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentence(splitSentence[2]));
            }
        }
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }

    //looks thru arrays in dialogue for branches and place then into the branchQueue.
    public void BranchDialogue()
    {
        if (branchesInBranchesIndex == -1)
        {
            if (characterDialogue.branches.Length != 0)
            {
                string[] temp = characterDialogue.branches;
                for (int i = 0; i < characterDialogue.branches.Length; i++)
                {
                    string branch = temp[i];
                    string[] splitBranch = branch.Split('~');
                    if (int.Parse(splitBranch[0]) == (count - 1))
                    {
                        if (int.Parse(splitBranch[1]) == player.GetChoice(int.Parse(splitBranch[3])))
                        {
                            branchSentences.Enqueue(branch);
                        }
                    }
                }
            }
        }
        else
        {
            string[] temp = characterDialogue.branchesInBranches;
            for (int i = 0; i < temp.Length; i++)
            {
                string[] splitBranch = temp[i].Split('~');
                if (int.Parse(splitBranch[0]) == branchesInBranchesIndex)
                {
                    if (player.GetChoice(int.Parse(splitBranch[3])) == int.Parse(splitBranch[1]))
                    {
                        branchSentences.Enqueue(temp[i]);
                    }
                }
            }
            branchesInBranchesIndex = -1;
        }
        DisplayNextSentence();
    }

    public void SetButtonsInactive()
    {
        Vector3 vec = new Vector3(0f, 0f, 0f);
        choiceButton1.GetComponent<RectTransform>().localScale = vec;
        choiceButton2.GetComponent<RectTransform>().localScale = vec;
        choiceButton3.GetComponent<RectTransform>().localScale = vec;
    }

    public void SetButtonsActive(int x)
    {
        Vector3 vec = new Vector3(1.0f, 1.0f, 1.0f);
        if (x == 3)
        {
            choiceButton1.GetComponent<RectTransform>().localScale = vec;
            choiceButton2.GetComponent<RectTransform>().localScale = vec;
            choiceButton3.GetComponent<RectTransform>().localScale = vec;
        }else if (x == 2)
        {
            choiceButton1.GetComponent<RectTransform>().localScale = vec;
            choiceButton2.GetComponent<RectTransform>().localScale = vec;
        }
    }
}
