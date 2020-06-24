using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TDMLF.Family;

namespace TDMLF.Locations
{
    public abstract class AddressBase : MonoBehaviour
    {

        [SerializeField] protected int address = 0;
        [SerializeField] protected string locationName = "Cafe";
        [SerializeField] protected string actionName = "Work";

        [SerializeField] protected FamilyManager familyManager = null;
        [SerializeField] protected Canvas myCanvas = null;
        [SerializeField] protected TextMeshProUGUI textBox = null;
        [SerializeField] protected CountdownTimer myTimer = null;

        protected FamilyMember worker = null;

        // Start is called before the first frame update
        protected void Awake()
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

        public void SetAddress(int a)
        {
            address = a;
        }
        
        protected void OnMouseDown()
        {

            if (worker != null) return;

            // Get the next worker from the Family Manager
            worker = familyManager.GetNextFamilyMember();

            if (worker != null)
                StartCoroutine(AssignWorker());
        }

        protected abstract IEnumerator AssignWorker();
        

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
            string tbText = GetToolTip();

            if (tbText == null)
                textBox.text = "NOT SET";

            textBox.text = tbText;
        }

        public abstract string GetToolTip();
    }
}