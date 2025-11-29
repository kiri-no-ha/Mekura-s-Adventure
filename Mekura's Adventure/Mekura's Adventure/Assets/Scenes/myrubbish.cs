using UnityEngine;

public class myrubbish : MonoBehaviour
{
    public TextAsset dialogXmlFile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Dialog dialog = Dialog.Load(dialogXmlFile);
        foreach (string text in dialog.texts)
        {
            Debug.Log(text); // Выведет все три текста
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
