using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinVolumeChanger.Models
{

    public class WinVolumeChangerModel : NotificationObject
    {
        #region 定数
        private static int MAX_VOLUME = 100;
        private static int MIN_VOLUME = 0;
        #endregion

        private CoreAudioDevice _AudioDevice;

        #region CurrentVolume変更通知プロパティ
        private int _CurrentVolume = 0;

        public int CurrentVolume
        {
            get
            { return _CurrentVolume; }
            set
            { 
                if (_CurrentVolume == value)
                    return;
                _CurrentVolume = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public WinVolumeChangerModel()
        {
            //コンストラクタ
            try
            {
                _AudioDevice = new CoreAudioController().DefaultPlaybackDevice;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("DefaultPlaybackDevice Get Error:" + ex);
            }
        }

        /// <summary>
        /// 音量を上げる
        /// </summary>
        async public void UpVolume()
        {
            this.GetVolume();
            int volumeValue = CurrentVolume == MAX_VOLUME ? CurrentVolume : ++CurrentVolume;
            await _AudioDevice.SetVolumeAsync(volumeValue);

            Debug.WriteLine("Set Volume:" + volumeValue);
            this.GetVolume();

        }

        /// <summary>
        /// 音量を下げる
        /// </summary>
        async public void DownVolume()
        {
            this.GetVolume();
            int volumeValue = CurrentVolume == MIN_VOLUME ? CurrentVolume : --CurrentVolume;
            await _AudioDevice.SetVolumeAsync(volumeValue);

            Debug.WriteLine("Set Volume:" + volumeValue);
            this.GetVolume();

        }

        /// <summary>
        /// 音量を最大にする
        /// </summary>
        async public void SetVolumeMax()
        {
            await _AudioDevice.SetVolumeAsync(MAX_VOLUME);

            Debug.WriteLine("Set Volume:" + MAX_VOLUME);
            this.GetVolume();

        }

        /// <summary>
        /// 音量を最小にする
        /// </summary>
        async public void SetVolumeMute()
        {
            await _AudioDevice.SetVolumeAsync(MIN_VOLUME);

            Debug.WriteLine("Set Volume:" + MIN_VOLUME);
            this.GetVolume();

        }

        /// <summary>
        /// 現在音量の取得
        /// </summary>
        async public void GetVolume()
        {
            CurrentVolume = (int)await _AudioDevice.GetVolumeAsync();
        }
    }
}
