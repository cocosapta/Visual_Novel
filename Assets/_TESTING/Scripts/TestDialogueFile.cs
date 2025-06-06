using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDialogueFile : MonoBehaviour
{
    [SerializeField] private TextAsset fileToRead = null;
    // Start is called before the first frame update
    void Start()
    {
        StartConversation();
    }

    // Update is called once per frame
    void StartConversation()
    {

        List<string> lines = FileManager.ReadTextAsset(fileToRead);

        // foreach (string line in lines)
        // {

        //     if (string.IsNullOrWhiteSpace(line))
        //         continue;
        //     DIALOGUE_LINE dl = DialogueParser.Parse(line);

        //     if (dl.commandData?.commands == null || dl.commandData.commands.Count == 0)
        //     {
        //         Debug.LogWarning($"No commands found in line: {line}");
        //         continue; // Lewati iterasi jika tidak ada commands
        //     }

        //     for (int i = 0; i < dl.commandData.commands.Count; i++)
        //     {
        //         DL_COMMAND_DATA.Command command = dl.commandData.commands[i];
        //         Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(",", command.arguments)}]");
        //     }
        // }

        DialogueSystem.instance.Say(lines);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            DialogueSystem.instance.dialogueContainer.Hide();

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            DialogueSystem.instance.dialogueContainer.Show();

    }

}
