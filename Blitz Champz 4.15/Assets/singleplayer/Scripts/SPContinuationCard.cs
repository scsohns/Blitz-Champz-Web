using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPContinuationCard : MonoBehaviour
{
    
    public void OnMouseDown()
    {
        int tagNumber = int.Parse(tag);

        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        if (p.getBlitz() == true)//this is to check whether they played a blitz card, because that will not cycle the turn until they select the card to steal
        {
            return;
        }
        if (tagNumber == 6)
        { //blitz cards are given the tag number 6 

            p.setBlitz(true);

        }
        else
        {
            if (p.PlayerBlock() == true)//if they needed to block/blitz and tried to play another card, they lose
            {
                print("AI wins");
                p.AIWin();
                return;
            }
            else if (tagNumber <= 2)//first down, pass completion, or 5-yard run 
            {


                for (int i = 1; i <= tagNumber; i++) // draw cards for the number on the tag
                {

                    GameObject newCard = p.draw();
                    newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);


                }
                //tag = 10.ToString();
                p.nextTurn();

            }
            else if (tagNumber == 3)
            {//fumble or end of quarter
             //these make the player go again, so the turn # isnt changed
             //This is to draw a card for the players turn.

                p.setLastPlayedAI(null);
                GameObject newCard = p.draw();
                newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            }
        }
        
        
        tag = "discard";
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
        //transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1f, 1f, 1f);
        p.setLastPlayed(null);
        return;


    }
    public void Onclick()//for testing a draw button
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        GameObject newCard = p.draw();
        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
        print("Number of cards in hand: "+ GameObject.FindWithTag("PlayerArea").transform.childCount);
    }


}
