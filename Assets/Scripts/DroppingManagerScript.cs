using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingManagerScript : MonoBehaviour
{
    public Transform droppings;
    
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i <= 9; i ++) {
            
            Transform go = Instantiate(droppings);
            go.transform.parent = this.transform;
            Vector2 loc = new Vector2(i, 8);
            go.transform.position = loc;
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }
        for (int i = 0; i <= 9; i ++)
        {
            float x = .5f + (float)i;
            Transform go = Instantiate(droppings);
            go.transform.parent = this.transform;
            Vector2 loc = new Vector2(x, 8);
            go.transform.position = loc;
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }
        
        for(int i = 0; i <= 9; i ++) {
            
            Transform go = Instantiate(droppings);
            go.transform.parent = this.transform;
            Vector2 loc = new Vector2(-i, 8);
            go.transform.position = loc;
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }
        for (int i = 0; i <= 9; i ++)
        {
            float x = .5f + (float)i;
            Transform go = Instantiate(droppings);
            go.transform.parent = this.transform;
            Vector2 loc = new Vector2(-x, 8);
            go.transform.position = loc;
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
