using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comments script for easier reading in inspector information about objects
/// </summary>

public class CommentScript : MonoBehaviour
{

    [TextArea]
    public string Notes = "Comment Here."; // Do not place your note/comment here. 
                                            // Enter your note in the Unity Editor.


}
