using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.CommonComponents.SetupProgress
{
    public class SetupInfoBlock : MonoBehaviour
    {
        [SerializeField] private SetupInfoItem infoItemPrefab;
        [SerializeField] private Transform container;
        [SerializeField] private ZebraList zebra;
        [SerializeField] private string suffix = "InfoBlock";

        public void Initialize(List<SetupInfoItemProps> initProps, string objectPrefix = "")
        {
            var images = new List<StyledImage>();
            initProps.ForEach(infoItemProps =>
            {
                var infoItem = Instantiate(infoItemPrefab, container);
                infoItem.Initialize(infoItemProps, $"{objectPrefix}{suffix}");
                infoItem.name = $"{objectPrefix}{suffix}_{infoItemProps.ID}";
                images.Add(infoItem.StyledImage);
            });
            zebra.ItemsList = images;
        }
    }
}