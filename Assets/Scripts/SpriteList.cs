using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteList : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    
    Image _image = null;

    Image image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();
            return _image;
        }
    }


    public void SetSprite(int index)
    {
        if (index < 0 || index >= sprites.Count) return;
        image.sprite = sprites[index];
    }
}
