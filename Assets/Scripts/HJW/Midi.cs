using System.IO;
using UnityEngine;
using System.Windows.Forms;
using Ookii.Dialogs;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Tools;
using UnityEngine.UI;
using SimpleSynth.EventArguments;
using SimpleSynth.Parameters;
using SimpleSynth.Parsing;
using SimpleSynth.Providers;
using SimpleSynth.Synths;
using System;
[System.Serializable]
public struct NoteForUnity
{
    public double timeStamps;
    public double length;
}
public class Midi : MonoBehaviour
{
    VistaOpenFileDialog OpenDialog;
    public GameObject MidiNote;
    public GameObject MidiMadi;
    public AudioSource music;
    public string filepath;
    public MidiFile midi;
    public int trackrestriction;
    public double TotalLength;
    public List<NoteForUnity> NoteForUnity = new List<NoteForUnity>();
    public List<MidiFile> Channels = new List<MidiFile>();
    private void Awake()
    {
        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "midi files(*.mid)|*.mid|midi files(*.midi)|*.midi";
        OpenDialog.Title = "Select Audio";
    }
    public string FileOpen()
    {
        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            return OpenDialog.FileName;
        }
        return null;
    }

    public void OnFileOpen()
    {
        filepath = FileOpen();

        if (!string.IsNullOrEmpty(filepath))
        {
            CopyFileToAssets(filepath);
            ReadMidiData();
        }
    }
    string FileNameWithoutExtention;
    private void CopyFileToAssets(string filepath)
    {

        string FileName = Path.GetFileName(filepath);
        FileNameWithoutExtention = Path.GetFileNameWithoutExtension(filepath);
        Makemadi.instance.musicnamee = FileNameWithoutExtention;
        string des = Path.Combine("Assets/Resources/Midi", FileName);
        File.Copy(filepath, des, true);
        var stream = File.OpenRead(filepath);
        // Parse the provided MIDI file.
        var interpretation = new MidiInterpretation(stream, new DefaultNoteSegmentProvider());

        // Create a new synthesizer with default providers.
        var synth = new BasicSynth(interpretation, new DefaultAdsrEnvelopeProvider(AdsrParameters.Short), new DefaultBalanceProvider());

        // Generate the WAV file
        MemoryStream result = synth.GenerateWAV();

        string wavpath = Path.Combine("Assets/Resources/Wav", FileNameWithoutExtention + ".wav");
        // Write WAV file to disk
        var outputStream = File.OpenWrite(wavpath);
        result.CopyTo(outputStream);

        result.Dispose();
        UnityEditor.AssetDatabase.Refresh();
        Invoke("ToAudioSource", 0.1f);
    }
    void ToAudioSource()
    {
        print("Wav/" + FileNameWithoutExtention);
        Makemadi.instance.audio_.mainmusic.clip = Resources.Load("Wav/" + FileNameWithoutExtention) as AudioClip;

    }

    private void ReadMidiData()
    {
        ResetMididata();
        midi = MidiFile.Read(filepath);
        TotalLength = TimeConverter.ConvertTo < MetricTimeSpan >(midi.GetDuration(TimeSpanType.Metric), midi.GetTempoMap()).TotalSeconds;
        foreach (var Splitedmidifiles in midi.SplitByChannel())
        {
            Channels.Add(Splitedmidifiles);
        }
        foreach (var note in Channels[trackrestriction].GetNotes())
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midi.GetTempoMap());
            var metricLengthSpan = LengthConverter.ConvertTo<MetricTimeSpan>(note.Length,note.Time ,midi.GetTempoMap());
            var newnote = new NoteForUnity();

            newnote.timeStamps = metricTimeSpan.TotalSeconds;
            newnote.length = metricLengthSpan.TotalSeconds;
            NoteForUnity.Add(newnote);
        }
        MidinoteLoad();
    }
    private void ChangeChannel()
    {
        NoteForUnity.Clear();
        foreach (var note in Channels[trackrestriction].GetNotes())
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midi.GetTempoMap());
            var metricLengthSpan = LengthConverter.ConvertTo<MetricTimeSpan>(note.Length, note.Time, midi.GetTempoMap());
            var newnote = new NoteForUnity();
            newnote.timeStamps = metricTimeSpan.TotalSeconds;
            newnote.length = metricLengthSpan.TotalSeconds;
            NoteForUnity.Add(newnote);
        }
    }
    void MidinoteLoad()
    {
        Makemadi.instance.sec = (float)TotalLength;
        Makemadi.instance.bpm = (int)midi.GetTempoMap().GetTempoAtTime((MidiTimeSpan)0).BeatsPerMinute;
        Makemadi.instance.madiset();
        foreach (var item in NoteForUnity)
        {
            var newpos = new Vector2((float)item.timeStamps * Makemadi.instance.madimultiplyer, 0);
            var n = Instantiate(MidiNote, newpos, Quaternion.identity, MidiMadi.transform);
            if(item.length > 0)
            {
                var mid = n.transform.GetChild(1);
                var end = n.transform.GetChild(2);
                mid.gameObject.SetActive(true);
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2 ((float)item.length * Makemadi.instance.madimultiplyer, 103.87f);
                end.gameObject.GetComponent<Image>().enabled = true;
                end.GetComponent<RectTransform>().localPosition = new Vector2((float)item.length * Makemadi.instance.madimultiplyer, 0);
                end.SetAsLastSibling();
            }
            n.transform.localPosition = newpos;
        }
    }

    private void ResetMididata()
    {
        Channels.Clear();
        NoteForUnity.Clear();
        for (int i = 0; i < MidiMadi.transform.childCount; i++)
        {
            Destroy(MidiMadi.transform.GetChild(i).gameObject);
        }
    }
}
