using System.Collections.Generic;
using UIScripts;
using UnityEngine;

public class ErrorFieldsInfo : MonoBehaviour
{
    [SerializeField] private List<StyledTextInput> inputs;
    [SerializeField] private StyledButton button;

    private void Awake()
    {
        
    }

    public void HideError() => gameObject.SetActive(false);

    public void CheckFields()
    {
        var error = false;
        inputs.ForEach(input =>
        {
            input.Validate();
            if (input.IsError) error = true;
        });
        
        gameObject.SetActive(error);
        // button.Disabled = error;
    }
}
