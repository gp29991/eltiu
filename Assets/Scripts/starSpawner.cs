using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSpawner : MonoBehaviour
{

    public GameObject star;
    private float minX;
    private float maxX;

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        StartCoroutine(zacznijSpawn());
    }

    IEnumerator zacznijSpawn()
    {
        yield return new WaitForSeconds(Random.Range(4f, 9f));

        GameObject k = Instantiate(star);

        float x = Random.Range(minX, maxX);

        k.transform.position = new Vector2(x, transform.position.y);

        StartCoroutine(zacznijSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
