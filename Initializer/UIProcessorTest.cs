using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace UIScripts.Initializer
{
    public class UIProcessorTest : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToInstantiate;
        [SerializeField] private GameObject loadScreen;
        [SerializeField] private TextMeshProUGUI value;
        [SerializeField] private TextMeshProUGUI jobName;

        private void Start()
        {
            var processor = UIProcessor.Instance;
            var showScreenJob = new UIJob(ShowScreen);
            processor.AddJob(showScreenJob);

            List<UIJob> jobs = new List<UIJob>();
            
            processor.AddJob(new UIJob(() => value.text = "Loading Resources..."));
            jobs.Add(new UIJobWaitForSeconds(3));
            
            for (var i = 0; i < objectsToInstantiate.Length; i++)
            {
                var item = objectsToInstantiate[i];

                Action InstantiateAction = () =>
                {
                    var obj = Instantiate(item);
                    // obj.SetActive(false);
                    print($"{obj.name} {Time.time}");
                    jobName.text = $"Loading {obj.name} resources";
                };
                
                var job = new UIJob(InstantiateAction);
                jobs.Add(job);
            }

            var seq = new UIJobSequence(jobs);
            seq.OnProgressChanged += ObsChangeListener;
            
            
            processor.AddJob(seq);
            var hideScreenJob = new UIJob(() => loadScreen.gameObject.SetActive(false));
            processor.AddJob(new UIJob(() => value.text = "Initialize screens"));
            processor.AddJob(new UIJobWaitForSeconds(3));
            processor.AddJob(hideScreenJob);
        }

        private void ShowScreen() => loadScreen.gameObject.SetActive(true);

        private void ObsChangeListener(float v) => value.text = $"{(int)(v * 100)}/100%";
    }
}