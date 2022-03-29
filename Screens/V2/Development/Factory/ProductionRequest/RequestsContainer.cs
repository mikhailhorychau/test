using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.Factory.ProductionRequest
{
    public class RequestsContainer : UIObjectsContainer<RequestItemData>
    {
        protected override void OnInitializeItem(RequestItemData data, UIObjectView<RequestItemData> view)
        {
            var color = view.transform.GetSiblingIndex() % 2 == 0 ? UIColorStyle.Background1 : UIColorStyle.Background2;
            view.GetComponent<StyledImage>().SwapColor(color);
        }
    }
}