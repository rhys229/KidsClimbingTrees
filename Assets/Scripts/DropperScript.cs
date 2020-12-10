using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperScript : MonoBehaviour
{
    public GameObject pinecones;
    private Vector2 position;
    
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        float delay = Random.Range(2f, 10f);
        float rate = Random.Range(2f,8f);
        InvokeRepeating("Drop",delay,rate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        
        int i = Random.Range(0,100);
        if (i > 40)
        {
            GameObject go = Instantiate(pinecones);
            go.transform.position = position;
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
