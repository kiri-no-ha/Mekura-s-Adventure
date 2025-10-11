using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogWindow : MonoBehaviour
{
    public int Index = 1;
    public TextMeshProUGUI log;
    public TextMeshProUGUI EventCast;
    public CanvasGroup cg;

    private List<string> logtext = new List<string>
    {
        

    };

    private Color logColor = Color.white;
    private Coroutine textAnimationCoroutine;
    private bool isTextAnimating = false;
    private Queue<string> messageQueue = new Queue<string>();

    public static DialogWindow Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        log.text = "";
        logColor = log.color;
        // «апускаем первое сообщение с анимацией
        Gamelog();
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote) || Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleConsole();
        }
    }
    public void AddMessage(List<string> textdialog)
    {
        logtext = textdialog;
    }
    private void ToggleConsole()
    {
        if (cg.alpha != 1)
        {
            cg.alpha = 1.0f;
            cg.interactable = true;
        }
        else
        {
            cg.alpha = 0f;
            cg.interactable = false;
        }
    }

    public void Gamelog()
    {
        if (Index >= 0 && Index < logtext.Count)
        {
            string messageToAdd = logtext[Index];
            AddMessageWithAnimation(messageToAdd);
        }
    }

    public void AddMessageWithAnimation(string message)
    {
        messageQueue.Enqueue(message);

        // ≈сли анимаци€ не запущена, запускаем обработку очереди
        if (!isTextAnimating)
        {
            StartCoroutine(ProcessMessageQueue());
        }
    }

    private IEnumerator ProcessMessageQueue()
    {
        isTextAnimating = true;

        while (messageQueue.Count > 0)
        {
            string currentMessage = messageQueue.Dequeue();

            // ƒобавл€ем перенос строки, если в логе уже есть текст
            if (!string.IsNullOrEmpty(log.text))
            {
                log.text = "";
            }

            yield return StartCoroutine(TextAnimation(currentMessage));
        }

        isTextAnimating = false;
    }

    private IEnumerator TextAnimation(string textToShow)
    {
        string currentText = "";

        foreach (char character in textToShow)
        {
            currentText += character;
            log.text = log.text.Substring(0, log.text.LastIndexOf('\n') + 1) + currentText;

            // ѕровер€ем на знаки препинани€ дл€ паузы
            if (character == '.' || character == ',' || character == '!' || character == '?')
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void garbage()
    {
        // ќстанавливаем все анимации при очистке
        if (textAnimationCoroutine != null)
        {
            {
                StopCoroutine(textAnimationCoroutine);
            }
            messageQueue.Clear();
            isTextAnimating = false;

            log.text = "консоль отчищена";
            Debug.Log(logtext);
        }
    }
    public void further()
    {
        Index++;
        Gamelog();
    }
}