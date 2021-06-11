using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCards : MonoBehaviour
{

    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerHand = Instantiate(PlayerArea, new Vector3(0, -187, 0), Quaternion.identity) as GameObject;
        PlayerHand.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

    }

    public void Onclick()
    {
        GameObject g = GameObject.FindWithTag("Manager");
        PositionManager p = (PositionManager)g.GetComponent(typeof(PositionManager));

        for (var i = 0; i < 8; i++)
        {
            var r = Random.Range(1, 4);


            if (r == 1)
            {
                
                Instantiate(Red, new Vector3(-420 + (i * 120), r, 0), Quaternion.identity).transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            }
            else if (r == 2)
            {
                Instantiate(Green, new Vector3(-420 + (i * 120), r, 0), Quaternion.identity).transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            }
            else
            {
                /*GameObject j = p.draw();
                j.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
                j.transform.position = new Vector3(-420 + (i * 120), r, 0);
                j.transform.rotation = Quaternion.Euler(0, 0, 0);
                print("in else");*/
                Instantiate(Yellow, new Vector3(-420 + (i * 120), r, 0), Quaternion.identity).transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            }

            //GameObject playerCard = Instantiate(Red, new Vector3(-420+(i * 120), r, 0), Quaternion.identity);
            //playerCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            this.gameObject.SetActive(false);
        }
        


    }

}
