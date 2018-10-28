using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using WinVolumeChanger.Models;

namespace WinVolumeChanger.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private WinVolumeChangerModel _Model;
        private PropertyChangedEventListener _ModelListener;

        #region VolumeValue変更通知プロパティ
        private string _CurrentValue = "0";

        public string CurrentVolume
        {
            get
            { return _CurrentValue; }
            set
            { 
                if (_CurrentValue == value)
                    return;
                _CurrentValue = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public void Initialize()
        {
            _Model = new WinVolumeChangerModel();

            //Modelデータリスナの初期化
            _ModelListener = new PropertyChangedEventListener(_Model) {
              () => _Model.CurrentVolume, ((sender, e) => CurrentVolumePropertyChanged())
            };

            CompositeDisposable.Add(_ModelListener);

            _Model.GetVolume();
        }

        /// <summary>
        /// スクロール上の処理
        /// </summary>
        public void ScrollUp()
        {
            _Model.UpVolume();
        }

        /// <summary>
        /// スクロール下の処理
        /// </summary>
        public void ScrollDown()
        {
            _Model.DownVolume();
        }


        /// <summary>
        /// 音量ミュート処理
        /// </summary>
        public void VolumeMute()
        {
            _Model.SetVolumeMute();
        }

        /// <summary>
        /// 音量MAX処理
        /// </summary>
        public void VolumeMax()
        {
            _Model.SetVolumeMax();
        }

        /// <summary>
        /// 現在音量の更新
        /// </summary>
        public void CurrentVolumePropertyChanged()
        {
            this.CurrentVolume = _Model.CurrentVolume.ToString();
        }
    }
}
