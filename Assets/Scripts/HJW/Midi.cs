using System.IO;
using UnityEngine;
using System.Windows.Forms;
using Ookii.Dialogs;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Tools;
[System.Serializable]
public struct NoteForUnity
{
    public double timeStamps;
    public double length;
}
public class Midi : MonoBehaviour
{
    VistaOpenFileDialog OpenDialog;
    public AudioSource music;
    public string filepath;
    public MidiFile midi;
    public int trackrestriction;
    public List<NoteForUnity> NoteForUnity = new List<NoteForUnity>();
    public List<MidiFile> Channels = new List<MidiFile>();
    private void Awake()
    {
        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "midi files(*.mid)|*.mid";
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
    private void CopyFileToAssets(string filepath)
    {
        string FileName = Path.GetFileName(filepath);
        string FileNameWithoutExtention = Path.GetFileNameWithoutExtension(filepath);
        string des = Path.Combine("Assets/Resources/Midi", FileName);
        File.Copy(filepath, des, true);
        UnityEditor.AssetDatabase.Refresh();
    }

    private void ReadMidiData()
    {
        ResetMididata();
        midi = MidiFile.Read(filepath);
        foreach (var Splitedmidifiles in midi.SplitByChannel())
        {
            Channels.Add(Splitedmidifiles);
        }
        print(Channels.Count);
        foreach (var note in Channels[trackrestriction].GetNotes())
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midi.GetTempoMap());
            var metricLengthSpan = LengthConverter.ConvertTo<MetricTimeSpan>(note.Length,note.Time ,midi.GetTempoMap());
            var newnote = new NoteForUnity();
            newnote.timeStamps = metricTimeSpan.TotalSeconds;
            newnote.length = metricLengthSpan.TotalSeconds;
            NoteForUnity.Add(newnote);
        }
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

    private void ResetMididata()
    {
        Channels.Clear();
        NoteForUnity.Clear();
    }
}
