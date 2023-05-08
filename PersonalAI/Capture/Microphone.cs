using System;
using System.Collections.ObjectModel;
using Microsoft.CognitiveServices.Speech.Audio;
using Whisper;

namespace PersonalAI.Capture
{
    public class Microphone
    {
        private static ObservableCollection<string> _captureDeviceNames = new ObservableCollection<string>();
        private static CaptureDeviceId[]? _deviceIds;


        public static int Init()
        {
            try
            {
                using iMediaFoundation mf = Library.initMediaFoundation();
                _deviceIds = mf.listCaptureDevices() ??
                             throw new ApplicationException();

                sCaptureParams cp = new sCaptureParams();
                // using iAudioCapture audioCapture = mf.openCaptureDevice();

                return 0;
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }

        public static ObservableCollection<string> GetCaptureDevices()
        {
            if (_deviceIds.Length != 0)
            {
                foreach (var deviceId in _deviceIds)
                {
                    _captureDeviceNames.Add(deviceId.displayName);
                }
            }
            
            return _captureDeviceNames;
        }
    }
}

