using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager_SJY : MonoBehaviour
{
    public Animator[] Character;

    //Animation anim;

    public int CharacterNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterControl()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CharacterSelectRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CharacterSelectLeft();
        }

    }

    void CharacterSelectRight()
    {
        Debug.Log("Right");

        if (CharacterNum - 2 < 0)
        {
            Character[CharacterNum + 3].Play("Character_Select_Right_1");
            Debug.Log("Right1");
        }
        else { Character[CharacterNum - 2].Play("Character_Select_Right_1"); Debug.Log("Right1-1"); }
        if (CharacterNum - 1 < 0)
        {
            Character[CharacterNum + 4].Play("Character_Select_Right_2");
        }
        else { Character[CharacterNum - 1].Play("Character_Select_Right_2"); }
        Character[CharacterNum].Play("Character_Select_Right_3");
        if (CharacterNum + 1 > 4)
        {
            Character[CharacterNum - 4].Play("Character_Select_Right_4");
        }
        else { Character[CharacterNum + 1].Play("Character_Select_Right_4"); }
        if (CharacterNum == 0) { CharacterNum = 4; }
        else { CharacterNum--; }
    }

    void CharacterSelectLeft()
    {
        if (CharacterNum - 1 < 0)
        {
            Character[CharacterNum + 4].Play("Character_Select_Left_1");
        }
        else { Character[CharacterNum - 1].Play("Character_Select_Left_1"); }
        Character[CharacterNum].Play("Character_Select_Left_2");
        if (CharacterNum + 1 > 4)
        {
            Character[CharacterNum - 4].Play("Character_Select_Left_3");
        }
        else { Character[CharacterNum + 1].Play("Character_Select_Left_3"); }
        if (CharacterNum + 2 > 4)
        {
            Character[CharacterNum - 3].Play("Character_Select_Left_4");
        }
        else { Character[CharacterNum + 2].Play("Character_Select_Left_4"); }
        if (CharacterNum == 4) { CharacterNum = 0; }
        else { CharacterNum++; }
    }
}
