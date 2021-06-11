using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        if(tag == "discard")
        {
            return;
        }
        if(transform.rotation != Quaternion.Euler(0, 0, 90))
        {
            transform.localScale = new Vector3(25f, 25f, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, 100, -5);
        }
        
    }
    private void OnMouseExit()
    {
        if(tag == "discard")
        {
            return;
        }
        if (transform.rotation != Quaternion.Euler(0, 0, 90))
        {
            transform.localScale = new Vector3(15f, 15f, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
