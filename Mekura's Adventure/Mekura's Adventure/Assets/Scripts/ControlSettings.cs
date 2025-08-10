using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ControlSettings : MonoBehaviour
{
    public Slider sliderMouse;
    public List<TMP_Text> buttons;
    public TMP_Text[] images;
    public bool isWaitingBt;
    public int currentBt;
    private List<string> default_bt;
    void Start()
    {
        int e = 0;
        images[e++].text = PlayerPrefs.GetString("Forward_button", "W");
        images[e++].text = PlayerPrefs.GetString("Left_button", "A");
        images[e++].text = PlayerPrefs.GetString("Back_button", "S");
        images[e++].text = PlayerPrefs.GetString("Right_button", "D");
        images[e++].text = PlayerPrefs.GetString("Bt_Action", "E");
        sliderMouse.value = PlayerPrefs.GetFloat("MouseSense", 50f);
        foreach (var button in buttons)
        {
            button.text = "Èçìåíèòü";
        }
        isWaitingBt = false;
        default_bt = new List<string>()
        {
            "W", "A", "S", "D", "E"
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown && isWaitingBt)
        {
            Debug.Log(Event.current.keyCode);
            // ÏÎÎÎÎÌÅÅÅËËÎÎÎ!!!!!!!
            // ÏÎÎÎÎÌÅÅÅËËÎÎÎ!!!!!!!
            // ÏÎÎÎÎÌÅÅÅËËÎÎÎ!!!!!!!
            // ÏÎÎÎÎÌÅÅÅËËÎÎÎ!!!!!!!
            // ÏÎÎÎÎÌÅÅÅËËÎÎÎ!!!!!!!
            // ÑÎÕĞÀÍßÅÌ ÇÍÀÅ×ÅÍÈÅ ÊÍÎÏÊÈ
            images[currentBt].text = Event.current.keyCode.ToString();
            //buttons[currentBt].transform.GetComponentInChildren<TMP_Text>(true).text = "Íàæìèòå äëÿ âûáîğà";
            //GetChild().text = "Íàæìèòå äëÿ âûáîğà";
            buttons[currentBt].text = "Èçìåíèòü";
            isWaitingBt = false;

        }

    }
    public void IsActiveWaiting(int cur)
    {
        isWaitingBt = !isWaitingBt;
        currentBt = cur;
        buttons[cur].text="Âûáåğåòå êëàâèøó";
        Debug.Log($"IsWaiting={isWaitingBt}");
    }
    public void SaveSettings()
    {
        if (!isWaitingBt)
        {
            int e = 0;
            PlayerPrefs.SetString("Forward_button", images[e++].text);
            PlayerPrefs.SetString("Left_button", images[e++].text);
            PlayerPrefs.SetString("Back_button", images[e++].text);
            PlayerPrefs.SetString("Right_button", images[e++].text);
            PlayerPrefs.SetString("Bt_Action", images[e++].text);
            PlayerPrefs.SetFloat("MouseSense", sliderMouse.value);
            e = 0;
        }
        else { Debug.Log("Ñíà÷àëî âûáåğè"); }
    }
    public void TrowSettings()
    {
        if (!isWaitingBt)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].text = default_bt[i];
            }
        }
        else { Debug.Log("Ñíà÷àëî âûáåğè"); }
    }
}
