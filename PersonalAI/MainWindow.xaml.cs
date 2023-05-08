using System;
using System.Windows;
using System.Windows.Controls;
using PersonalAI.Capture;
using PersonalAI.Handlers;

namespace PersonalAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //OpenAI
        private static string openai_OrgID;
        private static string openai_APIKey;
            
        //Azure
        private static string SPEECH_SERVICE_KEY;
        private static string SPEECH_SERVICE_REGION;
        
        public bool Listening = true;
        private SpeechHandler _speech;
        
        
        public MainWindow()
        {
            InitializeComponent();
            Microphone.Init();
            captureDevices.ItemsSource = Microphone.GetCaptureDevices();
            // Memory.LoadMemoryFromFile();
            LogBox.MaxLines = 8;
            Start_Listening.IsEnabled = false;
            LogBox.AppendText(DebugHandler.DebugMessageSystem("Starting..."));
            LogBox.AppendText(DebugHandler.DebugMessageSystem(Init()));
            _speech = new SpeechHandler();
            StartListeningAsync();
        }

        private void StartListeningAsync()
        {
            _speech.Listen();
        }

        private void StopListening()
        {
            _speech.Listen().Dispose();
        }
        
        public static string Init()
        {
            SPEECH_SERVICE_KEY = Environment.GetEnvironmentVariable("SPEECH_SERVICE_KEY", EnvironmentVariableTarget.User);
            if (SPEECH_SERVICE_KEY == null)
                return "SPEECH_SERVICE_KEY is not found. Please add one in the USER PATH.";
            SPEECH_SERVICE_REGION = Environment.GetEnvironmentVariable("SPEECH_SERVICE_REGION", EnvironmentVariableTarget.User);
            if (SPEECH_SERVICE_REGION == null)
                return "SPEECH_SERVICE_REGION is not found. ";
            openai_OrgID = Environment.GetEnvironmentVariable("openai_OrgID", EnvironmentVariableTarget.User);
            if (openai_OrgID == null)
                return "openai_OrgID is not found. Please add one in the USER PATH.";
            openai_APIKey = Environment.GetEnvironmentVariable("openai_APIKey", EnvironmentVariableTarget.User);
            if (openai_APIKey == null)
                return "openai_APIKey is not found. Please add one in the USER PATH.";

            return "Init Success!";
        }
        
        public static string GetSpeechKey()
        {
            return SPEECH_SERVICE_KEY;
        }
        public static string GetSpeechRegion()
        {
            return SPEECH_SERVICE_REGION;
        }
        public static string GetOpenAiKey()
        {
            return openai_APIKey;
        }
        public static string GetOpenAiOrgID()
        {
            return openai_OrgID;
        }

        private void Start_Listening_Click(object sender, RoutedEventArgs e)
        {
            Listening = !Listening;
            Start_Listening.IsEnabled = !Start_Listening.IsEnabled;
            Stop_Listening.IsEnabled = !Stop_Listening.IsEnabled;
            StartListeningAsync();
        }

        private void Stop_Listening_Click(object sender, RoutedEventArgs e)
        {

            Listening = !Listening;
            Start_Listening.IsEnabled = !Start_Listening.IsEnabled;
            Stop_Listening.IsEnabled = !Stop_Listening.IsEnabled;
            StopListening();
            LogBox.AppendText(DebugHandler.DebugMessageSystem("Stopped Listening."));
        }

        private void LogBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}