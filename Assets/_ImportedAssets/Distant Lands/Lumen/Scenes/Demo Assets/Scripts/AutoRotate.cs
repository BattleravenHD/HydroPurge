using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DistantLands.Lumen.Demo
{

    public class AutoRotate : MonoBehaviour
    {


        public Vector3 rotateAngle;



        // Update is called once per frame
        void Update()
        {


            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateAngle * Time.deltaTime);


        }
    }
}