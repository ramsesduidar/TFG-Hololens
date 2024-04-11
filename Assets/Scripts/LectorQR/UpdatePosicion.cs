using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTracking
{
    public class UpdatePosicion : MonoBehaviour
    {

        public GameObject relativo;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (relativo == null) return;

            Debug.Log("Update posicion: " + relativo.transform.position+ relativo.transform.rotation);
            gameObject.transform.SetPositionAndRotation(relativo.transform.position, relativo.transform.rotation);

        }
    }

}
