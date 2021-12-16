using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Json;

public class Main : MonoBehaviour

{
    void Start() {
        shared = this;

        buffsBtn.onClick.AddListener(startWithBuffs);
        noBuffsBtn.onClick.AddListener(startWithoutBuffs);

        battlemenu = new BattleMenu();
        battlemenu.anim.transform.SetParent(uiTopLayer);

        new Config();
        _world = new BaseWorld();

        Propmob p1 = new Propmob();
        p1.model = "models/swat";
        p1.stats = Config.shared.getAllStats();
        _mob1 = _world.spawnMob(p1);

        Propmob p2 = new Propmob();
        p2.stats = Config.shared.getAllStats();
        p2.model = "models/robot";
        _mob2 = _world.spawnMob(p2);

        _mob1.anim.transform.position = new Vector3(1, 0, 0);
        _mob2.anim.transform.position = new Vector3(-1, 0, 0);
        _mob1.setTarget(_mob2);
        _mob2.setTarget(_mob1);

        battlemenu.selectMob1(_mob2);
        battlemenu.selectMob2(_mob1);


    }

    void Update() {
        delta = (int)Mathf.Round(Time.deltaTime * 1000);
        _world.update();
        battlemenu.Update();
    }

    private void startWithBuffs() {
        

        _mob1.removeAllBuffs();
        _mob2.removeAllBuffs();

        _mob1.lifes = _mob1.props.maxLifes;
        _mob2.lifes = _mob2.props.maxLifes;

        JsonObject settings = Config.shared.getSettings();
        int minbuffsCount = settings["buffCountMin"];
        int maxbuffsCount = settings["buffCountMax"];
        bool duplicateBuffs = settings["allowDuplicateBuffs"];

        int buffscount = UnityEngine.Random.Range(minbuffsCount, maxbuffsCount);
        List<BuffProps> buffs = Config.shared.getRandomBuffs( buffscount, duplicateBuffs );
        for ( int  i = 0; i < buffs.Count; i++ ) {
              StatsBuff buff = new StatsBuff();
              buff.init(buffs[i]);
              buff.duration = 999999;
              _mob1.applyBuff(buff);
        }
        
        buffscount = UnityEngine.Random.Range(minbuffsCount, maxbuffsCount);
        buffs = Config.shared.getRandomBuffs(buffscount, duplicateBuffs);
        for (int i = 0; i < buffs.Count; i++) {
            StatsBuff buff = new StatsBuff();
            buff.init(buffs[i]);
            buff.duration = 999999;
            _mob2.applyBuff(buff);
        }

    }





    private void startWithoutBuffs() {
        _mob1.removeAllBuffs();
        _mob2.removeAllBuffs();

        _mob1.lifes = _mob1.props.maxLifes;
        _mob2.lifes = _mob2.props.maxLifes;
    }


    public GameObject addGameObject(string asset_name) {
        GameObject go = Instantiate(Resources.Load(asset_name)) as GameObject;
        return go;
    }


    public void removeGameObject(GameObject go) {
        Destroy(go);
    }

    private BaseMob _mob1;
    private BaseMob _mob2;

    public Button buffsBtn;
    public Button noBuffsBtn;



    private BaseWorld _world;
    public  BattleMenu battlemenu;
    private Config _config;
    public Camera camera;
    public Canvas canvas;
    public RectTransform uiTopLayer;
    public RectTransform uiBottomLayer;
    public static int delta;
    public static Main shared;

}
