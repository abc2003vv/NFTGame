using Solana.Unity.Soar.Accounts;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _playGamePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _inventoryPanel;
    public GameObject[] tabs;
    public Image[] tabBtns;
    public Sprite inactiveTabBg, activeTabBg;
    public Vector2 inactiveTabBtnSize, activeTabBtnSize;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Ẩn ban đầu bằng scale 0
        if (_settingsPanel != null) _settingsPanel.transform.localScale = Vector3.zero;
    }

    //Play Game Panel
    public void ClosePlayGamePanel()
    {
        if (_playGamePanel != null)
        {
            LeanTween.scale(_playGamePanel, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => _playGamePanel.SetActive(false));
        }
    }

    //UI Setting
    public void OpenSettingsPanel()
    {
        if (_settingsPanel != null)
        {
            _settingsPanel.SetActive(true);
            _settingsPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(_settingsPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
        }
    }

    //Settings Panel
    public void CloseSettingsPanel()
    {
        if (_settingsPanel != null)
        {
            LeanTween.scale(_settingsPanel, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => _settingsPanel.SetActive(false));
        }
    }

    //Shop Panel
    public void OpenShopPanel()
    {
        if (_shopPanel != null)
        {
            _shopPanel.SetActive(true);
            _shopPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(_shopPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
        }
    }

    public void CloseShopPanel()
    {
        if (_shopPanel != null)
        {
            LeanTween.scale(_shopPanel, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => _shopPanel.SetActive(false));
        }
    }

    public void SwitchTabs(int TabID)
    {
        foreach (GameObject go in tabs)
        {
            go.SetActive(false);
        }
        tabs[TabID].SetActive(true);

        foreach (Image img in tabBtns)
        {
            img.sprite = inactiveTabBg;
            img.rectTransform.sizeDelta = inactiveTabBtnSize;
        }
        tabBtns[TabID].sprite = activeTabBg;
        tabBtns[TabID].rectTransform.sizeDelta = activeTabBtnSize;
    }

    //Inventory Panel
    public void OpenInventoryPanel()
    {
        if (_inventoryPanel != null)
        {
            _inventoryPanel.SetActive(true);
            _inventoryPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(_inventoryPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
        }
    }
}
