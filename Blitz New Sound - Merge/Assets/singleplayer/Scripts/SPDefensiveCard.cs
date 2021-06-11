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

    private void OnMouseDown()//when the mouse is clicked down
    {
        if(tag != "discard")//makes sure the card is not discarded, you don't want to play cards from the discard pile
        {
            GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
            GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

            if(p.getTurn() % 2 == 0 || p.getTurn() %1 != 0)//verifies that it is the players turn. if it's not, the rest of the script won't run
            {
                return;
            }

            if (p.getLastPlayedAI() == null)//this is to check if there was a card played to block
            {
                return;
            }
            if (p.getBlitz() == true)//this is to check whether they played a blitz card, because that will not cycle the turn until they select the card to steal
            {
                return;
            }
            GameObject lastPlayedAI = p.getLastPlayedAI();
            string firstLetter = (lastPlayedAI.name.Substring(2,1));
            
            switch (firstLetter)//used to find which card was played last to determine if the card can block it. return if the card cannot.
            {
                case "r"://rushing td
                    if (int.Parse(tag) == 1)//1 is tackle
                    {
                        //block card
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "p"://passing td
                    if (int.Parse(tag) == 2)//2 is interception
                    {
                        //block card
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "h"://hail mary
                    //can't be blocked
                    return;
                case "c"://conversion
                    if (int.Parse(tag) == 1 || int.Parse(tag) == 2)
                    {
                        //block card
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "f":
                    if (int.Parse(tag) == 3)//3 is blocked kick
                    {
                        //block card
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "e":
                    if (int.Parse(tag) == 3)
                    {
                        //block card
                    }
                    else
                    {
                        return;
                    }
                    break;

            }

            //p.setLastPlayedAI(null);

            p.halfNextTurn();
            Destroy(GetComponent<CardHover>());
            

            StartCoroutine(BlockAndDiscard(lastPlayedAI.transform.position, 400));
            //StartCoroutine(Discard(400));
            //StartCoroutine(Block(lastPlayedAI.transform.position, 400));
            //TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>(); //access the enemy score object

            /*t.text = (int.Parse(t.text) - int.Parse(lastPlayedAI.tag)).ToString();//change the text of the score subtracting the tag of the last offensive card, which is the value of the card
            Destroy(lastPlayedAI); //this removes the card from the game because it has been blocked. it could also be discarded, but this saves memory
            
            //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
            
            tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
            transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
            if (p.PlayerBlock() == true && int.Parse(t.text) < 21)//if enemy score is now under 21, the player no longer needs to block
            {
                p.setPlayerBlock(false);
                
            }

            p.setLastPlayed(null);
            p.nextTurn(); */



        }
            
    }

    private IEnumerator BlockAndDiscard(Vector3 targetPosition, float speed)
    {
        yield return StartCoroutine(Block(targetPosition, speed));
        
        yield return StartCoroutine(Discard(speed));
    }
    IEnumerator Block(Vector3 targetPosition, float speed)
    {
        transform.localScale = new Vector3(1f, 1f, 0);
        

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)//comparing vector3's didnt work for some reason :)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }
        GetComponent<AudioSource>().Play();
    }
    IEnumerator Discard(float speed)
    {
        
        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        GameObject lastPlayedAI = p.getLastPlayedAI();

        TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>(); //access the enemy score object
        t.text = (int.Parse(t.text) - int.Parse(lastPlayedAI.tag)).ToString();//change the text of the score subtracting the tag of the last offensive card, which is the value of the card
        Destroy(lastPlayedAI); //this removes the card from the game because it has been blocked. it could also be discarded, but this saves memory

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }

        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
        if (p.GetAIScore() < 21)
        {
            p.setPlayerBlock(false);
        }

        p.setLastPlayed(null);
        p.halfNextTurn();

    }
}
