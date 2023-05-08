using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace PersonalAI.Handlers;

public class SpeechHandler
{
    
    private static MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

    void OutpuSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
    {
        switch (speechRecognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                mainWindow.LogBox.AppendText(DebugHandler.DebugMessageAi($"{speechRecognitionResult.Text}"));
                break;

            case ResultReason.NoMatch:
                mainWindow.LogBox.AppendText(DebugHandler.DebugMessageSystem("NOMATCH: Speech could not be recognized."));
                break;

            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                mainWindow.LogBox.AppendText(DebugHandler.DebugMessageAi($"CANCELED: Reason={cancellation.Reason}"));

                if (cancellation.Reason == CancellationReason.Error)
                {
                    mainWindow.LogBox.AppendText(DebugHandler.DebugMessageAi($"CANCELED: ErrorCode={cancellation.ErrorCode}"));
                    mainWindow.LogBox.AppendText(DebugHandler.DebugMessageAi($"CANCELED: ErrorDetails={cancellation.ErrorDetails}"));

                }
                break;

        }
    }

    public async Task Listen()
    {
        
        var speechConfig = SpeechConfig.FromSubscription(MainWindow.GetSpeechKey(), MainWindow.GetSpeechRegion());
        speechConfig.SpeechRecognitionLanguage = "en-US";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        while (mainWindow.Listening)
        {
            mainWindow.LogBox.AppendText(DebugHandler.DebugMessageSystem("Listening..."));
            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            OutpuSpeechRecognitionResult(speechRecognitionResult);
        }
    }
    
}