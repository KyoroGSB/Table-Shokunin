using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTest : MonoBehaviour
{
    public GameObject[] CornerSpot;
    public GameObject cylinder;
    public float TimeScale = 0.1f;
    public float Stat_scale = 2f;
     // Start is called before the first frame update
    private MakeTable MT;
    private Rigidbody rb;
    public GameObject smoke;
    void Start()
    {
        MT = GetComponent<MakeTable>();
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(LerpLength(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            //this.transform.localScale = new Vector3(transform.localScale.x,
            //    transform.localScale.y, transform.localScale.z*1.5f
             //);


            
        }
    }
    public void setTable(float a) {

        StartCoroutine(LerpLength(a));
       
    }

    IEnumerator LerpLength(float a) {
        if (a < 0)
        {
            a = -a;
        }
        float progress = 0;

        var FinalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * a * 0.2f * Stat_scale);
        while (progress <= 1)
        {
            if (transform.localScale == FinalScale)
            {
                progress = 2;
            }
            transform.localScale = Vector3.Lerp(transform.localScale, FinalScale, progress);
            progress += Time.deltaTime * TimeScale;
            yield return null;
            if (transform.localScale == FinalScale)
            {
                progress = 2;
            }
            
        }
        
        transform.localScale = FinalScale;
        //make table feet
        GameObject s = Instantiate(smoke,this.transform.position,transform.rotation,null);
        Destroy(s,3.0f);
        for (int i = 0; i < 4; i++)
        {
            
                GameObject obj = Instantiate(cylinder, CornerSpot[i].transform.position, CornerSpot[i].transform.rotation, this.transform);
                obj.transform.localScale = new Vector3(0.1f, 0.4f, 0.1f / FinalScale.z);
              
        }
        rb.isKinematic = false;

        yield return null;

    }
}
