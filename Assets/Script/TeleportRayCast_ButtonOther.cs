using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRayCast_ButtonOther : MonoBehaviour {

    public LayerMask mask;
    public float rayMax = 1000.0f;    //Ryaの射出距離
    public bool RayFlag = false;    //Rayの射出フラグ
                                    //Rayの作成
    Ray ray;     

    //Rayが衝突したコライダーの情報を取得する.
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        //Rayの射出原点と射出方向
            //Rayの射出原点と射出方向
            
    }

    // Update is called once per frame
    void Update () {
        CreateRay();

	}

    void CreateRay()
    {
        ray = new Ray(this.transform.position, this.transform.forward);


        //スペースボタンでRayの射出のon/off
        //かつ、テレポとデストロイが同時処理。

        //Rayの発射フラグOn/Off
        if (Input.GetKey(KeyCode.Space) && !RayFlag)
        {
            RayFlag = true;
            Debug.Log("Ray射出 :" + RayFlag);
        }
        else if(Input.GetKey(KeyCode.Space) && RayFlag)
        {
            RayFlag = false;
        }

        if (Physics.Raycast(ray, out hit, rayMax, mask) && RayFlag)
        {
            //Tボタンを押して、かつRayのあたったオブジェクトのタグがStageなら
            if (Input.GetKey(KeyCode.T) && hit.collider.tag == "Stage")
            {
                //Z座標はそのままに、X,Y座標を移動させる
                //this.transform.position = new Vector3(hit.point.x,hit.point.y,this.transform.position.z);

                //このオブジェクトのpositionをRayがあたったところのオブジェクトに入れ替えてテレポ移動する。
                this.transform.position = hit.point;
            }

            //Dボタンを押して、かつRayのあたったオブジェクトのタグがStageなら
            if (Input.GetKey(KeyCode.D) && hit.collider.tag == "DestroyObj")
            {
                //ヒットしたオブジェクトをデストロイする。
                Destroy(hit.collider.gameObject);
            }

            //Rayがあたったオブジェクトの名前と、タグを表示。
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.tag);
        }

        //Rayの可視化
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

    }
}
