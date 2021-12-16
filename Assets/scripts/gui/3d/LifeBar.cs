using UnityEngine;
using UnityEngine.UI;

public class LifeBar 
{

    public LifeBar() {
        _anim = Main.shared.addGameObject("ui/lifebar");
        _anim.transform.SetParent(Main.shared.uiBottomLayer);
        _transform = _anim.transform as RectTransform;
        _text = _anim.GetComponentInChildren<Text>();
        _bar = _anim.GetComponent<Image>();
    }

    public void updateLifes( int maxlifes, int lifes ) {
        if (_value == lifes && _maxvalue == maxlifes) return;
        _value = lifes;
        _maxvalue = maxlifes;

        _bar.fillAmount = lifes / (float)maxlifes;
        _text.text = lifes.ToString();
        if ( _value == _maxvalue ) {
             visible = false;
        } else {
             visible = true;
        }
    }

    public bool visible {
        set {
            if (visible == value) return;
            _anim.SetActive(value);
        }
        get {
            return _anim.activeSelf;
        }
    }


    public void setPos3D( Vector3 pos3d ) {
        if (visible == false) return;
        Vector2 pos2d = RectTransformUtility.WorldToScreenPoint( Main.shared.camera, pos3d);
        Vector2 localpos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform.parent as RectTransform, pos3d, null, out localpos);
        _transform.anchoredPosition = pos2d;
    }


    public void remove() {
        Main.shared.removeGameObject(_anim);
    }


    private int _value;
    private int _maxvalue;




    private GameObject _anim;
    private RectTransform _transform;
    private Text _text;
    private Image _bar;
    
    
}
