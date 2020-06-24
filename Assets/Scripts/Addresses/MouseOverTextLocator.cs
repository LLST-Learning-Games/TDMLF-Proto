using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDMLF.Locations
{
    public class MouseOverTextLocator : MonoBehaviour
    {

        [SerializeField] float xOffset = 1.5f;
        [SerializeField] float yOffset = 1.5f;

        // Start is called before the first frame update
        void Start()
        {
            Transform target = GetComponentInParent<AddressBase>().GetComponent<Transform>();
            RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            float offsetPosX = target.position.x + xOffset;
            float offsetPosY = target.position.y + yOffset;

            Vector2 offsetPos = new Vector2(offsetPosX, offsetPosY);

            //Vector2 canvasPos; 
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);
            transform.position = screenPoint;
        }


    }
}