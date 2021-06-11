using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private GameObject lastPlayed;
    //private GameObject cardPlayed;//experiment to rename played cards to incrementing numbers so i can find them later.

    private GameObject lastPlayedAI;
    //______________________________________
    private double gameTurn;

    private int[] deck;
    private int deckPosition;

    private bool PlayerMustBlock;
    private bool AIMustBlock;

    private bool Blitz;

    private int[] AIhand;
    private int AIhandindex;
    //These are all of the cards for the deck
    //___________________________________________
    public GameObject SPTackle;
    public GameObject SPInterception;
    public GameObject SPBlocked_Kick;
    public GameObject SPBlitz;
    public GameObject SPFumble;
    public GameObject SPPass_Completion;
    public GameObject SPFirst_Down;
    public GameObject SP5Yard_Run;
    public GameObject SPConversion;
    public GameObject SPField_Goal;
    public GameObject SPExtra_Point;
    public GameObject SPHail_Mary;
    public GameObject SPPassing_TD;
    public GameObject SPRushin_TD;
    public GameObject SPEnd_Of_Quarter;
    //___________________________________________
    private float animationSpeed;

    //
    // Start is called before the first frame update
    void Start()
    {
        lastPlayed = null;
        lastPlayedAI = null;

        gameTurn = 1;


        buildDeck();
        deckPosition = 0;

        PlayerMustBlock = false;
        AIMustBlock = false;

        Blitz = false;

        AIhand = new int[100];
        AIhandindex = 0;
        for (int i = 0; i < 5; i++)//AI draws it's initial 4 cards, draws the last when it's first turn starts
        {
            AIdraw();
        }
        //print(AIhand[0] + " " + AIhand[1] + " " + AIhand[2] + " " + AIhand[3] + " " + AIhand[4] + " " + AIhand[5] + " ");

        animationSpeed = 700;
    }

    private void buildDeck()
    {
        deck = new int[]
            {
                1,1,1,1,
                2,2,2,
                3,3,3,3,3,3,3,3,3,3,3,
                4,4,4,4,4,4,4,4,
                5,5,5,5,
                6,6,6,6,6,6,6,6,
                7,7,7,7,7,7,7,7,
                8,8,8,8,8,8,8,8,
                9,9,9,9,9,9,
                10,10,10,10,10,10,10,
                11,11,11,11,11,11,11,11,11,11,11,
                12,12,
                13,13,13,13,13,13,
                14,14,14,14,14,14,
                15,15,15,15,15,15,15,15
            };
        /* here is the breakdown of what card each number represents, the second column is the quantity of that card
    * assigned #   quantity    CardName   
        1	        4	        Tackle 
        2	        3	        Interception
        3	        11	        Blocked Kick
        4	        8	        Blitz
        5	        4	        Fumble
        6	        8	        Pass Completion
        7	        8	        First Down
        8	        8	        5-Yard Run
        9	        6	        Conversion
        10	        7	        Field Goal
        11	        11	        Extra Point
        12	        2	        Hail Mary
        13      	6       	Passing TD
        14      	6       	Rushing TD
        15	        8       	End of Quarter
        */
        int temp;
        int r1;
        int r2;
        
        for (int i = 0; i < 400; i++)//this is my shuffle system. rolls two numbers and swaps the numbers at those positions. can be run more if it's not random enough.
        {
            r1 = Random.Range(0, 100);
            r2 = Random.Range(0, 100);
            temp = deck[r1];
            deck[r1] = deck[r2];
            deck[r2] = temp;
        }
        
    }
    public GameObject draw()// this is the function for drawing cards. it takes the next number in the deck, determines what card it is, and instantiates it, returning the object
    {
        int cardID = deck[deckPosition];
        deckPosition++;
        if (deckPosition > 99)
        {
            buildDeck();
            deckPosition = 0;
        }
        GameObject g;
        switch (cardID)
        {
            case 1:
                g = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//position is arbitrary because the function calling this should change it
                break;
            case 2:
                g = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 3:
                g = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 4:
                g = Instantiate(SPBlitz, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 5:
                g = Instantiate(SPFumble, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 6:
                g = Instantiate(SPPass_Completion, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 7:
                g = Instantiate(SPFirst_Down, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 8:
                g = Instantiate(SP5Yard_Run, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 9:
                g = Instantiate(SPConversion, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 10:
                g = Instantiate(SPField_Goal, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 11:
                g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 12:
                g = Instantiate(SPHail_Mary, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 13:
                g = Instantiate(SPPassing_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 14:
                g = Instantiate(SPRushin_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 15:
                g = Instantiate(SPEnd_Of_Quarter, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;

            default:
                g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
        }
        return g;
    }
    //_____________________________________________________________________________

    public bool PlayerBlock()//check whether the player needs to block (if their opponent is over 21 and about to win)
    {

        return PlayerMustBlock;
    }
    public bool AIBlock()//check whether the AI needs to block
    {
        return AIMustBlock;
    }

    public void setPlayerBlock(bool b)//used to set playerblock if the player's next card needs to put AI score under 21 to avoid losing. Also to reset if the player has blocked
    {
        PlayerMustBlock = b;
    }
    public void setAIBlock(bool b)//used to set AIblock if the AI's next card needs to put player score under 21 to avoid losing. also to reset if the AI has blocked
    {
        AIMustBlock = b;
    }
    //______________________________________________________________________________
    public double getTurn()//returns the round number for determining who's turn it is
    {
        return gameTurn;
    }
    public void nextTurn()//advances the round number
    {
        gameTurn++; //the player cards check this value to see if the player is allowed to play a card they clicked on, this script checks that value in update...
        //            to make an action when it's turn is up. The players turn is every odd number, the computer is every even number. 
        print("GameTurn: " + gameTurn);
    }
    public void halfNextTurn()
    {
        gameTurn += .5;
        print("GameTurn: " + gameTurn);
    }
    //________________________________________________________________
    public bool getBlitz()
    {
        return Blitz;
    }
    public void setBlitz(bool b)
    {
        Blitz = b;
    }
    //_________________________________________________________________
    public void playerWin()
    {
        print("AI score: " + GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text + "   Player score: " + GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);
        
        SceneManager.LoadScene("SPEndScreenV");
        //GameObject.FindWithTag("PlayerWin").SetActive(true);
    }
    public void AIWin()
    {
        print("AI score: " + GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text + "   Player score: " + GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);

        SceneManager.LoadScene("SPEndScreenL");
        //GameObject.FindWithTag("PlayerLoss").SetActive(true);
    }

    //_________________________________________________________________
    public GameObject getLastPlayed()// when offensive cards are played, they are renamed to a number, this returns the number of the most recently played card for easy lookup
    {
        return lastPlayed;// it's minus 1 because I increment cardPlayed after setting the name in the next function
    }
    public void setLastPlayed(GameObject g) //changes the name of played cards to a number that is incremented. this allows the card to be found with a search by name
    {
        lastPlayed = g;
    }

    public void setLastPlayedAI(GameObject g)
    {
        lastPlayedAI = g;
    }
    public GameObject getLastPlayedAI()
    {
        return lastPlayedAI;
    }
    //__________________________________________________________________
    public void AIdraw()
    {
        //print("Ai drew: " + deck[deckPosition]);
        if(deckPosition > 99)
        {
            print("deckPosition: "+ deckPosition);
            buildDeck();
            deckPosition = 0;
        }
        AIhand[AIhandindex++] = deck[deckPosition++];
    }

    public void PScoreChange(int a)
    {
        TextMeshProUGUI t = GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>();
        t.text = (int.Parse(t.text) + a).ToString();
    }
    public void EScoreChange(int a)
    {
        TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();
        t.text = (int.Parse(t.text) + a).ToString();
    }
    public int GetAIScore()
    {
        return int.Parse(GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text);
    }
    public int GetPlayerScore()
    {
        return int.Parse(GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);
    }

    //____________________________________________________________________________________________________

    public void AIPlay()//this is to determine which card the AI will play. called in update()
    {
        if (GetAIScore() >= 21)
        {
            AIWin();
            return;
        }
        lastPlayedAI = null;//prevents player from blocking cards played on previous turns

        if (AIBlock())//if the AI needs to block a win from the player
        {

            string firstLetter = (lastPlayed.name.Substring(2, 1));

            switch (firstLetter) // Animations added
            {
                case "r"://rushing td
                    for (int i = 0; i < 100; i++)//searches through the AI hand
                    {
                        if (AIhand[i] == 1)
                        {
                            AIhand[i] = 0;//take the card out of hand


                            GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            /*GameObject card = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//create the card to discard
                            PScoreChange(-1 * int.Parse(lastPlayed.tag));//subtract the points from player's score
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;

                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";*/

                            return;
                        }
                    }

                    break;
                case "p"://passing td
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 2)
                        {
                            AIhand[i] = 0;//take the card out


                            GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
                case "h"://hail mary
                         //can't be blocked, only blitzed
                    break;

                case "c"://conversion
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 1)
                        {
                            AIhand[i] = 0;//take the card out of hand


                            GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//create the card to discard
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                        if (AIhand[i] == 2)
                        {
                            AIhand[i] = 0;//take the card out

                            GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
                case "f":
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 3)
                        {
                            AIhand[i] = 0;//take the card out

                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
                case "e":
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 3)
                        {
                            AIhand[i] = 0;//take the card out

                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());

                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
            }
            //if this switch fails, the card must be blitzed | Animations added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] == 4)
                {
                    /*AIhand[i] = 0;//take the card out
                    PScoreChange(-1 * int.Parse(lastPlayed.tag));//subtract from player
                    EScoreChange(int.Parse(lastPlayed.tag));//add to AI
                    lastPlayed.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);//move to AI scored on board
                    setLastPlayedAI(lastPlayed);
                    lastPlayed.AddComponent<Blitzable>();
                    setLastPlayed(null);
                    GameObject card = Instantiate(SPBlitz, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                    card.tag = "discard"; */

                    AIhand[i] = 0;
                    GameObject g = Instantiate(SPBlitz, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                    g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                    g.transform.position = new Vector3(540, 730, 0);
                    Destroy(g.GetComponent<CardHover>());

                    StartCoroutine(AIBlitzAndDiscardAnimation(g, lastPlayed, animationSpeed));

                    return;
                }
            }
            //if there is no blitz, the player wins
            playerWin();
            return;

        }
        else//this means there is no priority for what the AI plays
        {
            
            if (lastPlayed != null)//if there is a card to be blocked
            {
                //try to play defensive cards, if any are owned | Animations updated
                string firstLetter = (lastPlayed.name.Substring(2, 1));
                switch (firstLetter)
                {
                    case "r"://rushing td
                        for (int i = 0; i < 100; i++)//searches through the AI hand
                        {
                            if (AIhand[i] == 1)
                            {
                                AIhand[i] = 0;//take the card out of hand

                                
                                GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                                /*GameObject card = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//create the card to discard
                                PScoreChange(-1 * int.Parse(lastPlayed.tag));//subtract the points from player's score
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";*/

                                return;
                            }
                        }

                        break;
                    case "p"://passing td
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 2)
                            {
                                AIhand[i] = 0;//take the card out


                                GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                                /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                GameObject card = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";
                                Destroy(card.GetComponent<CardHover>());*/
                                return;
                            }
                        }

                        break;
                    case "h"://hail mary
                             //can't be blocked, only blitzed
                        break;

                    case "c"://conversion
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 1)
                            {
                                AIhand[i] = 0;//take the card out of hand


                                GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                                /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                GameObject card = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//create the card to discard
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";
                                Destroy(card.GetComponent<CardHover>());*/
                                return;
                            }
                            if (AIhand[i] == 2)
                            {
                                AIhand[i] = 0;//take the card out

                                GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                                /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                GameObject card = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";
                                Destroy(card.GetComponent<CardHover>());*/
                                return;
                            }
                        }

                        break;
                    case "f":
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 3)
                            {
                                AIhand[i] = 0;//take the card out

                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                                /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";
                                Destroy(card.GetComponent<CardHover>());*/
                                return;
                            }
                        }

                        break;
                    case "e":
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 3)
                            {
                                AIhand[i] = 0;//take the card out

                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                                /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                                Destroy(lastPlayed.gameObject);
                                lastPlayed = null;
                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                                card.tag = "discard";
                                Destroy(card.GetComponent<CardHover>());*/
                                return;
                            }
                        }

                        break;

                }

                var r = Random.Range(1, 4);//flip a coin to decide whether to try and blitz, if Ai even has one
                if (r == 1)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 4)
                        {
                            /*AIhand[i] = 0;//take the card out
                            PScoreChange(-1 * int.Parse(lastPlayed.tag));//subtract from player
                            EScoreChange(int.Parse(lastPlayed.tag));//add to AI
                            lastPlayed.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);//move to AI scored on board
                            lastPlayed.AddComponent<Blitzable>();
                            lastPlayed = null;
                            GameObject card = Instantiate(SPBlitz, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            return;*/
                            AIhand[i] = 0;
                            GameObject g = Instantiate(SPBlitz, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());

                            StartCoroutine(AIBlitzAndDiscardAnimation(g, lastPlayed, animationSpeed));
                            return;
                        }
                    }
                }
            }

            //play offensive cards, if owned | Animations updated
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] >= 9 && AIhand[i] <= 14)
                {

                    GameObject g;
                    switch (AIhand[i])
                    {
                        case 9:
                            AIhand[i] = 0;
                            g = Instantiate(SPConversion, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            //g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            /*EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/

                            return;

                        case 10:
                            AIhand[i] = 0;
                            g = Instantiate(SPField_Goal, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 11:
                            AIhand[i] = 0;
                            g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 12:
                            AIhand[i] = 0;
                            g = Instantiate(SPHail_Mary, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 13:
                            AIhand[i] = 0;
                            g = Instantiate(SPPassing_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 14:
                            AIhand[i] = 0;
                            g = Instantiate(SPRushin_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));

                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;

                    }
                }
            }

            //play cards to draw | Animations updated | audio added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] >= 6 && AIhand[i] <= 8)
                {

                    GameObject g;
                    switch (AIhand[i])
                    {

                        case 6:
                            AIhand[i] = 0;
                            g = Instantiate(SPPass_Completion, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();//draw 1
                            g.tag = "audio";
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));

                            return;

                        case 7:
                            AIhand[i] = 0;
                            g = Instantiate(SPFirst_Down, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();//first down is draw 2
                            AIdraw();
                            g.tag = "audio";
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));

                            return;

                        case 8:
                            AIhand[i] = 0;
                            g = Instantiate(SP5Yard_Run, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();
                            g.tag = "audio"; 
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                            return;

                    }
                }
            }
            //play cards to skip player turn | Animation updated | audio added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] == 5)
                {
                    
                    AIhand[i] = 0;
                    GameObject g = Instantiate(SPFumble, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                    g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                    g.transform.position = new Vector3(540, 730, 0);
                    Destroy(g.GetComponent<CardHover>());
                    nextTurn();//skips to next turn, then update() switches turn again, so AI will go twice
                    g.GetComponent<AudioSource>().Play();
                    StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                    return;
                }
                else if (AIhand[i] == 15)
                {
                    
                    AIhand[i] = 0;
                    GameObject g = Instantiate(SPEnd_Of_Quarter, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                    g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                    g.transform.position = new Vector3(540, 730, 0);
                    Destroy(g.GetComponent<CardHover>());
                    nextTurn();//skips to next turn, then update() switches turn again, so AI will go twice
                    g.GetComponent<AudioSource>().Play();
                    StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                    return;
                }
            }
            //probably won't get this far, but skip the AI turn if it does
            return;
        }
        
    }  
    
    IEnumerator AIOffensiveAnimation(GameObject card, float speed) //added audio
    {
        Vector3 targetPosition = GameObject.FindWithTag("AIScored").transform.position;
        float rotation = 0;
        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)
        {
            if (card.transform.rotation != Quaternion.Euler(0, 0, 90))
            {
                card.transform.rotation = Quaternion.Euler(0, 0, rotation);
                rotation += .25f;
            }
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        card.transform.rotation = Quaternion.identity;
        card.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);

        card.GetComponent<AudioSource>().Play();
        EScoreChange(int.Parse(card.tag));
        setLastPlayedAI(card);
        card.AddComponent<Blitzable>();
        
        if (GetAIScore() >= 21)
        {
            setPlayerBlock(true);
        }
        else
        {
            setPlayerBlock(false);
        }
        halfNextTurn();
        //.................................
    }


    //____________________________________________________________________________________________________
    private IEnumerator AIBlitzAndDiscardAnimation(GameObject card, GameObject lastPlayedCard, float speed)
    {
        yield return StartCoroutine(AIBlitzAnimation(card, lastPlayedCard, speed));
        
        yield return StartCoroutine(AIDiscardAnimation(card, speed));
    }
    private IEnumerator AIBlitzAnimation(GameObject card, GameObject lastPlayedCard, float speed) //added audio
    {
        Vector3 targetPosition = lastPlayedCard.transform.position;

        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)//comparing vector3's didnt work for some reason :)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            
            yield return null;
        }
        card.GetComponent<AudioSource>().Play();
        setLastPlayedAI(lastPlayedCard);
        PScoreChange(-1 * int.Parse(lastPlayedCard.tag));//subtract the points from player's score
        EScoreChange(int.Parse(lastPlayedCard.tag));
        lastPlayedCard.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
    }
    //____________________________________________________________________________________________________
    private IEnumerator AIBlockAndDiscardAnimation(GameObject card, GameObject lastPlayedCard, float speed)
    {
        yield return StartCoroutine(AIBlockAnimation(card, lastPlayedCard, speed));
        
        yield return StartCoroutine(AIDiscardAnimation(card, speed));
    }
    IEnumerator AIBlockAnimation(GameObject card, GameObject lastPlayedCard, float speed) //added audio
    {
        
        Vector3 targetPosition = lastPlayedCard.transform.position;
        

        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)//comparing vector3's didnt work for some reason :)
        {

            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        card.GetComponent<AudioSource>().Play();

        PScoreChange(-1 * int.Parse(lastPlayedCard.tag));//subtract the points from player's score
        Destroy(lastPlayedCard.gameObject);
    }
    IEnumerator AIDiscardAnimation(GameObject card, float speed)
    {
        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)
        {
            
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }
        if (card.tag == "audio")
        {
            card.GetComponent<AudioSource>().Play();
        }
        card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
        card.tag = "discard";

        if (GetPlayerScore() >= 21) //check these and add them to player cards after animations
        {
            playerWin();
        }
        else
        {
            setAIBlock(false);
        }
        if (GetAIScore() >= 21)
        {
            setPlayerBlock(true);
        }
        else
        {
            setPlayerBlock(false);
        }

        halfNextTurn();

    }
    //____________________________________________________________________________________________________
    //____________________________________________________________________________________________________
    private void Update()
    {
        if (gameTurn %2 == 0 && gameTurn % 1 == 0)
        {

            AIdraw();
            halfNextTurn();

            AIPlay();
            
            //nextTurn();
            
            //Draw card for player
            GameObject newCard = draw();
            newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            lastPlayed = null; // prevents AI from blocking cards played on previous turns, only newest card can be blocked
            
        }
    }
}
