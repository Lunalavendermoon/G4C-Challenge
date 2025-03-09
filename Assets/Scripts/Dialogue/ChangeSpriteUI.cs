using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangeSpriteUI : MonoBehaviour
{
    public Image image;

    public List<Sprite> UISprites;

    void Start() {
        if (image == null) {
            Debug.LogError("Image component is not assigned in the Inspector on " + gameObject.name);
        }
    }

    public void ChangeTo(int num) {
        if (image == null) {
            return;
        }

        if (num < 0 || num >= UISprites.Count) {
            return;
        }

        image.sprite = UISprites[num];
        ResetColor(); // Reset color whenever the sprite changes
    }

    // Changes the image color to grey
    public void GreyOut() {
        if (image != null) {
            image.color = Color.gray; // Sets the image to grey
        }
    }

    // Resets the image color to white (default)
    public void ResetColor() {
        if (image != null) {
            image.color = Color.white;
        }
    }
}
