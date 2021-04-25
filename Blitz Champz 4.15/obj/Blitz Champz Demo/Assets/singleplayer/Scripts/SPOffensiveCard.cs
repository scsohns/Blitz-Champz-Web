using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPOffensiveCard : MonoBehaviour 
{

    private void OnMouseDown()
    {
        if(transform.rotation != Quaternion.Euler(0, 0, 90))
        { 
            GameObject g = GameObject.FindWithTag("Manager");
            PositionManager p = (PositionManager)g.GetComponent(typeof(PositionManager));
            if(p.getTurn() % 2 == 0)
            {
                return;
            }
            p.setLastPlayed(this.gameObject);
            print(this.gameObject);
            transform.localScale = new Vector3(15f, 15f, 0);
            transform.localPosition = new Vector3(p.getPlayerCardPosition(), 150, p.getPlayedScoredZ());
            transform.rotation = Quaternion.Euler(0, 0, 90);

            TextMeshProUGUI t = GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>();

            t.text = (int.Parse(t.text) + int.Parse(tag)).ToString();
            p.nextTurn();
        }
        
    }

}
