
using System.IO;
using UnityEngine;
using System.Windows.Forms;
using Ookii.Dialogs;

public class FileOpenDialog : MonoBehaviour
{
    VistaOpenFileDialog OpenDialog;
    //  StreamingController openStream = null;

    public AudioSource music;
    public string filepath;

    private void Awake()
    {
        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "mp3 files(*.mp3)|*.mp3|wav files (*.wav)|*.wav|ogg files (*.ogg)|*.ogg;";
        OpenDialog.FilterIndex = 3;
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
        }
    }

    private void CopyFileToAssets(string filepath)
    {
        string FileName = Path.GetFileName(filepath);
        string FileNameWithoutExtention = Path.GetFileNameWithoutExtension(filepath);
        string des = Path.Combine("Assets/Resources/Sound", FileName);
        File.Copy(filepath, des, true);
        UnityEditor.AssetDatabase.Refresh();
        print("Sound/" + FileNameWithoutExtention);
        music.clip = Resources.Load("Sound/" + FileNameWithoutExtention) as AudioClip;
        Makemadi.instance.musicnamee = FileNameWithoutExtention;
        Makemadi.instance.musicname.text = Makemadi.instance.audio_.mainmusic.clip.ToString();
        Makemadi.instance.sec = Mathf.Round( music.clip.length);
        Makemadi.instance.uiset();
        Makemadi.instance.check();
    }
}
