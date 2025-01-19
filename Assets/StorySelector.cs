using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum StoryPackName
{
    CATCH_NOTE,
    END
}


public class StorySelector : MonoBehaviour
{
    public int iSelectedStory;
    public StoryPackName eSelectedStory;

    public string[] StoryName1 = { nameof(StoryPackName.CATCH_NOTE) };

    public void SelectStory(StoryPackName _name)
    {
        eSelectedStory = _name;
    }

    public StoryPackName SelectPlayButton()
    {
        return eSelectedStory;
    }

}
