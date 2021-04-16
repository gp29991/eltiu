using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundColor : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerController pc = player.GetComponent<playerController>();
        switch (pc.level)
        {
            case 1:
                this.GetComponent<SpriteRenderer>().color = new Color(0.66f, 1f, 1f, 1f);
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().color = new Color(0.24f, 0.78f, 1f, 1f);
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().color = new Color(0.27f, 0.29f, 0.63f, 1f);
                break;
        }
    }
}
