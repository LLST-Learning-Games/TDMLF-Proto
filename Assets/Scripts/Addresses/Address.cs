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
        [SerializeField] CountdownTimer myTimer = null;

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
            if (myTimer == null)
                myTimer = GetComponentInChildren<CountdownTimer>();

            PopulateTextBox();
            textBox.gameObject.SetActive(false);
            myTimer.gameObject.SetActive(false);
        }


        private void OnMouseDown()
        {

            if (worker != null) return;

            // Get the next worker from the Family Manager
            worker = familyManager.GetNextFamilyMember();

            if (worker != null)
                StartCoroutine(AssignWorker());
        }

        private IEnumerator AssignWorker()
        {
            
            // Set the worker to "assigned"
            worker.AssignMe();
            worker.transform.position = transform.position + Vector3.back;

            // Start the timer bar
            myTimer.gameObject.SetActive(true);
            StartCoroutine(myTimer.CountDownAnimation(worker.GetWorkTime()));

            familyManager.UpdateResource(cost);

            // Wait for working to happen
            yield return new WaitForSeconds(worker.GetWorkTime());

            // Working is done, pay and bring home
            familyManager.UpdateResource(payout);

            Debug.Log("Paid!");
            familyManager.ReturnFamilyMember(worker);
            worker = null;
        }

        private void OnMouseOver()
        {
            textBox.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            textBox.gameObject.SetActive(false);
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