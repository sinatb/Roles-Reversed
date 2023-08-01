using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PerkManager
{
    private List<TroopPerk> tp;
    private List<EnemyPerk> ep;
    private static PerkManager instance;
    public static PerkManager GetInstance()
    {
        if(instance == null)
        {
            instance = new PerkManager();
        }
        return instance;
    }

    public TroopPerk GetTroopPerk()
    {
        return tp[Random.Range(0, tp.Count)];
    }

    public EnemyPerk GetEnemyPerk()
    {
        return ep[Random.Range(0, ep.Count)];
    }
    private PerkManager()
    {
        tp = new List<TroopPerk>();
        ep = new List<EnemyPerk>();
        //troop perks
        //sp = speed
        tp.Add(new TroopPerk("sp",1.2f));
        //hp = health point
        tp.Add(new TroopPerk("hp",1.2f));
        //st = spawn timer
        tp.Add(new TroopPerk("st",0.8f));
        //dm = damage
        tp.Add(new TroopPerk("dm",1.2f));
        //at = attack time
        tp.Add(new TroopPerk("at",0.9f));
        
        //enemy perks
        //sp = speed
        ep.Add(new EnemyPerk("sp",1.2f));
        //hp = health point
        ep.Add(new EnemyPerk("hp",1.2f));
        //ra = range
        ep.Add(new EnemyPerk("ra",1.1f));
        //bn = bullet number
        ep.Add(new EnemyPerk("bn",1.0f));
        //at = attack time
        ep.Add(new EnemyPerk("at",0.9f));
        
    }
}
