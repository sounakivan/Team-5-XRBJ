using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeInteractions : MonoBehaviour
{
    [SerializeField]
    Transform _hitPointObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(MagicLeapInputManager.Instance.EyeData.leftEyePosition, MagicLeapInputManager.Instance.LeftEyeForwardGaze);

        RaycastHit raycastHit;

        Vector3 endPosition = MagicLeapInputManager.Instance.EyeData.leftEyePosition + (10f * MagicLeapInputManager.Instance.LeftEyeForwardGaze);

        if(Physics.Raycast(ray, out raycastHit, 10f))
        {
            endPosition = raycastHit.point;
            _hitPointObject.position = endPosition;

            if (raycastHit.transform.gameObject.name == "")
            {

            }
        }

    }
}
