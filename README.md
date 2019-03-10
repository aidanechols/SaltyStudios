# SaltyStudios

Last Edited: 3/9/2019
Last Editor: Thana Sithisombath (Birthana)

Unity - Version Used 2018.3.7 - May or may not work in newer or older version.
However, did move the project from 2018.2.7 version so it may be fine.

Dialogue System -
The dialogue system is currently tied to a button, "Start Dialogue." Normally
I think that the script on the button would tied to a character sprite or
maybe have different scenes as divides between different dialogue and at the
start of a different scene it activates the dialogue manager. The current
button when pressed activates the script, NPC, that it is attached to. NPC is
a simple script that just calls the DialogueManager object to start displaying
dialogue. The DialogueManager hides the script DialogueManager which manages 
basically everything around dialogue.It will display dialogue, branch when 
necessary, and store information of player choices in a PlayerChoiceManager.
Currently, the script hides various public variable which can be edit in the
Unity program or by using a Find function in the Start(). I been using the
Unity program to drop in variables/objects/scripts to assign the proper 
variables. 

Variables in DialogueManager:
PlayerChoiceManager player - object that contains choices of branching dialogue
Dialogue characterDialogue - object that contains all dialogue for the scene
TextMeshProUGUI nameText - UI that conatins string of the name of NPC. Displayer
TextMeshProUGUI dialogueText - UI that contains the string of dialogue. Displayer
Text choice1 - UI Button that contains choice 1 of branching dialogue. Displayer
Text choice2 - UI Button that contains choice 2 of branching dialogue. Displayer
Text choice3 - UI Button that contains choice 3 of branching dialogue. Displayer
Queue<string> sentences - Data structure that has all dialogue in main dialogue.
Queue<string> branchSentences - Data structure that has branching dialogue. Priority
GameObject continueButton - UI Button that displays next dialogue in queues.
GameObject choiceButton1 - UI Button that appears or disappears thru localScale.
GameObject choiceButton2 - UI Button that appears or disappears thru localScale.
GameObject choiceButton3 - UI Button that appears or disappears thru localScale.
int branchesInBranchesIndex - index that marks the next branches in branches dialogue in the array
int count - count that keep tracks of the branch dialogue in the array.

*TextMeshPro is just a recent addition to Unity that makes text look nice & clear
*Queue is a data structure. Look at documentation in a C librbary to know the methods.

The StartDialogue function is called when the NPC is called, currently via button.
It sets up most of the variables from the Dialogue scriptable object. Then, calls
DisplayNextSentence function in the same script, to begin displaying dialogue.
The function, DisplayNextSentence, will look at the next piece of dialogue in the
queues. First, it will check the branches queue for branches, then go back into 
the main sentences queue. Each time this function is called, it will display the
dialogue using an iterator to give it a classic dialogue text feel and look. If
the piece of dialogue asks the player for a choice it will display choice buttons
and change their text for each choice. The button on the Unity Program side of
things handles the choices the players makes and calls necessary functions after.
The choice buttons are connected to the other buttons to make them appear or
disappear when needed, the other choice buttons and the continue button. They also
set the player choices in the PlayerChoiceManager, storing player's choices. They
call the BranchDialogue function to go thru the branch or branchInBranch arrays to
find the proper/next dialogue via the player's choice and puts them in the branch
queue. The DisplayNextSentence is called most likely choosing the sentences in the
queue which is the recently picked choice. THe rest of the functions just makes
the buttons appear or disappear.

*Dialogue is a scriptable object
*PlayerChoiceManager stores information as 0,1,2,etc. so need to read down
somewhere what number correspond to which choice.

To make Dialogue - 
*Use current example scriptable objects as examples of the extend of the system.

Dialogue is a scriptable object, so I added it to the creatable objects in Unity.
Just go to the top bar, under Assests, Create, Dialogue, and NPC. It will create
a scriptable object. Scriptable Objects are stored in the Assets folders under
Dialogue. Currently there are three test scriptable objects that test various
things of the dialogue system, Foxgirl 1,2,3. To into dialogue into the system go
to the DialogueManager in the Unity Program, in the variable space CharacterDialogue
drag the desired scriptable object from the Dialogue folder to the space,
overwriting anything in their currenty.The following is to write the specfic form
for each dialogue space:

Size - number of pieces of dialogue. Need to split dialogue to have enough space
in dialogue text box.
*Array start from 0, not 1. So size of 4 will have 4 boxes, but marked 0 to 3.

3 Array Areas - separator is the ~ sign which is generally next to the 1 key, on
the same key as `

Sentence -
Normal Format - <dialogue>
Branching dialogue with 2 choices - Branch~<dialogue>~<choice1>~<choice2>
Branching dialogue with 3 choices - Branch3~<dialogue>~<choice1>~<choice2>~<choice3>

Branches -
Normal Format - <groupIndex>~<dialogueChoice>~<dialogue>~<playerChoice>
*groupIndex - Group is the possibilies of a branch. Start from 0.
*dialogueChoice - 0,1 for 2 choices. 0,1,2 for choices
*playerChoice - Choice that matters to this dialogue. Number references PlayerChoiceManager's index
in the array.
*Can input multiple pieces of dialogue for each choice, just use same dialogueChoice

Branching Branch with 2 choices - Normal Format~Branch~<choice1>~<choice2>~<branchesInBranchesIndex>
*branchesInBranchesIndex - references the branchesInBranches array that contains
 the results of the choice made in the Branch

Branching Branch with 3 choices - Normal Format~Branch3~<choice1>~<choice2>~<choice3>~<branchesInBranchesIndex>


BranchesInBranches - 
*Generally Follows same formats as Branches

Normal Format - <ReferenceIndex>~<dialogueChoice>~<dialogue>~<playerChoice>
Branching Branch with 2 choices - Normal Format~Branch~<choice1>~<choice2>~<branchesInBranchesIndex>
Branching Branch with 3 choices - Normal Format~Branch3~<choice1>~<choice2>~<choice3>~<branchesInBranchesIndex>

*Because the way I did it, the example Fox Girl 3 uses 1 to start the reference
indexes for the branhesInBranches which is arbitary and could of started with 0
to be consist with the other implementations.














