using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TDMLF.Family;

namespace TDMLF.Locations
{
    public class HousingSearch : MonoBehaviour
    {
        [SerializeField] FamilyManager familyManager = null;

        [SerializeField] Canvas myCanvas = null;
        [SerializeField] TextMeshProUGUI textBox = null;
        [SerializeField] CountdownTimer myTimer = null;

        // Start is called before the first frame update
        void Start()
        {
            if (familyManager == null)
                FindObjectOfType<FamilyManager>();
        }

        private void OnMouseDown()
        {
            familyManager.UpdateHousing(new House());
        }

        private void OnMouseOver()
        {
            textBox.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            textBox.gameObject.SetActive(false);
        }
    }
}