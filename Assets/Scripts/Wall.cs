using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    private Vector3 target = new Vector3(0,0,-20);
    const float speed = 25f;

    float[] positionsX = new float[8];
    float[] positionsY = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        StartPositionSnaps();

        RandomizeGrace();
    }
    void StartPositionSnaps()
    {
        float width = this.GetComponentsInChildren<BoxCollider>().Where(x => x.gameObject.CompareTag("Harmful")).First().transform.localScale.x;
        float height = this.GetComponentsInChildren<BoxCollider>().Where(x => x.gameObject.CompareTag("Harmful")).First().transform.localScale.y;
        for (int i = 0; i < 8; i++)
        {
            positionsX[i] = width * i / 8;
        }
        for (int i = 0; i < 3; i++)
        {
            positionsY[i] = height* i / 3;
        }

        this.GetComponentsInChildren<Transform>().Where(x => x.gameObject.CompareTag("Grace")).First().localScale = new Vector3(width / 8f, height/3f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (System.Math.Abs(transform.position.z - target.z) < 0.2)
        {
            Destroy(this.gameObject);
        }

    }

    void RandomizeGrace()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.CompareTag("Grace"))
            {
                item.position = new Vector3(positionsX[Random.Range(0, 8)] -8.76f, positionsY[Random.Range(0, 3)]+0.3f, item.position.z);
            }
        } 
    }

    internal void Break()
    {
        /*foreach (var item in GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(item.gameObject);
        }*/
        GameObject.Destroy(this.gameObject);
    }
}
