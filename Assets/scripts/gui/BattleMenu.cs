using UnityEngine;
using System.Collections;

public class BattleMenu 
{

    public BattleMenu() {
        anim = Main.shared.addGameObject("ui/battlemenu");
        helper = anim.GetComponent<BattleMenuHelper>();
        helper.transform.SetParent(Main.shared.uiTopLayer);
        (helper.transform as RectTransform).offsetMin = new Vector2();
        (helper.transform as RectTransform).offsetMax = new Vector2();

        panel1 = new MobPanel();
        panel1.anim.transform.parent = helper.leftPanel;
        RectTransform r = panel1.anim.transform as RectTransform;
        r.offsetMax = new Vector2(r.offsetMax.x, 0);

        panel2 = new MobPanel();
        panel2.anim.transform.parent = helper.rightPanel;
        r = panel2.anim.transform as RectTransform;
        r.offsetMax = new Vector2(r.offsetMax.x, 0);
        r.anchoredPosition = new Vector2(0, 0);
    }

    public void Update() {
        panel1.update();
        panel2.update();
    }


    public void selectMob1( BaseMob mob ) {
        panel1.selectMob(mob);
    }


    public void selectMob2(BaseMob mob) {
        panel2.selectMob(mob);
    }

    private BattleMenuHelper helper;
    public GameObject anim;

    private MobPanel panel1;
    private MobPanel panel2;
    


}
