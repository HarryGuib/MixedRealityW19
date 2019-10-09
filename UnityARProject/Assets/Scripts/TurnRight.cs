using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : MonoBehaviour
{
    // Start is called before the first frame update
    public bool turn1;
    public bool turn2;
    public bool turn3;
    public bool turn4;
    private walkAround wkA;

    void Start()
    {
        MeshRenderer ms = GetComponent<MeshRenderer>();
        ms.enabled = false;
        turn1 = false;
        turn2 = false;
        turn3 = false;
        turn4 = false;
        wkA = GetComponent<walkAround>();

    }

    // Update is called once per frame
    void Update()
    {
     
    }
     void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.CompareTag("model") && this.name=="target4")
         {
                turn1 = true;
                turn2 = false;
                turn3 = false;
                turn4 = false;
             Debug.Log("number4");
         }
          if(target.gameObject.CompareTag("model") && this.name=="target1")
         {
                turn1 = false;
                turn2 = true;
                turn3 = false;
                turn4 = false;
             Debug.Log("number1");
         }
         if(target.gameObject.CompareTag("model") && this.name=="target2")
         {
                turn1 = false;
                turn2 = false;
                turn3 = true;
                turn4 = false;
             Debug.Log("number2");
         }
         if(target.gameObject.CompareTag("model") && this.name=="target3")
         {
                turn1 = false;
                turn2 = false;
                turn3 = false;
                turn4 = true;
             Debug.Log("number3");
         }
        
    }
}
