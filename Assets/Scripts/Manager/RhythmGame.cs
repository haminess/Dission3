using System.Collections;
using UnityEngine;

// 리듬게임 플레이 시작, 종료 등을 제어하는 스크립트
public class RhythmGame : MonoBehaviour
{
    // 플레이 데이터
    // 1. 배경 음악
    // 2. 채보 데이터
    // 3. 플레이어/판정 점수
    // 4. 플레이 상태


    // 음악 당 채보 데이터
    // 1. 채보 아이디
    // 3. 노트 데이터
    private static RhythmGame instance;
    public static RhythmGame Instance
    {
        get
        {
            if (instance == null)
            {
                // 1. 먼저 씬에서 찾아보기
                instance = FindObjectOfType<RhythmGame>();

                // 2. 씬에도 없다면 생성
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


    // 메인 판정범위 관리
    [Header("Judge")]
    [SerializeField] private float perfectRange = 0.1f;
    [SerializeField] private float goodRange = 0.3f;
    [SerializeField] private float badRange = 0.8f;
    [SerializeField] private float missRange = 1f;
    [SerializeField] private float judgeRange = 0f;

    // 메인 점수범위 관리
    [SerializeField] private float perfectScore = 500;
    [SerializeField] private float goodScore = 300;
    [SerializeField] private float badScore = 100;
    [SerializeField] private float missScore = 0;
    [SerializeField] private float comboScore = 10;



    public void Reset()
    {
        // 플레이 데이터 초기화
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
        // 데이터를 id로 불러올 것인지 string으로 불러올 것인지
        // 채보 id : 음악, 채보
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
        // 게임 데이터 초기화
        Reset();

        // 플레이 세팅
        LoadData();

        // 플레이어 시작 위치
        PlayerReposition();

        // 시작 전 첫 노트 보여주기
        yield return StartCoroutine(ShowNextNoteCo());

        // 시작 카운트
        //yield return StartCoroutine(TimeCountCo());

        // 게임 시작
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
        // 게임 종료
    }


    public void PlayerReposition()
    {
        //// 시작 위치 조정
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
