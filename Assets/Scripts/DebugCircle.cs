using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCircle : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        text.text = value.ToString();
    }
}
