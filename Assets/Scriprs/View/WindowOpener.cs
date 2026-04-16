using System;
using Scriprs.Service.StaticData;
using Scriprs.Service.Windows;
using Scriprs.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowOpener : MonoBehaviour
{
    private static IWindowService _windowService;
    private static IStaticDataService _staticDataService;
    public Button openButton;
    public Button OpenCustomButton;
    public Sprite DetailImage;

    [Inject]
    private void Construct( IWindowService windowService, IStaticDataService  staticDataService)
    {
        _staticDataService = staticDataService;
        _windowService = windowService;
    }

    private void Awake()
    {
        _staticDataService.LoadAll();
    }

    void Start()
    {
        openButton.onClick.AddListener(OpenWindow);
        OpenCustomButton.onClick.AddListener(OpenCustomWindow);
    }

    private void OpenCustomWindow()
    {
        Debug.Log("Open Window with payload");
        _windowService.Open(
            WindowId.PuzzleDetailsWindow,
            new WindowWithParamsPayload(
                id: "tigerId",
                previewImage: DetailImage,
                reward: 1000,
                partsCount: new int[] {16,25,36}));
    }

    private void OpenWindow()
    {
        Debug.Log("Open Simple Window");
        _windowService.Open(WindowId.GameOverWindow);
    }
}
