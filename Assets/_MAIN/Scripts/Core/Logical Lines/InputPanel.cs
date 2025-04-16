using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InputPanel : MonoBehaviour
{
    public static InputPanel instance { get; private set; }
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button accetBotton;
    [SerializeField] private TMP_InputField inputField;
    private CanvasGroupController cg;

    public string lastInput { get; private set; } = string.Empty;
    public bool isWaitingOnUserInput { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cg = new CanvasGroupController(this, canvasGroup);

        canvasGroup.alpha = 0;
        SetCanvasState(active: false);
        accetBotton.gameObject.SetActive(false);

        inputField.onValueChanged.AddListener(OnInputChanged);
        accetBotton.onClick.AddListener(onAcceptInput);
    }

    public void Show(string title)
    {
        titleText.text = title;
        inputField.text = string.Empty;
        cg.Show();
        SetCanvasState(active: true);
        isWaitingOnUserInput = true;
    }
    public void Hide()
    {
        
        cg.Hide();
        SetCanvasState(active: false);
        isWaitingOnUserInput = false;
    }

    public void onAcceptInput()
    {
        if (inputField.text == string.Empty)
            return;
        
        lastInput = inputField.text;
        Hide();
    }

    private void SetCanvasState(bool active)
    {
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }
    public void OnInputChanged(string value)
    {
        accetBotton.gameObject.SetActive(HasvalidText());

    }
    private bool HasvalidText()
    {
        return inputField.text != string.Empty;
    }
}
