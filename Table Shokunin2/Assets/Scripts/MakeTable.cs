using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MakeTable : MonoBehaviour
{
    public enum HandType { Left,Right,None};
    HandType hand = HandType.None;
    private SteamVR_Behaviour_Pose thisHand;
    private Vector3 V1;
    private Vector3 V2;
    private float time;
    private float T1;
    private float T2;
    //
    public GameObject[] CornerSpot;
    public GameObject cylinder;
    public GameObject table;
    public Transform tablespot;
    private GameObject table_new = null; 


    private bool inAction = false;
    void Start() {
        thisHand = GetComponent<SteamVR_Behaviour_Pose>();

        
    }
    // Update is called once per frame
    void Update()
    {
        if (inAction)
        {
            time += Time.deltaTime;
        }

        // VR Step1 
        if (SteamVR_Input.GetStateDown("default", "Step1", SteamVR_Input_Sources.Any)){
            inAction = true;
            StartTable();
            V1 = thisHand.GetVelocity();
        }
        if (SteamVR_Input.GetStateUp("default", "Step1", SteamVR_Input_Sources.Any)) {
            inAction = false;
            V2 = thisHand.GetVelocity();
            StartCoroutine(Acc_Cal());

        }
        
    }

    IEnumerator Acc_Cal() {
        var temp = (V2 - V1) / time;
        float a = temp.z;
        print(a);
        //print("V1" + V1);
        //print("V2" + V2);
        //print("T1" + T1);
        //print("T2" + T2);
        ScaleTest st = table_new.GetComponent<ScaleTest>();
        st.setTable(a);
        time = 0f;
        
        var tmp = table_new;
        table_new = null;
        Destroy(tmp, 5.0f);
        yield return  null;
    }
    void StartTable() {
        GameObject a = Instantiate(table,tablespot.position,Quaternion.Euler(0,tablespot.rotation.eulerAngles.y,0),null);
        //ScaleTest st = a.GetComponent<ScaleTest>();
        table_new = a;
    }
    
}
