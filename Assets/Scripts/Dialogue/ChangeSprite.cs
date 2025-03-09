using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    //Attach this script to an Image GameObject and set its Source Image to the Sprite you would like.

    SpriteRenderer image;
    //Set this in the Inspector
    public List<Sprite> Sprites;

    void Awake() {
      image = GetComponent<SpriteRenderer>();
    }
    public void ChangeTo(int index) {
        image.sprite = Sprites[index];
    }
    public int GetCurrentSprite() {
        for (int i = 0; i < Sprites.Count; i++) {
          if (Sprites[i] == image.sprite) {
            return i;
          }
        }
        return -1;
    }

    // public void ChangeAutoRight() {
    //   if (image.sprite == Sprites[Sprites.Count - 1]) {
    //     image.sprite = Sprites[1];
    //   }
    //   else if (image.sprite == Sprites[Sprites.Count - 2]) {
    //     image.sprite = Sprites[0];
    //   }
    //   else {
    //     image.sprite = Sprites[GetCurrentSprite() + 2];
    //   }
    // }

    // public void ChangeAutoLeft() {
    //   if (image.sprite == Sprites[1]) {
    //     image.sprite = Sprites[Sprites.Count - 1];
    //   }
    //   else if (image.sprite == Sprites[0]) {
    //     image.sprite = Sprites[Sprites.Count - 2];
    //   }
    //   else {
    //     image.sprite = Sprites[GetCurrentSprite() - 2];
    //   }
    // }
}
