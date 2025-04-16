using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
         StartCoroutine(Running());   
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        IEnumerator Running2()
        {
            Character_Sprite Nando = CreateCharacter("Nando") as Character_Sprite;
            Character Rizal = CreateCharacter("Pak Rizal");
            Nando.Show();

            // yield return new WaitForSeconds(0.5f);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop:true);

            yield return Rizal.Say("I'm going to turn off the radio.");
            // Nando.Animate("Hop");
            // Nando.Say("Wow");

            AudioManager.instance.StopSoundEffect("RadioStatic");
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");

            Nando.Say("Okay!");
        }

        IEnumerator Running(){
            // AudioChannel channel = new AudioChannel(1);
            yield return new  WaitForSeconds(1);

            Character_Sprite Nando = CreateCharacter("Nando") as Character_Sprite;

            Nando.Show();

            yield return DialogueSystem.instance.Say("Narrator", "Can we see yout ship?");

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/05");
            AudioManager.instance.PlayTrack("Audio/Music/Upbeat", 0);
            // AudioManager.instance.PlayVoice("Audio/Voices/exclamation");

            Nando.MoveToPosition(new Vector2(0.7f, 0), speed:0.5f);
            yield return Nando.Say("Ahhh");

            AudioManager.instance.StopTrack(0);
            yield return DialogueSystem.instance.Say("Narrator", "Can we see yout house");

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/gerbang");
            yield return DialogueSystem.instance.Say("Narrator", "Can we see yout p");
             
            // yield return null; 
        }
    }

}
