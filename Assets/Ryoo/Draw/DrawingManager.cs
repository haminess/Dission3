using UnityEngine;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    public GameObject drawingPanel;
    public Button penButton;
    public Button eraserButton;
    public Button resetButton;
    public Texture2D eraserCursorTexture;
    private Texture2D drawingTexture;
    private bool isDrawingPanelActive = false;
    private bool isEraserActive = false;
    private bool isPenActive = false;
    private Image drawingImage;
    private Vector2 previousDrawPoint;
    private bool isPreviousPointSet = false; // 이전 점이 설정되었는지 여부
    private int eraserSize = 10; // 기본 지우개 크기
    private int penSize = 2; // 기본 펜 크기
    private GameObject eraserCursor;
    public SpriteRenderer chalkboardRenderer;
    private Color customChalkboardColor = new Color(101f / 255f, 144f / 255f, 115f / 255f); // #659073
    private Color penColor = Color.white; // 펜 색상
    private GameObject penCursor; // 펜 커서
    public Text penSizeText; // 펜 굵기 표시용 텍스트
    public Text eraserSizeText; // 지우개 굵기 표시용 텍스트

    public GameObject drawingPaper;

    public Sprite penDefaultSprite;
    public Sprite penPressedSprite;
    public Sprite eraserDefaultSprite;
    public Sprite eraserPressedSprite;
    public Sprite resetDefaultSprite;
    public Sprite resetPressedSprite;

    void Start()
    {
        // 원본 이미지의 크기 가져오기
        RectTransform drawingPanelRect = drawingPanel.GetComponent<RectTransform>();
        float panelHeight = drawingPanelRect.rect.height;
        float panelWidth = panelHeight * (16f / 9f); // 예를 들어, 16:9 비율로 설정 가능

        drawingPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);
        drawingPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelHeight);

        int width = (int)panelWidth;
        int height = (int)panelHeight;

        // Texture2D 객체 생성 (알파 채널 지원을 위해 RGBA32 형식 사용)
        drawingTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // drawingTexture를 투명하게 초기화
        Color[] clearPixels = new Color[width * height];
        for (int i = 0; i < clearPixels.Length; i++)
        {
            clearPixels[i] = Color.clear;
        }
        drawingTexture.SetPixels(clearPixels);
        drawingTexture.Apply();

        // 초기화
        ResetDrawing();

        drawingImage = drawingPanel.GetComponent<Image>();
        Sprite drawingSprite = Sprite.Create(drawingTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        drawingImage.sprite = drawingSprite;

        // 버튼 클릭 이벤트
        penButton.onClick.AddListener(TogglePen);
        eraserButton.onClick.AddListener(ToggleEraser);
        resetButton.onClick.AddListener(ResetDrawing); // 초기화 버튼 클릭 이벤트 추가

        // 지우개 커서 초기화
        CreateEraserCursor();

        // 게임이 시작될 때 원본 스프라이트를 업데이트
        UpdateChalkboard();

        CreatePenCursor(); // 펜 커서 초기화

        // Pen Button 설정
        SetButtonSprites(penButton, penDefaultSprite, penPressedSprite);

        // Eraser Button 설정
        SetButtonSprites(eraserButton, eraserDefaultSprite, eraserPressedSprite);

        // Reset Button 설정
        SetButtonSprites(resetButton, resetDefaultSprite, resetPressedSprite);

        // 초기 UI 텍스트 설정
        UpdatePenSizeText();
        UpdateEraserSizeText();
    }

    void Update()
    {
        // 그림판이 활성화된 상태에서 마우스 클릭으로 그림 그리기
        if (isDrawingPanelActive && (isPenActive || isEraserActive))
        {
            if (Input.GetMouseButton(0))
            {
                Draw(false);
            }
            else
            {
                isPreviousPointSet = false; // 마우스를 뗐을 때 이전 점 초기화
            }

            // 펜 또는 지우개의 크기 조절
            if (isPenActive)
            {
                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    IncreasePenSize();
                }
                if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    DecreasePenSize();
                }
            }
            else if (isEraserActive)
            {
                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    IncreaseEraserSize();
                }
                if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    DecreaseEraserSize();
                }
            }

            // 지우개 커서 위치 업데이트
            if (isEraserActive)
            {
                UpdateEraserCursor();
            }

            if (isPenActive)
            {
                UpdatePenCursor(); // 펜 커서 위치 업데이트
            }
        }
    }

    public void ToggleDrawingPanel()
    {
        isDrawingPanelActive = !isDrawingPanelActive;
        drawingPanel.SetActive(isDrawingPanelActive);
        drawingPaper.SetActive(isDrawingPanelActive);

        if (!isDrawingPanelActive)
        {
            UpdateChalkboard(); // 그림판이 닫힐 때 칠판 리소스를 업데이트
        }
    }

    void TogglePen()
    {
        if (isPenActive)
        {
            DeactivatePen();
        }
        else
        {
            ActivatePen();
        }
    }

    void ToggleEraser()
    {
        if (isEraserActive)
        {
            DeactivateEraser();
        }
        else
        {
            DeactivatePen();
            ActivateEraser();
        }
    }

    void ActivatePen()
    {
        isPenActive = true;
        isEraserActive = false;
        eraserCursor.SetActive(false);

        UpdatePenCursorSize(); // 펜 커서 크기 업데이트
        penCursor.SetActive(true); // 펜 커서를 활성화

        // 버튼 이미지 업데이트
        penButton.image.sprite = penPressedSprite;
        eraserButton.image.sprite = eraserDefaultSprite;
    }

    void DeactivatePen()
    {
        isPenActive = false;
        penCursor.SetActive(false); // 펜 커서를 숨김

        // 버튼 이미지 업데이트
        penButton.image.sprite = penDefaultSprite;
    }

    void ActivateEraser()
    {
        isEraserActive = true;
        isPenActive = false;
        eraserSize = 10; // 지우개 크기를 기본값으로

        UpdateEraserCursorSize(); // 지우개 커서 크기를 초기화된 값으로 설정
        eraserCursor.SetActive(true);

        // 버튼 이미지 업데이트
        eraserButton.image.sprite = eraserPressedSprite;
        penButton.image.sprite = penDefaultSprite;
    }

    void DeactivateEraser()
    {
        isEraserActive = false;
        eraserCursor.SetActive(false); // 지우개 커서를 숨김

        // 버튼 이미지 업데이트
        eraserButton.image.sprite = eraserDefaultSprite;
    }

    void IncreaseEraserSize()
    {
        eraserSize = Mathf.Min(eraserSize + 1, 50); // 지우개 크기를 최대 50까지
        UpdateEraserCursorSize();
        Debug.Log($"Eraser size increased: {eraserSize}");
        UpdateEraserSizeText(); // UI 업데이트
    }

    void DecreaseEraserSize()
    {
        eraserSize = Mathf.Max(eraserSize - 1, 1); // 지우개 크기를 최소 1까지
        UpdateEraserCursorSize();
        Debug.Log($"Eraser size decreased: {eraserSize}");
        UpdateEraserSizeText(); // UI 업데이트
    }

    void IncreasePenSize()
    {
        penSize = Mathf.Min(penSize + 1, 20); // 펜 굵기를 최대 20까지
        UpdatePenCursorSize(); // 펜 커서 크기 업데이트
        Debug.Log($"Pen size increased: {penSize}");
        UpdatePenSizeText(); // UI 업데이트
    }

    void DecreasePenSize()
    {
        penSize = Mathf.Max(penSize - 1, 1); // 펜 굵기를 최소 1까지
        UpdatePenCursorSize(); // 펜 커서 크기 업데이트
        Debug.Log($"Pen size decreased: {penSize}");
        UpdatePenSizeText(); // UI 업데이트
    }

    void UpdatePenSizeText()
    {
        if (penSizeText != null)
        {
            penSizeText.text = "Pen Size: " + penSize;
        }
    }

    void UpdateEraserSizeText()
    {
        if (eraserSizeText != null)
        {
            eraserSizeText.text = "Eraser Size: " + eraserSize;
        }
    }

    void Draw(bool isNewLine)
    {
        if (!isPenActive && !isEraserActive) return; // 펜과 지우개가 모두 비활성화된 경우

        RectTransform rt = drawingPanel.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPoint);

        // 그림판 내에서의 좌표로 변환
        float relativeX = (localPoint.x + rt.rect.width * 0.5f) / rt.rect.width;
        float relativeY = (localPoint.y + rt.rect.height * 0.5f) / rt.rect.height;

        int x = Mathf.Clamp((int)(relativeX * drawingTexture.width), 0, drawingTexture.width - 1);
        int y = Mathf.Clamp((int)(relativeY * drawingTexture.height), 0, drawingTexture.height - 1);

        if (!isPreviousPointSet)
        {
            previousDrawPoint = new Vector2(x, y);
            isPreviousPointSet = true;
        }

        DrawLine(previousDrawPoint, new Vector2(x, y));

        previousDrawPoint = new Vector2(x, y);

        drawingTexture.Apply();
    }

    void DrawLine(Vector2 start, Vector2 end)
    {
        int width = drawingTexture.width;
        int height = drawingTexture.height;

        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int err = dx - dy;
        int e2;

        while (true)
        {
            if (isEraserActive)
            {
                EraseCircle(x0, y0, Color.clear);  // 지우개는 투명하게 만듦
            }
            else if (isPenActive)
            {
                DrawCircle(x0, y0, penSize, penColor);
            }
            if (x0 == x1 && y0 == y1) break;

            e2 = err * 2;

            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    void DrawCircle(int centerX, int centerY, int radius, Color color)
    {
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int drawX = Mathf.Clamp(centerX + x, 0, drawingTexture.width - 1);
                    int drawY = Mathf.Clamp(centerY + y, 0, drawingTexture.height - 1);
                    drawingTexture.SetPixel(drawX, drawY, color);
                }
            }
        }
    }

    void EraseCircle(int centerX, int centerY, Color eraseColor)
    {
        int radius = eraserSize / 2;
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int drawX = Mathf.Clamp(centerX + x, 0, drawingTexture.width - 1);
                    int drawY = Mathf.Clamp(centerY + y, 0, drawingTexture.height - 1);
                    drawingTexture.SetPixel(drawX, drawY, eraseColor);  // 지우개 기능: 지정한 색상으로 칠하기
                }
            }
        }
    }

    void ResetDrawing()
    {
        // 펜과 지우개가 초기화되었는지 확인하고 비활성화
        if (penButton != null && penCursor != null)
        {
            DeactivatePen();
        }
        if (eraserButton != null && eraserCursor != null)
        {
            DeactivateEraser();
        }

        // 그림판을 투명하게 초기화
        Color[] clearPixels = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < clearPixels.Length; i++)
        {
            clearPixels[i] = Color.clear;
        }
        drawingTexture.SetPixels(clearPixels);
        drawingTexture.Apply();
    }

    void CreateEraserCursor()
    {
        // 지우개 커서를 위한 게임오브젝트 생성
        eraserCursor = new GameObject("EraserCursor");
        eraserCursor.transform.SetParent(drawingPanel.transform);

        // 커서 이미지를 설정
        Image cursorImage = eraserCursor.AddComponent<Image>();
        cursorImage.sprite = Sprite.Create(eraserCursorTexture, new Rect(0, 0, eraserCursorTexture.width, eraserCursorTexture.height), new Vector2(0.5f, 0.5f));
        cursorImage.color = new Color(1f, 1f, 1f, 0.5f);  // 반투명
        cursorImage.raycastTarget = false; // 커서가 다른 UI의 상호작용을 방해하지 않도록

        // 초기 크기 및 위치 설정
        UpdateEraserCursorSize();
        eraserCursor.SetActive(false);
    }

    void UpdateEraserCursorSize()
    {
        if (eraserCursor != null)
        {
            // 지우개 크기에 맞게 커서 크기 조정
            eraserCursor.GetComponent<RectTransform>().sizeDelta = new Vector2(eraserSize * 2, eraserSize * 2);
        }
    }

    void UpdateEraserCursor()
    {
        if (eraserCursor != null)
        {
            // 마우스 위치에 맞게 지우개 커서를 이동
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingPanel.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            eraserCursor.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    void UpdateChalkboard()
    {
        // 현재 drawingTexture를 사용하여 새로운 스프라이트 생성
        float pixelsPerUnit = chalkboardRenderer.sprite.pixelsPerUnit;

        Sprite updatedSprite = Sprite.Create(drawingTexture,
            new Rect(0, 0, drawingTexture.width, drawingTexture.height),
            new Vector2(0.5f, 0.5f), pixelsPerUnit);

        // 스프라이트 렌더러에 업데이트된 스프라이트 적용
        chalkboardRenderer.sprite = updatedSprite;

        // 스프라이트 렌더러의 머티리얼을 투명을 지원하는 것으로 변경
        chalkboardRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void CreatePenCursor()
    {
        // 펜 커서를 위한 게임오브젝트 생성
        penCursor = new GameObject("PenCursor");
        penCursor.transform.SetParent(drawingPanel.transform);

        // 커서 이미지를 설정
        Image cursorImage = penCursor.AddComponent<Image>();
        cursorImage.color = new Color(1f, 1f, 1f, 0.5f); // 반투명 흰색 원형 커서
        cursorImage.raycastTarget = false;

        // 초기 크기 및 위치 설정
        UpdatePenCursorSize();
        penCursor.SetActive(false);
    }

    void UpdatePenCursorSize()
    {
        if (penCursor != null)
        {
            // 펜 크기에 맞게 커서 크기 조정
            penCursor.GetComponent<RectTransform>().sizeDelta = new Vector2(penSize * 2, penSize * 2);
        }
    }

    void UpdatePenCursor()
    {
        if (penCursor != null)
        {
            // 마우스 위치에 맞게 펜 커서를 이동
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingPanel.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            penCursor.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    void SetButtonSprites(Button button, Sprite defaultSprite, Sprite pressedSprite)
    {
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.sprite = defaultSprite;

        SpriteState spriteState = new SpriteState
        {
            pressedSprite = pressedSprite,
            highlightedSprite = defaultSprite, // 강조 시에도 기본 스프라이트 사용
            disabledSprite = defaultSprite // 비활성화 시에도 기본 스프라이트 사용
        };
        button.spriteState = spriteState;
    }
}