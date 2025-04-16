using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;
using TMPro;

namespace TESTING
{

    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempFont;

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);
        // Start is called before the first frame update
        void Start()
        {
            // Character Nando = CharacterManager.instance.CreateCharacter("Bu Ana");
            StartCoroutine(Tes());

        }

        IEnumerator Tes()
        {

            Character_Sprite Ana = CreateCharacter("Bu Ana") as Character_Sprite;
            // Character_Sprite ana3 = CreateCharacter("ana3 as Bu Ana") as Character_Sprite;
            // Character_Sprite Rizal = CreateCharacter("Pak Rizal") as Character_Sprite;
            // Character_Sprite AnaRed = CreateCharacter("Bu Ana as Bu Ana") as Character_Sprite;
           
            
            Ana.SetPosition(new Vector2(1, 0));
            yield return new WaitForSeconds(1);
            Ana.TransitionSprite(Ana.GetSprite("Ana 1"));
            yield return Ana.Say("Semoga hariku menyenangkan");
            yield return Ana.Say("Kenapa ada orang yg kesini");
            Ana.Animate("Shiver", true);
            yield return new WaitForSeconds(1);

            Ana.FaceRight();
            Ana.Animate("Shiver", false);
            Ana.TransitionSprite(Ana.GetSprite("Ana 1"));
            Character_Sprite Nando = CreateCharacter("Nando") as Character_Sprite;
            Ana.FaceRight();
            Nando.SetPosition(new Vector2(0, 0));
            Ana.Animate("Hop");
            yield return Ana.Say("Ada yg bisa saya bantu");
            yield return Nando.Say("Permisi bu Ana{c} Saya nando {a}, saya ingin mendaftar");

            yield return new WaitForSeconds(1);


            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
