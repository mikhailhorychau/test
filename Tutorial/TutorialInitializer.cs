using System.Collections;
using System.Collections.Generic;
using Helpers;
using Helpers.Tutorial;
using UIScripts.Tutorial;
using UnityEngine;

public class TutorialInitializer : MonoBehaviour
{
    [SerializeField] TutorialWindow tutorial;
    void Awake()
    {
        TutorialHelper.TutorialWnd = tutorial;
    }

}
