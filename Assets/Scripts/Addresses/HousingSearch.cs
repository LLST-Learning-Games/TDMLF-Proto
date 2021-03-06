﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TDMLF.Family;
using UnityEngine.UI;
using System;

namespace TDMLF.Locations
{
    public class HousingSearch : AddressBase
    {
        [SerializeField] Button[] buttons;
        [SerializeField] bool showButtons = false;

        House[] housingOptions;

        new void Awake()
        {
            base.Awake();

            buttons = GetComponentsInChildren<Button>();
            UpdateButtons();

            housingOptions = new House[buttons.Length];
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
            DisplayHousingOptions();
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


        private void DisplayHousingOptions()
        {
            ToggleButtons();
            
            for (int i = 0; i < buttons.Length; i++)
            {
                // This is a slightly hacky trick: send the index along to set a new seed for the House() constructor.
                // This should be replaced by a central random number manager at some point.
                housingOptions[i] = new House(i + (int) Time.time*1000);
                PopulateButtonText(buttons[i], housingOptions[i], i);
            }
        }

        public void OnButtonPress(int i)
        {
            ChooseHouse(housingOptions[i]);
            ToggleButtons();

            familyManager.ReturnFamilyMember(worker);
            worker = null;
        }

        private void PopulateButtonText(Button button, House house, int i)
        {
            String s = "";
            s = "Option #" + (i + 1).ToString();
            s += "Rooms: " + house.rooms + "\n";
            s += "Quality: " + house.quality + "\n";
            s += "Rent: " + house.rent + "\n";

            //Debug.Log("Button String: " + s);

            button.GetComponentInChildren<Text>().text = s;
        }

        private void ChooseHouse(House newHouse)
        {
            familyManager.UpdateHousing(newHouse);
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