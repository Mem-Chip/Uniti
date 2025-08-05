using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Dialogue;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextDisplayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float baseSpeed = 0.05f;  // 基础显示速度
    [SerializeField] private KeyCode skipKey = KeyCode.Space;

    [Header("Dialogue")]
    [SerializeField] private Dialogue[] dialogues = new Dialogue[0]; // 对话内容

    [Header("Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    private TextMeshProUGUI dialogueText;
    private float currentSpeed;
    private int currentLineIndex;
    private bool isTyping;
    public bool autoPlay = false; // 是否自动播放

    [SerializeField] private Image LeftPortrait; // 左侧角色头像
    [SerializeField] private Image CenterPortrait; // 中间角色头像
    [SerializeField] private Image RightPortrait; // 右侧角色头像

    [SerializeField] private Image Background;

    private Dictionary<string, Sprite> portraits = new Dictionary<string, Sprite>(); // 角色头像字典
    //private Dictionary<string, Sprite> backgrounds = new Dictionary<string, Sprite>();

    private bool inRichTextBracket = false; // 是否在富文本标签内

    void Update()
    {
        if (isTyping){
            // 如果正在打字，按下跳过键则跳过当前文本
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(skipKey))
            {
                isTyping = false;
            }
        }
    }

    private void Awake()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
        currentSpeed = baseSpeed;
        AudioSource music = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        var dialogue = UnityEngine.Resources.Load<TextAsset>($"Dialogues/{DialogueLoader.DialogueID}");
        Debug.Log(DialogueLoader.DialogueID);
        LoadNewDialogue(ParseFromCSV(dialogue));
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        OnDialogueStart?.Invoke();
        StartCoroutine(RunDialogue());
    }

    private IEnumerator RunDialogue()
    {
        while (currentLineIndex < dialogues.Length)
        {
            Dialogue line = dialogues[currentLineIndex];
            yield return TypeText(line);
            yield return WaitForNextLine();
            currentLineIndex++;
        }
        OnDialogueEnd?.Invoke();
    }

    private IEnumerator TypeText(Dialogue line)
    {
        currentSpeed = line.Speed == 0 ? 0 : (float) baseSpeed/line.Speed;

        Debug.Log("running on speed" + currentSpeed);

        dialogueText.text = line.Content;
        dialogueText.maxVisibleCharacters = 0;
        isTyping = true;

        if (!string.IsNullOrEmpty(line.Left))
        {
            if (!portraits.TryGetValue(line.Left, out var leftSprite))
            {
                leftSprite = UnityEngine.Resources.Load<Sprite>($"Portraits/{line.Left}");
                if (leftSprite != null) portraits[line.Left] = leftSprite;
                else Debug.LogWarning($"Portrait not found for {line.Left}");
            }
            LeftPortrait.sprite = leftSprite;
            LeftPortrait.enabled = leftSprite != null;
            if (line.Speaker == line.Left) LeftPortrait.color = Color.white; // 角色头像颜色
            else LeftPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1); // 非当前说话者头像颜色
        }
        else LeftPortrait.enabled = false;

        if (!string.IsNullOrEmpty(line.Right))
        {
            if (!portraits.TryGetValue(line.Right, out var rightSprite))
            {
                rightSprite = UnityEngine.Resources.Load<Sprite>($"Portraits/{line.Right}");
                if (rightSprite != null) portraits[line.Right] = rightSprite;
            }
            RightPortrait.sprite = rightSprite;
            RightPortrait.enabled = rightSprite != null;
            if (line.Speaker == line.Right) RightPortrait.color = Color.white;
            else RightPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else RightPortrait.enabled = false;

        if (string.IsNullOrEmpty(line.Background)) Background.sprite = UnityEngine.Resources.Load<Sprite>(line.Background);

        if (line.Speaker != null && line.Speaker != "") dialogueText.text = (
            $"{line.Speaker}："
            //+ "<font=ZhuQueFangSong>"
            + $"{line.Content}"
            //+ "</font>"
        );
        
        for (int i = 0; i < dialogueText.text.Length; i++)
        {
            dialogueText.maxVisibleCharacters = i + 1;
            char character = dialogueText.text[i];

            if (!isTyping && i > 1)
            {
                dialogueText.maxVisibleCharacters = dialogueText.text.Length; // 显示完整文本
                yield break;
            }

            // 标点符号停顿更长
            //if (i < dialogueText.text.Length && (character == ',' || character == '。' || character == '.' || 
            //    character == '!' || character == '?' || character == '，' || character == '！' || character == '？'))
            //{
            //    yield return new WaitForSeconds(currentSpeed * 2);
            //}
            if (character == '<') inRichTextBracket = true; // 进入富文本标签
            if (character == '>') { 
                inRichTextBracket = false; // 退出富文本标签
            }
            if (inRichTextBracket) continue; // 如果在富文本标签内，则跳过
            yield return new WaitForSeconds(currentSpeed);
        }
        
        isTyping = false;
    }

    private IEnumerator WaitForNextLine()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(skipKey))
            {
                yield break;
            }
            yield return null;
        }
    }

    public void SetTextSpeed(float speed)
    {
        // 将0-1的滑块值转换为实际速度（值越小速度越快）
        currentSpeed = Mathf.Lerp(0.01f, 0.1f, 1 - speed);
    }

    // 外部调用加载新对话
    public void LoadNewDialogue(Dialogue[] newLines)
    {
        dialogues = newLines;
        currentLineIndex = 0;
        StartDialogue();
    }
}
