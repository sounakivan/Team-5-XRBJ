using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap.Examples
{
    public class AnchorVisualizer : MonoBehaviour
    {
        public GameObject anchorPrefab;
        public MLAnchors.Request query;
        private Transform mainCamera;
        private Transform xrOrigin;
        private Dictionary<string, AnchorVisual> map = new Dictionary<string, AnchorVisual>();

        public GameObject guideEnviron;
        public GameObject travellerEnviron;


        public GameObject cube2;

        void Start()
        {
            mainCamera = Camera.main.transform;
            xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>().transform;
            query = new MLAnchors.Request();
        }

        void Update()
        {
            if (query == null)
                return;

            var mlResultStart = query.Start(new MLAnchors.Request.Params(mainCamera.position, 0, 0, true));
            var mlResultGet = query.TryGetResult(out MLAnchors.Request.Result result);

            if (mlResultStart.IsOk && mlResultGet.IsOk)
            {
                foreach (var anchor in result.anchors)
                {
                    string id = anchor.Id;

                    

                    if (map.ContainsKey(id) == false)
                    {
                        if(id == "ffec5c1f-9361-7018-bdae-be16ad164177"){
                            travellerEnviron.SetActive(true);
                            cube2.SetActive(true);
                            cube2.transform.position = anchor.Pose.position;
                            travellerEnviron.transform.position = anchor.Pose.position;
                            //travellerEnviron.transform.rotation = new Vector3()
                        }else if(id == "45ab9766-587b-7018-85ab-8ef424e85330"){
                            guideEnviron.SetActive(true);
                            guideEnviron.transform.position = anchor.Pose.position;
                        }


                        GameObject anchorGO = Instantiate(anchorPrefab, xrOrigin);
                        map.Add(id, anchorGO.AddComponent<AnchorVisual>());
                    }
                    map[id].Set(anchor);
                }
            }
        }
    }
}


