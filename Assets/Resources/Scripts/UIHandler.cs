using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    #region Fields

    private SpawnManager _spawnManager = null;

    private Toggle _toggleRandomGravity = null;
    private Toggle _toggleRandomPrimitive = null;
    private Toggle _toggleRandomStartSpeed = null;

    private Toggle _toggleGravityAll = null;
    private Slider _sliderGravity = null;
    private TextMeshProUGUI _textMeshGravityValue = null;

    private Dropdown _dropdownPrimitives = null;

    private Slider _sliderSpawnTime = null;
    private TextMeshProUGUI _textMeshSpawnTime = null;

    private Slider _sliderStartSpeed = null;
    private TextMeshProUGUI _textMeshStartSpeed = null;

    #endregion

    #region Methods
    private void Start()
    {
        CacheElements();
        InitElements();
    }

    private void CacheElements()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        _toggleRandomGravity = GameObject.Find("ToggleGravity").GetComponent<Toggle>();
        _toggleRandomPrimitive = GameObject.Find("TogglePrimitive").GetComponent<Toggle>();
        _toggleRandomStartSpeed = GameObject.Find("ToggleStartSpeed").GetComponent<Toggle>();

        _sliderGravity = GameObject.Find("SliderGravity").GetComponent<Slider>();
        _toggleGravityAll = GameObject.Find("ToggleGravityAll").GetComponent<Toggle>();
        _textMeshGravityValue = GameObject.Find("GravityValueLabel").GetComponent<TextMeshProUGUI>();

        _dropdownPrimitives = GameObject.Find("DropdownPrimitives").GetComponent<Dropdown>();

        _sliderSpawnTime = GameObject.Find("SliderSpawnTime").GetComponent<Slider>();
        _textMeshSpawnTime = GameObject.Find("SpawnTimeLabel").GetComponent<TextMeshProUGUI>();

        _sliderStartSpeed = GameObject.Find("StartSpeedTime").GetComponent<Slider>();
        _textMeshStartSpeed = GameObject.Find("StartSpeedLabel").GetComponent<TextMeshProUGUI>();
    }

    private void InitElements()
    {
        if (_toggleRandomGravity != null)
            _toggleRandomGravity.isOn = false;
        else Debug.LogWarning("Toggle Gravity no found!");

        if (_toggleRandomPrimitive != null)
            _toggleRandomPrimitive.isOn = false;
        else Debug.LogWarning("Toggle Primitive no found!");

        if (_toggleRandomStartSpeed != null)
            _toggleRandomStartSpeed.isOn = false;
        else Debug.LogWarning("Toggle StartSpeed no found!");

        if (_sliderGravity != null)
            _sliderGravity.value = 9.81f;
        else Debug.LogWarning("Slider Gravity no found!");

        if (_toggleGravityAll != null)
            _toggleGravityAll.isOn = false;
        else Debug.LogWarning("Toggle GravityAll Gravity no found!");

        if (_textMeshGravityValue != null)
            _textMeshGravityValue.text = _sliderGravity.value.ToString();
        else Debug.LogWarning("Gravity Value Label no found!");

        if (_dropdownPrimitives != null)
            _dropdownPrimitives.value = 0;
        else Debug.LogWarning("Dropdown Primitives no found!");

        if (_sliderSpawnTime != null)
            _sliderSpawnTime.value = 0.5f;
        else Debug.LogWarning("Slider SpawnTime no found!");
        
        if (_textMeshSpawnTime != null)
            _textMeshSpawnTime.text = _sliderSpawnTime.value.ToString();
        else Debug.LogWarning("SpawnTime Label no found!");

        if (_sliderStartSpeed != null)
            _sliderStartSpeed.value = 0.0f;
        else Debug.LogWarning("Slider StartSpeed no found!");

        if (_textMeshStartSpeed != null)
            _textMeshStartSpeed.text = _sliderStartSpeed.value.ToString();
        else Debug.LogWarning("StartSpeed Label no found!");
    }

    public void UI_ToggleGravity()
    {
        _spawnManager.useRandomGravity = _toggleRandomGravity.isOn;
    }

    public void UI_TogglePrimitive()
    {
        _spawnManager.useRandomPrimitive = _toggleRandomPrimitive.isOn;
    }    

    public void UI_ToggleStartSpeed()
    {
        _spawnManager.useRandomStartSpeed = _toggleRandomStartSpeed.isOn;
    }

    public void UI_SliderGravity()
    {
        if (_toggleGravityAll.isOn)
        {
            foreach (SimplePhysics g in GameObject.FindObjectsOfType<SimplePhysics>())
            {
                g.gravity = _sliderGravity.value;
            }           
        }
        _spawnManager.gravity = _sliderGravity.value;
        _textMeshGravityValue.text = _sliderGravity.value.ToString("N2");
    }

    public void UI_DropdownPrimitivese()
    {
        PrimitiveType primitive;

        switch (_dropdownPrimitives.value)
        {
            case 0:
                primitive = PrimitiveType.Sphere;
                break;
            case 1:
                primitive = PrimitiveType.Capsule;
                break;
            case 2:
                primitive = PrimitiveType.Cylinder;
                break;
            case 3:
                primitive = PrimitiveType.Cube;
                break;
            default:
                primitive = PrimitiveType.Sphere;
                break;
        }

        _spawnManager.buildinPrimitive = primitive;
    }

    public void UI_SliderSpawnTime()
    {
        _textMeshSpawnTime.text = _sliderSpawnTime.value.ToString("N2");
        _spawnManager.repeatTime = _sliderSpawnTime.value;
        _spawnManager.StartSpawnig();
    }

    public void UI_SliderStartSpeed()
    {
        _textMeshStartSpeed.text = _sliderStartSpeed.value.ToString("N2");
        _spawnManager.startSpeed = _sliderStartSpeed.value;
    }

    public void UI_BtnStartSpawn()
    {
        _spawnManager.StartSpawnig();
    }

    public void UI_BtnStopSpawn()
    {
        _spawnManager.StopSpawnig();
    }

    public void UI_BtnClearScene()
    {
        _spawnManager.StopSpawnig();
        foreach (SimplePhysics g in GameObject.FindObjectsOfType<SimplePhysics>())
            Destroy(g.gameObject);
    }

    #endregion
}
