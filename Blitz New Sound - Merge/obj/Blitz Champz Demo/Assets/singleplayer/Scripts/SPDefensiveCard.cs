using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPDefensiveCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if(tag != "discard")
        {
            GameObject g = GameObject.FindWithTag("Manager");
            PositionManager p = (PositionManager)g.GetComponent(typeof(PositionManager));

            if (p.getTurn() % 2 == 0)
            {
                return;
            }

            if (GameObject.Find(p.getLastPlayed().ToString()) == null)
            {
                return;
            }
            GameObject lastPlayed = GameObject.Find(p.getLastPlayed().ToString());

            TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();

            t.text = (int.Parse(t.text) - int.Parse(lastPlayed.tag)).ToString();
            Destroy(lastPlayed);
            //lastPlayed.SetActive(false);//lastPlayed.transform.position = new Vector3(450, 100, 0);
            transform.position = new Vector3(450, 100, p.getPlayedScoredZ());
            transform.localScale = new Vector3(15f, 15f, 0);
            tag = "discard";
            p.nextTurn();
        }
            
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
