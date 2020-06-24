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
        [SerializeField] Text housingDisplayText;
        [SerializeField] int currentFamilyMemberIndex;
        [SerializeField] int qualityOfLife;

        House familyHome;

        const int HOMELESSNESS_PENALTY = -10;


        private Vector2 homePos = new Vector2(-10, -4);
        private Vector2 homePosOffset = new Vector2(1, 0);

        Dictionary<ResourceType, int> resourceTracker = new Dictionary<ResourceType, int>();

        // Start is called before the first frame update
        void Start()
        {
            currentFamilyMemberIndex = familyMembersAtHome.Count;
            qualityOfLife = UnityEngine.Random.Range(1,10);
            InitResources();
            UpdateResourceReadout();

            familyHome = null;
            UpdateHousingReadout();
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
        
        

        public void UpdateResource(Resource update)
        {
            resourceTracker[update.rType] += update.amt;
            UpdateResourceReadout();
        }

        private void UpdateResourceReadout()
        {
            string s = "";
            foreach (ResourceType rType in Enum.GetValues(typeof(ResourceType)))
            {
                s += rType.ToString() + ": " + resourceTracker[rType].ToString() + "\n";
            }

            s += "QoL: " + qualityOfLife;

            resourceDisplayText.text = s;
        }


        public void UpdateHousing(House newHouse)
        {
            familyHome = newHouse;
            UpdateHousingReadout();
        }

        private void UpdateHousingReadout()
        {
            string s = "Family Home: \n";

            if (familyHome == null)
                s += "None. Family is homeless!\n";
            else
            {
                s += "Rooms: " + familyHome.rooms + "\n";
                s += "Quality: " + familyHome.quality + "\n";
                s += "Rent: " + familyHome.rent + "\n";
            }

            housingDisplayText.text = s;
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
            if (familyHome != null)
            {
                resourceTracker[ResourceType.Money] -= familyHome.rent;
                Debug.Log("Paying rent of " + familyHome.rent);

                if (familyHome.rooms < familyMembersAtHome.Count / 2)
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
            else
            {
                Debug.Log("Homelessness penalty: " + HOMELESSNESS_PENALTY);
                resourceTracker[ResourceType.Luxury] += HOMELESSNESS_PENALTY;
            }

        }

        private void UpdateQoL()
        {
            int luxQoLCompRatio = resourceTracker[ResourceType.Luxury] / 2;

            if (luxQoLCompRatio >= qualityOfLife)
            {
                Debug.Log("Luxuries is more than double QoL. QoL increases.");
                qualityOfLife++;
            }
            else if (resourceTracker[ResourceType.Luxury] < qualityOfLife && qualityOfLife > 0)
            {
                Debug.Log("Luxuries is less than QoL. QoL decreases.");
                qualityOfLife--;
                
            }

            Debug.Log("Quality of Life is now: " + qualityOfLife);

            resourceTracker[ResourceType.Luxury] = 0;
        }


    }
}