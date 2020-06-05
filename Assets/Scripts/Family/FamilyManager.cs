using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDMLF.Family
{
    public class FamilyManager : MonoBehaviour
    {
        [SerializeField] List<FamilyMember> familyMembersAtHome = new List<FamilyMember>();
        [SerializeField] Text resourceDisplayText;
        [SerializeField] int currentFamilyMemberIndex;
        [SerializeField] House familyHome;
        [SerializeField] int qualityOfLife;
        

        private Vector2 homePos = new Vector2(-10, -4);
        private Vector2 homePosOffset = new Vector2(1, 0);

        Dictionary<ResourceType, int> resourceTracker = new Dictionary<ResourceType, int>();

        // Start is called before the first frame update
        void Start()
        {
            currentFamilyMemberIndex = familyMembersAtHome.Count;
            qualityOfLife = UnityEngine.Random.Range(1,10);
            InitResources();
            //UpdateResource(new Resource(ResourceType.Bread, 5));
            ReadResources();
            familyHome = new House();
        }
        private void InitResources()
        {
            foreach (ResourceType rType in Enum.GetValues(typeof(ResourceType)))
            {
                resourceTracker.Add(rType, 0);
            }
        }

        public FamilyMember GetNextFamilyMember()
        {
            if (currentFamilyMemberIndex > 0)
            {
                currentFamilyMemberIndex--;
                FamilyMember familyMemberToReturn = familyMembersAtHome[currentFamilyMemberIndex];
                familyMembersAtHome.Remove(familyMemberToReturn);
                return familyMemberToReturn;
            }

            return null;
        }

        public void ReturnFamilyMember(FamilyMember returnee)
        {
            if (returnee.CheckIsWorking())
            {
                returnee.SendMeHome();
                familyMembersAtHome.Add(returnee);
                returnee.transform.position = homePos + homePosOffset * currentFamilyMemberIndex;
                currentFamilyMemberIndex++;
            }
        }
        
        private void ReadResources()
        {
            /*
            foreach(var resource in resourceTracker)
            {
                Debug.Log("You have " + resource.Value.ToString() + " of " + resource.Key.ToString());
            }
            */
            UpdateResourceReadout();
        }

        public void UpdateResource(Resource update)
        {
            resourceTracker[update.rType] += update.amt;
            ReadResources();
        }

        private void UpdateResourceReadout()
        {
            resourceDisplayText.text = "";
            foreach (ResourceType rType in Enum.GetValues(typeof(ResourceType)))
            {
                resourceDisplayText.text += rType.ToString() + ": " + resourceTracker[rType].ToString() + "\n";
            }
        }


        public void UpdateHousing(House newHouse)
        {
            familyHome = newHouse;
        }



        public void AccountingStep()
        {
            PayRent();
            FeedFamily();
            UpdateQoL();
            UpdateResourceReadout();
        }

        

        private void FeedFamily()
        {
            int hungerModifier = 0;

            if (resourceTracker[ResourceType.Bread] + resourceTracker[ResourceType.HealthyFood] < familyMembersAtHome.Count * 2)
                hungerModifier = -5;
            else if (resourceTracker[ResourceType.HealthyFood] < familyMembersAtHome.Count)
                hungerModifier = -2;
            else if (resourceTracker[ResourceType.HealthyFood] >= familyMembersAtHome.Count * 2)
                hungerModifier = 2;

            Debug.Log("Luxury Hunger Modifier of " + hungerModifier);

            resourceTracker[ResourceType.Bread] = 0;
            resourceTracker[ResourceType.HealthyFood] = 0;

            resourceTracker[ResourceType.Luxury] += hungerModifier;
        }

        private void PayRent()
        {
            resourceTracker[ResourceType.Money] -= familyHome.rent;
            Debug.Log("Paying rent of " + familyHome.rent);

            if(familyHome.rooms < familyMembersAtHome.Count / 2)
            {
                resourceTracker[ResourceType.Luxury] -= 2;
                Debug.Log("Overcrowded!");
            }
            else if (familyHome.rooms > familyMembersAtHome.Count)
            {
                resourceTracker[ResourceType.Luxury] += 2;
                Debug.Log("Spatious flat bonus");
            }

            resourceTracker[ResourceType.Luxury] += familyHome.quality;
            Debug.Log("flat quality modifier: " + familyHome.quality);

        }

        private void UpdateQoL()
        {
            int luxQoLCompRatio = resourceTracker[ResourceType.Luxury] / 2;

            if (luxQoLCompRatio >= qualityOfLife)
                qualityOfLife++;
            else if (resourceTracker[ResourceType.Luxury] < qualityOfLife)
                qualityOfLife--;

            resourceTracker[ResourceType.Luxury] = 0;
        }


    }
}