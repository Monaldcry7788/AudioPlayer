﻿/*using System;
using System.Collections.Generic;
using System.IO;
using AudioPlayer.API;
using Dissonance.Audio.Capture;
using Exiled.API.Features;
using NAudio.Wave;
using UnityEngine;
using UnityEngine.Networking;

namespace AudioPlayer.Core.Components
{
    // Streaming audio as a fake microphone.
    // https://placeholder-software.co.uk/dissonance/docs/Tutorials/Custom-Microphone-Capture.html#step-4-file-streaming
    public class CustomWavMicrophone : MonoBehaviour, IMicrophoneCapture
    {
        public bool IsRecording { get; private set; }
        public TimeSpan Latency { get; private set; }

        public AudioClip clip;

        private readonly List<IMicrophoneSubscriber> _subscribers = new List<IMicrophoneSubscriber>();

        private WaveFormat _format = new WaveFormat(44100, 1);
        private readonly float[] _frame = new float[sizeof(int)];
        private int _frameCount = 16 * 4;
        
        private float _elapsedTime;
        private int _offset;

        public WaveFormat StartCapture(string micName)
        {
            Log.Debug("Starting capture.", AudioPlayer.Singleton.Config.ShowDebugLogs);

            AudioController.Comms._capture._network = AudioController.Comms._net;
            
            Log.Info(_frame.Length);
            
            Log.Info($"{clip.name}, {clip.frequency}, {clip.samples}, {clip.length}, {clip.channels}" );
            
            clip.GetData(_frame, 0);
            
            _frameCount = clip.samples;
            
            IsRecording = true;
            Latency = TimeSpan.Zero;
            return _format;
        }

        public void StopCapture()
        {
            Log.Debug("Stopping capture.", AudioPlayer.Singleton.Config.ShowDebugLogs);

            //if (clip == null)
                return;

            //clip.UnloadAudioData();
            //clip = null;
        }
        
        public void Subscribe(IMicrophoneSubscriber listener) => _subscribers.Add(listener);
        public bool Unsubscribe(IMicrophoneSubscriber listener) => _subscribers.Remove(listener);
        
        public bool UpdateSubscribers()
        { 
            _elapsedTime += Time.unscaledDeltaTime;

            //Log.Info(_subscribers.Count);
            
            while (_elapsedTime > 0.02f)
            {
                _elapsedTime -= 0.02f;
                
                foreach (var subscriber in _subscribers)
                    subscriber.ReceiveMicrophoneData(new ArraySegment<float>(_frame, _offset, _frameCount), _format);
                
                _offset += _frameCount;
            }

            /*if (File.Position != File.Length) 
                return false;

            if (AudioController.LoopMusic)
                File.Position = 0;
            else
                AudioController.Stop();//*

            return false;
        }
    }
}*/