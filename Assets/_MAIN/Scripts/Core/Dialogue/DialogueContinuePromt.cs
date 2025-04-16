using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueContinuePromt : MonoBehaviour
    {
        private RectTransform root;
        [SerializeField] private Animator anim;
        [SerializeField] private TextMeshProUGUI tmpro;

        public bool isShowing => anim.gameObject.activeSelf;
        // Start is called before the first frame update
        void Start()
        {
            root = GetComponent<RectTransform>();
        }

        public void Show()
        {
            if (tmpro.text == string.Empty)
            {
                if (isShowing)
                    Hide();
                
                return;
            }
            
            tmpro.ForceMeshUpdate();

            anim.gameObject.SetActive(true);
            root.transform.SetParent(tmpro.transform);

            TMP_CharacterInfo finalCharcter = tmpro.textInfo.characterInfo[tmpro.textInfo.characterCount -1];
            Vector3 targetPos = finalCharcter.bottomRight;
            float characterWidth = finalCharcter.pointSize * 0.5f;
            targetPos = new Vector3(targetPos.x + characterWidth, targetPos.y, 0);
            root.localPosition = targetPos;
        }
        public void Hide()
        {
            anim.gameObject.SetActive(false);
        }
    }
}
