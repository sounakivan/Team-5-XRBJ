using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeInteractions : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    Transform _hitPointObject;
    [SerializeField]
    Material _missMaterial;
    [SerializeField]
    Material _hitMaterial;

    // Update is called once per frame
    void Update()
    {
        Vector3 eyeDirection = MagicLeapInputManager.Instance.EyeData.fixationPoint - Camera.main.transform.position;

        Ray ray = new Ray(MagicLeapInputManager.Instance.EyeData.leftEyePosition, eyeDirection);

        RaycastHit raycastHit;

        Vector3 endPosition;

        if (Physics.Raycast(ray, out raycastHit))
        {
            endPosition = raycastHit.point;
            if(_hitPointObject != null)
            {
                _hitPointObject.position = endPosition;
                _hitPointObject.GetComponent<Renderer>().material = _hitMaterial;
            }
            
            Debug.Log(raycastHit.collider.name);

            if (raycastHit.transform.gameObject.name == "")
            {

            }
        }
        else
        {
            if (_hitPointObject != null)
            {
                _hitPointObject.GetComponent<Renderer>().material = _missMaterial;
            }
        }

        

    }
}
