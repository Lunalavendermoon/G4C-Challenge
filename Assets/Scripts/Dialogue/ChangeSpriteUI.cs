using UnityEngine;
using UnityEngine.UI;
public class ChangeSpriteUI : MonoBehaviour
{
   //Attach this script to an Image GameObject and set its Source Image to the Sprite you would like.

    Image image;
    //Set this in the Inspector
    public Sprite a;
    public Sprite b;
    public Sprite c;

    // void Start()
    // {
    //     //Fetch the Image from the GameObject
    //     image = GetComponent<Image>();
        
    //     if (image == null)
    //         Debug.LogError($"No Image component found on {gameObject.name}.");
    //     else
    //         Debug.Log($"Image component found on {gameObject.name}.");


    // }

    void Awake() {
      image = GetComponent<Image>();
    }
    public void Change(char spriteName) {
        if (image == null)
        {
            Debug.LogError("Image component is not assigned.");
            return;
        }
        switch(spriteName) 
        {
          case 'a':
            if (a == null)
                Debug.LogError("Sprite 'a' is not assigned.");
            else
                image.sprite = a;
            break;
          case 'b':
            image.sprite = b;
            break;
          default:
            image.sprite = c;
            break;
        }
    }

    public void ChangeAuto() {
      if (image.sprite == a) {
        image.sprite = b;
      }
      else if (image.sprite == b) {
        image.sprite = c;
      }
      else if (image.sprite == c) {
        image.sprite = a;
      }
    }
}
