using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TDMLF.Family;
using UnityEngine.UI;
using System;

namespace TDMLF.Locations
{
    public class ApplyForAid : AddressBase
    {
        [SerializeField] Button[] buttons;
        [SerializeField] bool showButtons = false;


        new void Awake()
        {
            base.Awake();

            buttons = GetComponentsInChildren<Button>();
            UpdateButtons();

        }

        new void OnMouseDown()
        {
            if (!showButtons) base.OnMouseDown();
        }

        protected override IEnumerator AssignWorker()
        {

            // Set the worker to "assigned"
            worker.AssignMe();
            worker.transform.position = transform.position + Vector3.back;

            // Start the timer bar
            myTimer.gameObject.SetActive(true);
            StartCoroutine(myTimer.CountDownAnimation(worker.GetWorkTime()));

            // familyManager.UpdateResource(cost);

            // Wait for working to happen
            yield return new WaitForSeconds(worker.GetWorkTime());

            //yield return new WaitForSeconds(0.1f);

            // Working is done, pay and bring home
            StartAidApplicationProcess();
            //ChooseHouse(new House());

            //familyManager.ReturnFamilyMember(worker);
            //worker = null;
        }

        public override string GetToolTip()
        {
            string tbText = "";
            tbText = "Site: " + locationName + "\n";
            tbText += "Action: " + actionName + "\n";
            return tbText;
        }


        private void StartAidApplicationProcess()
        {
            ToggleButtons();
            
            for (int i = 0; i < buttons.Length; i++)
            {
                PopulateButtonText(buttons[i], i);
            }
        }

        public void OnButtonPress(int i)
        {
            ToggleButtons();
            
            familyManager.UpdateResource(new Resource((ResourceType)i, 5));
            familyManager.ReturnFamilyMember(worker);
            worker = null;
        }

        private void PopulateButtonText(Button button, int i)
        {
            String s = "";
            s = "What do you need? \n";
            s += ((ResourceType)i).ToString();


            button.GetComponentInChildren<Text>().text = s;
        }

        
                              
        private void ToggleButtons()
        {
            showButtons = !showButtons;
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            foreach (Button b in buttons)
            {
                b.gameObject.SetActive(showButtons);
            }
        }
    }
}