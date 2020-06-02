using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TDMLF.Family;

namespace TDMLF.Locations
{
    public class Address : MonoBehaviour
    {

        [SerializeField] int address = 0;
        [SerializeField] string locationName = "Cafe";
        [SerializeField] string actionName = "Work";
        [SerializeField] Resource payout;
        [SerializeField] Resource cost;

        [SerializeField] FamilyManager familyManager = null;
        [SerializeField] Canvas myCanvas = null;
        [SerializeField] TextMeshProUGUI textBox = null;

        FamilyMember worker = null;

        // Start is called before the first frame update
        void Start()
        {
            if (familyManager == null)
                familyManager = FindObjectOfType<FamilyManager>();
            if (myCanvas == null)
                myCanvas = GetComponentInChildren<Canvas>();
            if (textBox == null)
                textBox = GetComponentInChildren<TextMeshProUGUI>();

            PopulateTextBox();
            myCanvas.gameObject.SetActive(false);
        }


        private void OnMouseDown()
        {
            if (worker != null)
            {
                familyManager.ReturnFamilyMember(worker);
                worker = null;
                return;
            }

            worker = familyManager.GetNextFamilyMember();
            worker.AssignMe();
            worker.transform.position = transform.position + Vector3.back;
            ProcessPayout();
            Debug.Log("Paid!");
        }

        private void OnMouseOver()
        {
            myCanvas.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            myCanvas.gameObject.SetActive(false);
        }

        private void ProcessPayout()
        {
            //Process the cost:
            familyManager.UpdateResource(cost);

            //Process the payout:
            familyManager.UpdateResource(payout);
        }

        private void PopulateTextBox()
        {
            textBox.text = "Site: " + locationName + "\n";
            textBox.text += "Action: " + actionName + "\n";
            textBox.text += "Cost: " + Mathf.Abs(cost.amt) + " " + cost.rType.ToString() + "\n";
            textBox.text += "Payout: " + Mathf.Abs(payout.amt) + " " + payout.rType.ToString() + "\n";
        }
    }
}