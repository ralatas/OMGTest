using Scriprs.Service.Windows;
using UnityEngine;

namespace Scriprs.View
{
    public class WindowWithParamsPayload : IWindowPayload
    {
        public string Id { get; set; }
        public int Reward { get; set; }
        public int [] PartsCount { get; set; }
        public Sprite PreviewImage { get; set; }

        public WindowWithParamsPayload(string id, Sprite previewImage, int reward, int [] partsCount)
        {
            Id = id;
            PreviewImage = previewImage;
            Reward = reward;
            PartsCount = partsCount;
        }
    }
}