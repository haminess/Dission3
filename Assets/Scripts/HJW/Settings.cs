using System.Collections;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static bool popup;
    public bool inputup;
    public GameObject popupui;
    public GameObject pleaseinput;
    public GameObject inputwindow;
    public TextMeshProUGUI[] keyinputext;

    public KeyCode[] keyCodes =
    {
        KeyCode.A, KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.G, KeyCode.U, KeyCode.Y, KeyCode.Z,
        KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,
        KeyCode.AltGr,KeyCode.Ampersand,KeyCode.Asterisk,KeyCode.At,KeyCode.BackQuote,KeyCode.Backslash,KeyCode.Backspace,KeyCode.Break,KeyCode.CapsLock,KeyCode.Caret,KeyCode.Clear,KeyCode.Colon,KeyCode.Comma,KeyCode.Delete,KeyCode.Dollar,KeyCode.DoubleQuote,KeyCode.DownArrow,KeyCode.End,KeyCode.Equals,KeyCode.Escape,KeyCode.Exclaim,KeyCode.F1,KeyCode.F2,KeyCode.F3,KeyCode.F4,KeyCode.F5,KeyCode.F6,KeyCode.F7,KeyCode.F8,KeyCode.F9,KeyCode.F10,KeyCode.F11,KeyCode.F12,
        KeyCode.Greater,KeyCode.Hash,KeyCode.Help,KeyCode.Home,KeyCode.Insert,KeyCode.Keypad0,KeyCode.Keypad1,KeyCode.Keypad2,KeyCode.Keypad3,KeyCode.Keypad4,KeyCode.Keypad5,KeyCode.Keypad6,KeyCode.Keypad7,KeyCode.Keypad8,KeyCode.Keypad9,KeyCode.KeypadDivide,KeyCode.KeypadEnter,KeyCode.KeypadEquals,KeyCode.KeypadMinus,KeyCode.KeypadMultiply,KeyCode.KeypadPeriod,KeyCode.KeypadPlus,KeyCode.LeftAlt,KeyCode.LeftApple,KeyCode.LeftArrow,KeyCode.LeftBracket,KeyCode.LeftCommand,KeyCode.LeftControl,KeyCode.LeftCurlyBracket,KeyCode.LeftMeta,KeyCode.LeftParen,KeyCode.LeftShift,KeyCode.LeftWindows,KeyCode.Less,
        KeyCode.Menu,KeyCode.Minus,KeyCode.None,KeyCode.Numlock,KeyCode.PageDown,KeyCode.PageUp,KeyCode.Pause,KeyCode.Percent,KeyCode.Period,KeyCode.Pipe,KeyCode.Print,KeyCode.Plus,KeyCode.Question,KeyCode.Quote,KeyCode.Return,KeyCode.RightAlt,KeyCode.RightApple,KeyCode.RightArrow,KeyCode.RightBracket,KeyCode.RightCommand,KeyCode.RightControl,KeyCode.RightMeta,KeyCode.RightCurlyBracket,KeyCode.RightParen,KeyCode.RightShift,KeyCode.RightWindows,KeyCode.ScrollLock,KeyCode.Semicolon,KeyCode.Slash,KeyCode.Space,KeyCode.SysReq,KeyCode.Tab,KeyCode.Tilde,KeyCode.Underscore,KeyCode.UpArrow
    };

    private void Start()
    {
    }
    private void Update()
    {
        if (inputup && Input.GetKeyDown(KeyCode.Escape))
        {
            inputwindow.SetActive(false);
            hideinput();
        }
        else if (popup && Input.GetKeyDown(KeyCode.Escape))
        {
            popupui.SetActive(false);
            close();
        }
    }
    public void popupp() //openui
    {
        popup = true;
        Makemadi.instance.uiset();
    }
    public void popdown() //closeui
    {
        Makemadi.instance.check();
    }

    public void close()
    {
        Settings.popup = false;
    }

    public void editname()
    {
        Makemadi.instance.projectname = Makemadi.instance.name_ui.text;
    }
    public void showinput()
    {
        inputup = true;
        for (int i = 0; i < keyinputext.Length; i++)
        {
            keyinputext[i].text = Maketile.instance.keys[i].ToString();
        }
    }
    public void hideinput()
    {
        inputup = false;
    }
    public void getInput(int w)
    {
        StartCoroutine(ha(w));
    }

    IEnumerator ha(int w)
    {
        bool found = false;
        while (true)
        {
            if (found)
            {
                pleaseinput.SetActive(false);
                break;
            }
            yield return null;
            for(int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    Maketile.instance.keys[w] = keyCodes[i];
                    keyinputext[w].text = keyCodes[i].ToString();
                    print(keyCodes[i]);
                    found = true;
                    break;
                }
            }
        }
    }

    public void opensettingfolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
    public void Saveeditordatadele()
    {
        DataManager.Instance.SaveEditorData();
        listloaddele();
    }
    public void listloaddele()
    {
        DataManager.Instance.listload();
    }
}
