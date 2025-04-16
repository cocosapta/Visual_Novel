using COMMANDS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class CMD_DatabaseExtension_Examples : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("print", new Action(PrintDefaultMassage));
            database.AddCommand("print_lp", new Action<string>(PrintUsermessage));
            database.AddCommand("print_mp", new Action<string[]>(PrintLines));

            database.AddCommand("lambda", new Action(() => { Debug.Log("Printing a default message to console from lambda  command."); }));
            database.AddCommand("lambda_lp", new Action<string>((args) => { Debug.Log($"Log user Lambda Message: '{args}'"); }));
            database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); }));

            database.AddCommand("process", new Func<IEnumerator>(SImpleProcess));
            database.AddCommand("process_lp", new Func<string, IEnumerator>(LineProcess));
            database.AddCommand("process_mp", new Func<string[], IEnumerator>(MultiLineProcess));

            database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));

        }
        private static void PrintDefaultMassage()
        {
            Debug.Log("Printing a default message to console.");
        }

        private static void PrintUsermessage(string message)
        {
            Debug.Log($"User Message: '{message}'");
        }
        private static void PrintLines(string[] lines)
        {
            int i = 1;
            foreach (string line in lines)
            {
                Debug.Log($"{i++}. '{line}'");
            }
        }

        private static IEnumerator SImpleProcess()
        {
            for (int i = 1; i <= 5; i++)
            {
                Debug.Log($"Process Running....[{i}]");
                yield return new WaitForSeconds(1);
            }
        }
        private static IEnumerator LineProcess(string data)
        {
            if (int.TryParse(data, out int num))
            {
                for (int i = 1; i <= num; i++)
                {
                    Debug.Log($"Process Running....[{i}]");
                    yield return new WaitForSeconds(1);
                }
            }
        }
        private static IEnumerator MultiLineProcess(string[] data)
        {
            foreach (string line in data)
            {

                Debug.Log($"Process Running....'{line}'");
                yield return new WaitForSeconds(0.5f);
            }
        }

        private static IEnumerator MoveCharacter(string direction)
        {
            bool left = direction.ToLower() == "left";

            Transform character = GameObject.Find("Image").transform;
            float moveSpeed = 15;

            float targetx = left ? -8 : 8;

            float currentx = character.position.x;

            while (Mathf.Abs(targetx - currentx) < 0.1f)
            {
                // Debug.Log($"Moving Character to {(left ? "left" : "right")} [{currentx}/{targetx}] ");
                currentx = Mathf.MoveTowards(currentx, currentx, moveSpeed * Time.deltaTime);
                character.position = new Vector3(currentx, character.position.y, character.position.z);
                yield return null;
            }

        }
    }

}
