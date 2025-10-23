using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMessenger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    private GameObject NPC;

    private void SearchNPS()
    {
        GameObject[] listNPC = GameObject.FindGameObjectsWithTag("NPC");
        float distance = Mathf.Infinity;
        foreach (GameObject npc in listNPC) {
            Vector3 diff = player.transform.position -  npc.transform.position;
            float distance2 = diff.magnitude;
            if (distance > distance2)
            {
                distance = distance2;
                NPC = npc;
            }
        }
        // сделать подсветку игрока
    }
    public void StartMessage()
    {
        SearchNPS();
        if (NPC is not null)
        {
            //List<string> dialogsfromNPC = NPC.GetComponent<CanTalk>().LoadDialog();
            //DialogWindow dialogWindow_script = player.GetComponent<DialogWindow>();
            //dialogWindow_script.AddMessage(dialogsfromNPC);
            //dialogWindow_script.Gamelog();
            
        }
        


    }
}
