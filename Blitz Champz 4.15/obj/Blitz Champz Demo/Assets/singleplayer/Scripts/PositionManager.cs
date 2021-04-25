using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionManager : MonoBehaviour
{
    private int playerCardPosition;// Determines the x coordinate for offensive cards that have been played
    private float playerScoredZ;

    private float enemyScoredZ;
    private int enemyCardPosition;

    private GameObject lastPlayed;
    private int cardPlayed;//experiment to rename played cards to incrementing numbers so i can find them later.
    
    private int gameTurn;

    private int[] deck;
    private int deckPosition;

    public GameObject Green;
    public GameObject Green2;
    public GameObject Green3;

    public GameObject SPTackle;
    // Start is called before the first frame update
    void Start()
    {
        playerCardPosition = -375;
        enemyCardPosition = -375;
        playerScoredZ = -1.0f;
        enemyScoredZ = -1.0f;
        lastPlayed = null;
        cardPlayed = 0;
        gameTurn = 1;


        buildDeck();
        deckPosition = 0;
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
        /*
        1	4	Tackle 
        2	3	Interception
        3	11	Blocked Kick
        4	8	Blitz
        5	4	Fumble
        6	8	Pass Completion
        7	8	First Down
        8	8	5-Yard Run
        9	6	Conversion
        10	7	Field Goal
        11	11	Extra Point
        12	2	Hail Mary
        13	6	Passing TD
        14	6	Rushing TD
        15	8	End of Quarter
        */
        int temp;
        int r1;
        int r2;
        
        for (int i = 0; i < 300; i++)
        {
            r1 = Random.Range(0, 100);
            r2 = Random.Range(0, 100);
            temp = deck[r1];
            deck[r1] = deck[r2];
            deck[r2] = temp;
        }
        print(deck[0] + " "+ deck[2] +" "+ deck[3]);
    }
    public GameObject draw()
    {
        int cardID = deck[deckPosition];
        deckPosition++;
        GameObject g;
        switch (cardID)
        {
            case 1:
                g = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                break;
            case 2:
                g = Instantiate(Green2, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                break;
            default:
                g = Instantiate(Green, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                break;
        }
        return g;
    }

    //______________________________________________________________________________
    public int getTurn()//returns the round number for determining who's turn it is
    {
        return gameTurn;
    }
    public void nextTurn()//advances the round number
    {
        gameTurn = gameTurn + 1;
    }
    //________________________________________________________________
    public int getPlayerCardPosition()
    {
        playerCardPosition += 30;
        return playerCardPosition;
    }
    public int getEnemyCardPosition()
    {
        enemyCardPosition += 30;
        return enemyCardPosition;
    }
    //________________________________________________________________
    public float getPlayedScoredZ()
    {
        playerScoredZ -= .1f;
        return playerScoredZ;
    }
    public float getEnemyScoredZ()
    {
        enemyScoredZ -= .1f;
        return enemyScoredZ;
    }
    //_________________________________________________________________
    public int getLastPlayed()
    {
        return (cardPlayed-1);
    }
    public void setLastPlayed(GameObject g)
    {
        g.gameObject.name = cardPlayed.ToString();
        cardPlayed++;
    }
    //__________________________________________________________________
    private void Update()
    {
        if (gameTurn %2 == 0)
        {
            var r = Random.Range(1, 4);


            if (r == 1)
            {
                GameObject enemyCard = Instantiate(Green, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                enemyCard.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

                enemyCard.transform.localPosition = new Vector3(getEnemyCardPosition(), 160, getEnemyScoredZ());
                transform.rotation = Quaternion.Euler(0, 0, 90);
                setLastPlayed(enemyCard);

                TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();

                t.text = (int.Parse(t.text) + int.Parse(enemyCard.tag)).ToString();

                gameTurn++;
            }
            else if(r == 2)
            {
                GameObject enemyCard = Instantiate(Green2, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                enemyCard.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

                enemyCard.transform.localPosition = new Vector3(getEnemyCardPosition(), 160, getEnemyScoredZ());
                transform.rotation = Quaternion.Euler(0, 0, 90);
                setLastPlayed(enemyCard);

                TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();

                t.text = (int.Parse(t.text) + int.Parse(enemyCard.tag)).ToString();

                gameTurn++;
            }
            else if(r == 3)
            {
                GameObject enemyCard = Instantiate(Green3, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                enemyCard.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

                enemyCard.transform.localPosition = new Vector3(getEnemyCardPosition(), 160, getEnemyScoredZ());
                transform.rotation = Quaternion.Euler(0, 0, 90);
                setLastPlayed(enemyCard);

                TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();

                t.text = (int.Parse(t.text) + int.Parse(enemyCard.tag)).ToString();

                gameTurn++;
            }
        }
    }
}
