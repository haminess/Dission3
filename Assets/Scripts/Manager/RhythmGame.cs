using System.Collections;
using UnityEngine;

// ������� �÷��� ����, ���� ���� �����ϴ� ��ũ��Ʈ
public class RhythmGame : MonoBehaviour
{
    // �÷��� ������
    // 1. ��� ����
    // 2. ä�� ������
    // 3. �÷��̾�/���� ����
    // 4. �÷��� ����


    // ���� �� ä�� ������
    // 1. ä�� ���̵�
    // 3. ��Ʈ ������
    private static RhythmGame instance;
    public static RhythmGame Instance
    {
        get
        {
            if (instance == null)
            {
                // 1. ���� ������ ã�ƺ���
                instance = FindObjectOfType<RhythmGame>();

                // 2. ������ ���ٸ� ����
                if (instance == null)
                {
                    GameObject container = new GameObject("RhythmGame");
                    instance = container.AddComponent<RhythmGame>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private Coroutine play;

    [Header("Data Input Field")]
    public string music_name;

    [Header("Game Data")]
    public ChartList chart_list;
    private AudioClip music;
    private int playID = 1;
    private Note[] note;

    [Header("Game State")]
    public bool isEnd = false;
    public float time = 0f;


    [Header("User Data")]
    public PlayData playData;
    public int curCombo;

    // life
    public int life;
    public int maxLife = 10;


    // ���� �������� ����
    [Header("Judge")]
    [SerializeField] private float perfectRange = 0.1f;
    [SerializeField] private float goodRange = 0.3f;
    [SerializeField] private float badRange = 0.8f;
    [SerializeField] private float missRange = 1f;
    [SerializeField] private float judgeRange = 0f;

    // ���� �������� ����
    [SerializeField] private float perfectScore = 500;
    [SerializeField] private float goodScore = 300;
    [SerializeField] private float badScore = 100;
    [SerializeField] private float missScore = 0;
    [SerializeField] private float comboScore = 10;



    public void Reset()
    {
        // �÷��� ������ �ʱ�ȭ
        playData.score = 0;
        playData.perfect = 0;
        playData.good = 0;
        playData.bad = 0;
        playData.miss = 0;
        playData.combo = 0;
        playData.rank = 0;
    }

    public void LoadData()
    {
        // �����͸� id�� �ҷ��� ������ string���� �ҷ��� ������
        // ä�� id : ����, ä��
        ChartList list = DataManager.Instance.Load<ChartList>("chart_" + music_name + ".json");
        ChartData chart = list.list[0];
        note = (Note[])chart.note.Clone();
        music = Resources.Load<AudioClip>("Wav/" + list.music_name + ".wav");
    }

    [ContextMenu("Play")]
    public void Play()
    {
        play = StartCoroutine(PlayCo());
    }

    IEnumerator PlayCo()
    {
        // ���� ������ �ʱ�ȭ
        Reset();

        // �÷��� ����
        LoadData();

        // �÷��̾� ���� ��ġ
        PlayerReposition();

        // ���� �� ù ��Ʈ �����ֱ�
        yield return StartCoroutine(ShowNextNoteCo());

        // ���� ī��Ʈ
        //yield return StartCoroutine(TimeCountCo());

        // ���� ����
        //bgm.Stop();
        //bgm.time = 0;


        yield return new WaitForSeconds(1);
        //bgm.Play();
    }

    public void Pause()
    {

    }


    [ContextMenu("Stop")]
    public void Stop()
    {
        // ���� ����
    }


    public void PlayerReposition()
    {
        //// ���� ��ġ ����
        //Vector2 firstNote = note[noteIndex].pos;
        //player.CurPos = firstNote;

        //LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        //if (Physics2D.Raycast(firstNote, Vector3.up, 1, mask) == false)
        //{
        //    player.CurPos += Vector3.up;
        //}
        //else if (Physics2D.Raycast(firstNote, Vector3.down, 1, mask) == false)
        //{
        //    player.CurPos += Vector3.down;
        //}
        //else if (Physics2D.Raycast(firstNote, Vector3.left, 1, mask) == false)
        //{
        //    player.CurPos += Vector3.left;
        //}
        //else if (Physics2D.Raycast(firstNote, Vector3.right, 1, mask) == false)
        //{
        //    player.CurPos += Vector3.right;
        //}
    }

    IEnumerator ShowNextNoteCo()
    {
        //var note1 = Instantiate(PrenotePrefab);
        //note1.transform.position = note[noteIndex].pos;
        //Destroy(note1, 3);
        yield return new WaitForSeconds(3);
    }




}
