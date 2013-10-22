using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Speech.Recognition;
using System.IO;
using System.Threading;
using System.Speech.AudioFormat;
using KinectAdapter.Interfaces;
using KinectAdapter.Models;
using System.Diagnostics;

namespace KinectAdapter.VoiceRecognition
{
    class KinectVoiceGestureDetector : IGestureDetector
    {
        #region members
        private KinectSensor _sensor;
        private KinectAudioSource _kinectSource;
        private SpeechRecognitionEngine _speechEngine;
        private Stream _stream;
        private RecognizerInfo _recognizerInfo;
        private Choices _choices;
        private string[] _registetedGestures;
        #endregion

        public KinectVoiceGestureDetector(KinectSensor sensor)
        {
            _sensor = sensor;
            _choices = new Choices();
            RegisterVoiceGestures();
            //FOR TEST

            
            // END TEST
            _recognizerInfo = SpeechRecognitionEngine.InstalledRecognizers()[0];
            BuildSpeechEngine(_recognizerInfo);
        }

        protected void RegisterVoiceGestures()
        {
            _registetedGestures = new string[]{ "Play", "Stop", "Start", "Pause", "Pause", "Close"};
            foreach (var ges in _registetedGestures)
                _choices.Add(ges);
        }


        void BuildSpeechEngine(RecognizerInfo rec)
        {
            _speechEngine = new SpeechRecognitionEngine(rec.Id);

            var gb = new GrammarBuilder { Culture = rec.Culture };
            gb.Append(_choices);

            var g = new Grammar(gb);

            _speechEngine.LoadGrammar(g);
            //recognized a word or words that may be a component of multiple 
            //complete phrases in a grammar.
            _speechEngine.SpeechHypothesized += new EventHandler
             <SpeechHypothesizedEventArgs>(SpeechEngineSpeechHypothesized);
            //receives input that matches any of its loaded and enabled Grammar 
            //objects.
            _speechEngine.SpeechRecognized += new EventHandler
             <SpeechRecognizedEventArgs>(_speechEngineSpeechRecognized);
            //receives input that does not match any of its loaded and enabled 
            //Grammar objects.
            _speechEngine.SpeechRecognitionRejected += new EventHandler
                                     <SpeechRecognitionRejectedEventArgs>
                                     (_speechEngineSpeechRecognitionRejected);


            //C# threads are MTA by default and calling RecognizeAsync in the same
            //thread will cause an COM exception.
            var t = new Thread(StartAudioStream);
            t.Start();
        }

        void _speechEngineSpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Debug.WriteLine("\rSpeech Rejected: \t{0} \n", e.Result.Text);
        }

        void _speechEngineSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Debug.WriteLine("Speech Recognized:" + e.Result.Text);
            if (GestureDetected != null)
                GestureDetected(this, new GestureArgs(e.Result.Text,-1));
        }

        void SpeechEngineSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Debug.WriteLine(string.Format("{0} - Confidence={1}\n", e.Result.Text, e.Result.Confidence));
        }


        void StartAudioStream()
        {
            
            _kinectSource = _sensor.AudioSource;

            _stream = _kinectSource.Start();

            _speechEngine.SetInputToAudioStream(_stream,
                            new SpeechAudioFormatInfo(
                                EncodingFormat.Pcm, 16000, 16, 1,
                                32000, 2, null));

            _speechEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        #region IGestureDetector Implementation
        public event EventHandler<KinectAdapter.Interfaces.GestureArgs> GestureDetected;

        public bool IsGestureSupported(KinectGesture gesture)
        {
            return gesture.GestureType == GestureType.Voice && _registetedGestures.Contains(gesture.GestureName);
        }
        #endregion


        public GestureType GestureType
        {
            get { return GestureType.Voice; }
        }
    }
}
