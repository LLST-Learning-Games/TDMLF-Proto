using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDMLF.Family
{
    public enum Gender
    {
        Woman,
        Man,
        Trans
    }


    public class FamilyMember : MonoBehaviour
    {
        [SerializeField] int age;
        [SerializeField] bool isChild;
        [SerializeField] Gender gender;

        const int MIN_AGE = 9;
        const int MAX_AGE = 65;

        [Header("Work")]
        [SerializeField] bool isAssigned = false;
        [SerializeField] float workTime = 1f;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            age = Random.Range(MIN_AGE, MAX_AGE);
            if (age < 18)
            {
                isChild = true;
                workTime *= 2;
            }
            else
                isChild = false;



            gender = RollForGender();
                        
        }

        private Gender RollForGender()
        {
            Gender newGender;
            int roll = Random.Range(0,99);

            if (roll < 5)
                newGender = Gender.Trans;
            else if (roll >= 5 && roll < 53)
                newGender = Gender.Woman;
            else
                newGender = Gender.Man;
            
            return newGender;
        }

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

        public float GetWorkTime()
        {
            return workTime;
        }
    }
}