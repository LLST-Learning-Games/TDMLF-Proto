using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDMLF.Family
{
    public class FamilyMember : MonoBehaviour
    {

        [SerializeField] bool isAssigned = false;
        [SerializeField] float workTime = 5f;

        

        public void AssignMe()
        {
            isAssigned = true;
        }

        public bool CheckIsWorking()
        {
            return isAssigned;
        }

        public void SendMeHome()
        {
            isAssigned = false;
        }

    }
}