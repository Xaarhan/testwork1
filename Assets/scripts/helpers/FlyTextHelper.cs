using UnityEngine;
using UnityEngine.UI;

public class FlyTextHelper : MonoBehaviour
{
    void Start()
    {
        
    }


    public void setValue( int val ) {
        if (val > 0) {
            text.color = goodColor;
        } else {
            text.color = badColor;
        }
        text.text = val.ToString();
    }

    void Update() {
        Vector2 pos2d = RectTransformUtility.WorldToScreenPoint(Main.shared.camera, pos3d);
        Vector2 localpos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, pos3d, null, out localpos);
        (transform as RectTransform).anchoredPosition = pos2d;
        pos3d.y += speed * Main.delta;
        time = time - Main.delta;
        if ( time <= 0 ) {
             Destroy(gameObject);
        }
    }

    public Color goodColor;
    public Color badColor;

    public Vector3 pos3d;
    public Text text;
    public int time;
    public float speed;

}
