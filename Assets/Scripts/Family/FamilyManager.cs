using System;
using System.Collections;
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

        private Vector2 homePos = new Vector2(-10, -4);
        private Vector2 homePosOffset = new Vector2(1, 0);

        Dictionary<ResourceType, int> resourceTracker = new Dictionary<ResourceType, int>();

        // Start is called before the first frame update
        void Start()
        {
            currentFamilyMemberIndex = familyMembersAtHome.Count;
            InitResources();
            //UpdateResource(new Resource(ResourceType.Bread, 5));
            ReadResources();
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

        private void InitResources()
        {
            foreach (ResourceType rType in Enum.GetValues(typeof(ResourceType)))
            {
                resourceTracker.Add(rType, 0);
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}