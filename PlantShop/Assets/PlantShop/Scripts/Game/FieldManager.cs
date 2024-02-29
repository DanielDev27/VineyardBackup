using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour
{
    [SerializeField] int fieldCross;
    [SerializeField] public List<GameObject> vineAssets;
    [SerializeField] GameObject vineHolder;
    [SerializeField] List<GameObject> vinePrefabs;
    [Header("UI Prompts")]
    [SerializeField] Camera camera;
    [SerializeField] public Tool requiredTool;
    [SerializeField] public Canvas promptCanvas;
    [SerializeField] public List<Image> promptImages;
    private void Awake()
    {
        GenerateField();
    }
    void Start()
    {
        promptCanvas.GetComponentInChildren<TMP_Text>().transform.LookAt(camera.transform.forward);
        promptCanvas.GetComponentInChildren<Image>().transform.LookAt(camera.transform.forward);
        promptCanvas.GetComponentInChildren<TMP_Text>().enabled = false;
        promptCanvas.GetComponentInChildren<Image>().enabled = false;
    }
    void Update()
    {

    }
    [Button]
    public void GenerateField()
    {
        for (int i = 0; i < fieldCross; i++)
        {
            for (int j = 0; j < fieldCross; j++)
            {
                GameObject vine = Instantiate(vineHolder);
                vine.transform.position = this.transform.position + new Vector3(i - 1.5f, 0, j - 1.5f);
                vine.transform.parent = this.transform;
                vineAssets.Add(vine);
            }
        }
        UpdateField(0);
    }
    [Button]
    public void UpdateField(int vineIndex)
    {
        foreach (GameObject _vine in vineAssets)
        {
            if (_vine.TryGetComponent(out Vine component))
            {
                if (_vine.GetComponent<Vine>().vinePrefab != null)
                {
                    GameObject _vineExisting = _vine.GetComponent<Vine>().vinePrefab.gameObject;
                    DestroyImmediate(_vineExisting.gameObject, true);
                }
                //Create new Model instance
                _vine.GetComponent<Vine>().vinePrefab = Instantiate(vinePrefabs[vineIndex], _vine.transform.position, Quaternion.identity);
                _vine.GetComponent<Vine>().vinePrefab.transform.parent = _vine.transform;
            }
        }
    }
    [Button]
    public void TriggerPromptImage(Tool requiredTool)
    {
        switch (requiredTool)
        {
            case Tool.WateringTool:
                requiredTool = Tool.WateringTool;
                promptCanvas.GetComponentInChildren<TMP_Text>().text = "Watering Tool";
                //promptCanvas.GetComponentInChildren<Image>().sprite = promptImages[0].sprite;
                break;
            case Tool.PruningTool:
                requiredTool = Tool.PruningTool;
                promptCanvas.GetComponentInChildren<TMP_Text>().text = "Pruning Tool";
                //promptCanvas.GetComponentInChildren<Image>().sprite = promptImages[0].sprite;
                break;
            case Tool.PestControlTool:
                requiredTool = Tool.PestControlTool;
                promptCanvas.GetComponentInChildren<TMP_Text>().text = "Pest Control Tool";
                //promptCanvas.GetComponentInChildren<Image>().sprite = promptImages[0].sprite;
                break;
            default:
                break;

        }
        promptCanvas.GetComponentInChildren<TMP_Text>().enabled = true;
        //promptCanvas.GetComponentInChildren<Image>().enabled = true;
    }
    public void ClosePromptImage()
    {
        promptCanvas.GetComponentInChildren<TMP_Text>().enabled = false;
        promptCanvas.GetComponentInChildren<Image>().enabled = false;
    }

}
