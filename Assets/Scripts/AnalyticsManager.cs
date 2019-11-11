using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUIOne;
    public TextMeshProUGUI textMeshProUGUITwo;
    public TextMeshProUGUI textMeshProUGUIThree;
    public TextMeshProUGUI textMeshProUGUIFour;

    Dictionary<string, int> dictionary;

    public void addHit(string name)
    {
        int currentCount = 0;

        if(dictionary.ContainsKey(name))
        {
            dictionary[name]++;
        } else
        {
            dictionary.Add(name, 1);
        }

        //Debug.Log("Name: " + name + ", Count: " + dictionary[name]);

        if (textMeshProUGUIOne.gameObject.activeInHierarchy)
        {
            var myList = dictionary.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            //Debug.Log("Count: " + myList.Count);

            textMeshProUGUIOne.text = "Name: " + myList[0].Key + ", Count: " + myList[0].Value;

            if (myList.Count > 1)
            {
                textMeshProUGUITwo.text = "Name: " + myList[1].Key + ", Count: " + myList[1].Value;
            }

            if (myList.Count > 2)
            {
                textMeshProUGUIThree.text = "Name: " + myList[2].Key + ", Count: " + myList[2].Value;
            }

            if (myList.Count > 3)
            {
                textMeshProUGUIFour.text = "Name: " + myList[3].Key + ", Count: " + myList[3].Value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
