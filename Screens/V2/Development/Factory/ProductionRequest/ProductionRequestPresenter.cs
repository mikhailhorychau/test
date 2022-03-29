using System.Linq;
using TMPro;
using UIScripts.Observable;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.Factory.ProductionRequest
{
    public class ProductionRequestPresenter : MonoBehaviour
    {
        [SerializeField] private UIObjectsContainer<RequestItemData> container;
        [SerializeField] private TextMeshProUGUI productionRequests;

        private ProductionRequestData _data;
        
        public void Initialize(ProductionRequestData data)
        {
            _data = data;
            data.Requests.Dictionary.ToList().ForEach(pair => container.InitializeItem(pair.Value));

            data.Requests.OnAdded += RequestAddedListener;

            productionRequests.text = data.ProductionRequests;
        }

        private void OnDestroy()
        {
            if (_data == null) return;

            _data.Requests.OnAdded -= RequestAddedListener;
        }

        private void RequestAddedListener(int id, RequestItemData data) => container.InitializeItem(data);
    }

    public class ProductionRequestData
    {
        public ObservableDictionary<int, RequestItemData> Requests { get; } =
            new ObservableDictionary<int, RequestItemData>();
        
        public string ProductionRequests { get; set; }
    }
}