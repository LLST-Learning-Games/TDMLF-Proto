using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDMLF.Locations
{
    public class ResourceTransaction : AddressBase
    {
        [SerializeField] Resource payout;
        [SerializeField] Resource cost;

        

        protected override IEnumerator AssignWorker()
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

        

        public override string GetToolTip()
        {
            string tbText = "";
            tbText = "Site: " + locationName + "\n";
            tbText += "Action: " + actionName + "\n";
            tbText += "Cost: " + Mathf.Abs(cost.amt) + " " + cost.rType.ToString() + "\n";
            tbText += "Payout: " + Mathf.Abs(payout.amt) + " " + payout.rType.ToString() + "\n";

            return tbText;
        }
    }
}
