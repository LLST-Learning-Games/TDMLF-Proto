using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TDMLF.Family;

public class GameManager : MonoBehaviour
{
    [SerializeField] int month = 1;
    [SerializeField] float monthTime = 5f;
    [SerializeField] float monthTimeLeft;
    [SerializeField] FamilyManager gameFamily;

    [Header("Debug")]
    [SerializeField] bool debugRoundMode = false;
    [SerializeField] Button accountingStepDebugButton;


    // Start is called before the first frame update
    void Start()
    {
        if (gameFamily == null)
            gameFamily = FindObjectOfType<FamilyManager>();

        monthTimeLeft = monthTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(!debugRoundMode)
        {
            monthTimeLeft -= Time.deltaTime;
        }

        if(monthTimeLeft < 0)
        {
            EndMonth();
        }

        if (accountingStepDebugButton != null)
        {
            accountingStepDebugButton.gameObject.SetActive(debugRoundMode);
        }
    }

    public void EndMonth()
    {
        gameFamily.AccountingStep();
        monthTimeLeft = monthTime;
        month++;
    }
}
