using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteServices : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprite;
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(_textMeshPro.text== "Treinamento")
        {
            this.gameObject.GetComponent<Image>().sprite = _sprite[0];
        }
        if (_textMeshPro.text == "Técnologia")
        {
            this.gameObject.GetComponent<Image>().sprite = _sprite[1];
        }
        if (_textMeshPro.text == "Infraestrutura")
        {
            this.gameObject.GetComponent<Image>().sprite = _sprite[2];
        }
    }
}
