
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        textLabel.text = "Hello World!!\nThis is the text that I Wrote in the Code"; 
    }
}

