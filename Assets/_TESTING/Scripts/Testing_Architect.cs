using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {

        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;
        // Start is called before the first frame update
        string[] lines = new string[5]
        {
            "kalimat random line of dialogue",
            "kalimat random line of dialogue 2",
            "kalimat random line of dialogue 3",
            "kalimat random line of dialogue 4",
            "kalimat random line of dialogue 5",

        };
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                architect.Stop();
            }

            string longLine = " ini adalah kalimat yang sangat panjang menjadi tidak tau jadi Saya cuma mencoba, kamu tau, saya belum tidur? sekarang adalah cara saya untuk Push skiil saya.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                {
                    architect.Build(longLine);
                    // architect.Build(lines[Random.Range(0, lines.Length)]);
                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longLine);
                // architect.Append(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}
