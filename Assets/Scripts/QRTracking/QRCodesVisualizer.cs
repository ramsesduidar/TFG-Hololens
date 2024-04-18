using System;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.OpenXR;

namespace QRTracking
{
    /*
     * QRCodesVisualizer.cs - Handles all QR code visualizing in the scene 
     * and instantiates all QR codes in the local list kept in QRCodesManager.
    */

    public class HoloEventArgs : EventArgs
    {
        public string ValorQR { get; }
        public Guid Tracker { get; }

        public HoloEventArgs(string valorQR, Guid tracker)
        {
            this.ValorQR = valorQR;
            this.Tracker = tracker;
        }
    }

    public class QRCodesVisualizer : MonoBehaviour
    {
        public GameObject qrCodePrefab;

        private SortedDictionary<System.Guid, GameObject> qrCodesObjectsList;
        private Queue<ActionData> pendingActions = new Queue<ActionData>();
        private bool clearExisting = false;

        private GameObject current_qr;

        public event EventHandler<HoloEventArgs> eventoHolo;

        struct ActionData
        {
            public enum Type
            {
                Added,
                Updated,
                Removed
            };
            public Type type;
            public Microsoft.MixedReality.QR.QRCode qrCode;

            public ActionData(Type type, Microsoft.MixedReality.QR.QRCode qRCode) : this()
            {
                this.type = type;
                qrCode = qRCode;
            }
        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("QRCodesVisualizer start");
            qrCodesObjectsList = new SortedDictionary<System.Guid, GameObject>();

            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.QRCodeAdded += Instance_QRCodeAdded;
            QRCodesManager.Instance.QRCodeUpdated += Instance_QRCodeUpdated;
            QRCodesManager.Instance.QRCodeRemoved += Instance_QRCodeRemoved;
            if (qrCodePrefab == null)
            {
                throw new System.Exception("Prefab not assigned");
            }
        }
        private void Instance_QRCodesTrackingStateChanged(object sender, bool status)
        {
            if (!status)
            {
                clearExisting = true;
            }
        }

        private void Instance_QRCodeAdded(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            if (QRCodesManager.Instance.fechaInicio > e.Data.LastDetectedTime)
                return;

            Debug.Log("QRCodesVisualizer Instance_QRCodeAdded");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, e.Data));
            }
        }

        private void Instance_QRCodeUpdated(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            if (QRCodesManager.Instance.fechaInicio > e.Data.LastDetectedTime)
                return;

            Debug.Log("QRCodesVisualizer Instance_QRCodeUpdated");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, e.Data));
            }
        }

        private void Instance_QRCodeRemoved(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeRemoved");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, e.Data));
            }
        }

        private void HandleEvents()
        {
            lock (pendingActions)
            {
                while (pendingActions.Count > 0)
                {
                    var action = pendingActions.Dequeue();
                    Debug.Log($"QRCodesVisualizer HandleEvents: {action.type}");
                    Debug.Log($"QRCodeDATA: {action.qrCode.Data}");

                    if (action.type == ActionData.Type.Added)
                    {
                        GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                        qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                        qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);

                        if (this.current_qr != null) this.current_qr.SetActive(false);

                        this.current_qr = this.qrCodesObjectsList[action.qrCode.Id];

                        this.eventoHolo?.Invoke(this, new HoloEventArgs(action.qrCode.Data, action.qrCode.SpatialGraphNodeId));
                        
                        current_qr.SetActive(true);

                    }
                    else if (action.type == ActionData.Type.Updated)
                    {
                        if (!qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                            qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                            qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);
                        }

                        if (this.current_qr != null) current_qr.SetActive(false);

                        this.current_qr = this.qrCodesObjectsList[action.qrCode.Id];

                        current_qr.SetActive(true);
                        this.eventoHolo?.Invoke(this, new HoloEventArgs(action.qrCode.Data, action.qrCode.SpatialGraphNodeId));

                        Debug.Log("Qr actualizado: " + action.qrCode.Data);
                    }
                    else if (action.type == ActionData.Type.Removed)
                    {
                        if (qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            Destroy(qrCodesObjectsList[action.qrCode.Id]);
                            qrCodesObjectsList.Remove(action.qrCode.Id);
                        }
                    }
                }
            }
            if (clearExisting)
            {
                clearExisting = false;
                foreach (var obj in qrCodesObjectsList)
                {
                    Destroy(obj.Value);
                }
                qrCodesObjectsList.Clear();

            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleEvents();
        }

    }

}