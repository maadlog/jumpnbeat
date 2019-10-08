using UnityEngine;
using System.Linq;

public class Wall : MonoBehaviour
{
    private Vector3 target;
    const float speed = 25f;

    public GameObject[] graces;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(transform.position.x,transform.position.y,-20);
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

    internal void SetGraces(bool[] gracesActive)
    {
        for (int i = 0; i < 8; i++)
        {
            if (!gracesActive[i]) {
                graces[i].transform.localScale = Vector3.zero;
            } else {
                graces[i].transform.localScale = new Vector3(3,8,1);
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
