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
        if(p.getTurn() % 2 == 0 || p.getTurn() %1 != 0)//verifies that it is the players turn. if it's not, the rest of the script won't run
        {
            return;
        }
        if (p.getBlitz() == true)//this is to check whether they played a blitz card, because that will not cycle the turn until they select the card to steal
        {
            return;
        }
        if (tagNumber == 6)
        { //blitz cards are given the tag number 6 

            p.setBlitz(true);
            StartCoroutine(PlayBlitz(500));
            return;
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


                
                //tag = 10.ToString();
                p.halfNextTurn();
                StartCoroutine(DiscardDraw(500));
            }
            else if (tagNumber == 3)
            {//fumble or end of quarter
             //these make the player go again, so the turn # isnt changed
             //This is to draw a card for the players turn.

                p.setLastPlayedAI(null);
                GameObject newCard = p.draw();
                newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
                StartCoroutine(DiscardSkipTurn(500));
            }
        }

        return;
    }
    public void Onclick()//for testing a draw button
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        GameObject newCard = p.draw();
        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
    }

    IEnumerator DiscardDraw(float speed)
    {
        Destroy(GetComponent<CardHover>());
        transform.localScale = new Vector3(1f, 1f, 0);

        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }
        //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
        for (int i = 1; i <= int.Parse(tag); i++) // draw cards for the number on the tag
        {

            GameObject newCard = p.draw();
            newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);


        }

        GetComponent<AudioSource>().Play();

        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);

        p.setLastPlayed(null);
        p.halfNextTurn();

    }
    IEnumerator DiscardSkipTurn(float speed)
    {
        Destroy(GetComponent<CardHover>());
        transform.localScale = new Vector3(1f, 1f, 0);

        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }
        //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
        GetComponent<AudioSource>().Play();
        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);

        p.setLastPlayed(null);

    }

    IEnumerator PlayBlitz(float speed)
    {
        Destroy(GetComponent<CardHover>());
        transform.localScale = new Vector3(1f, 1f, 0);

        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }
        //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card

        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);

        GetComponent<AudioSource>().Play();

        if (p.GetPlayerScore() >= 21)
        {
            p.setAIBlock(true);
        }
        else
        {
            p.setAIBlock(false);
        }

        p.setLastPlayed(null);
        p.halfNextTurn();
    }
}
