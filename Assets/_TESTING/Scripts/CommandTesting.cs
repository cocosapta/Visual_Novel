using COMMANDS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Running());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            CommandManager.instance.Execute("moveCharDemo", "left");
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            CommandManager.instance.Execute("moveCharDemo", "Right");
    }

    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_lp", "hello world!");
        yield return CommandManager.instance.Execute("print_mp", "Line1", "Line2", "Line3");

        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_lp", "hello Lambda!");
        yield return CommandManager.instance.Execute("lambda_mp", "lambda1", "lambda2", "lambda3");

        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_lp", "3");
        yield return CommandManager.instance.Execute("process_mp", "Process Line 1", "Process Line 2", "Process Line 3");
    }
}
