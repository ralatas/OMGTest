using System.Collections.Generic;
using Scriprs.Service.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scriprs.View
{
    public class PuzzleDetailsWindow: BaseWindow
    {
        [Header("Puzzle Data")]
        [SerializeField] public TextMeshProUGUI rewardText;
        [SerializeField] public Image previewImage;
        [SerializeField] public Image gridImage;
        [SerializeField] public List<Sprite> GridSprites;

        [Header("Puzzle Buttons")]
        [SerializeField] public Transform buttonContainer;
        [SerializeField] public Button buttonCountPartPrefub;
        [SerializeField] public Button buttonClose;
        [SerializeField] public Button buttonPlay;
        [SerializeField] public Button buttonPlayAD;
        [SerializeField] public Button buttonPlayCoins;

        private static IWindowService _windowService;
        private  List<Button> gridButtons = new();
        private int countPartsIndex;
        private int[] countParts;
        private string payloadId;
        
        [Inject]
        private void Construct(IWindowService windowService)
        {
            Id = WindowId.GameOverWindow;
            _windowService = windowService;
        }
        protected override void Initialize()
        {
            SetGrid(0);
            Id = WindowId.PuzzleDetailsWindow;
            if (Payload is WindowWithParamsPayload payload) {
                rewardText.text = "Reward " + payload.Reward;
                previewImage.sprite = payload.PreviewImage;
                payloadId =  payload.Id;
                countParts = payload.PartsCount;
                
                for (int i = 0; i < payload.PartsCount.Length; i++)
                {
                   var button = Instantiate(buttonCountPartPrefub, buttonContainer);
                   button.GetComponentInChildren<TextMeshProUGUI>().text = payload.PartsCount[i].ToString();
                   gridButtons.Add(button);
                   var index = i;
                   button.onClick.AddListener(() => SetGrid(index));
                }
            }
            else
            {
                Debug.LogWarning("WindowWithParams payload was not a WindowWithParamsPayload");
            }
        }
        
        protected override void SubscribeUpdates()
        {
            buttonPlay.onClick.AddListener(() => LoadPuzzleDetails(payloadId));
            buttonPlayAD.onClick.AddListener(() => OpenWithShowAd(payloadId));
            buttonPlayCoins.onClick.AddListener(() => OpenWithCoins(payloadId));
            buttonClose.onClick.AddListener(() => _windowService.Close(WindowId.PuzzleDetailsWindow));
        }

        protected override void UnsubscribeUpdates()
        {
            buttonPlay.onClick.RemoveAllListeners();
            buttonPlayAD.onClick.RemoveAllListeners();
            buttonPlayCoins.onClick.RemoveAllListeners();
            buttonClose.onClick.RemoveAllListeners();
            gridButtons.ForEach(button =>
            {
                button.onClick.RemoveAllListeners();
                Destroy(button.gameObject);
            });
            gridButtons.Clear();
        }

        private void SetGrid(int index)
        {
            countPartsIndex = index;
            gridImage.sprite = GridSprites[index];
        }

        private void LoadPuzzleDetails(string puzzleId)
        {
            Debug.Log($"LoadPuzzleDetails for Id: {puzzleId} and CountParts: {countParts[countPartsIndex]}");
        }

        private void OpenWithShowAd(string puzzleId)
        {
            Debug.Log("Show Ad");
            LoadPuzzleDetails(puzzleId);
        }
        private void OpenWithCoins(string puzzleId)
        {
            Debug.Log("Spend Coins");
            LoadPuzzleDetails(puzzleId);
        }
    }
}