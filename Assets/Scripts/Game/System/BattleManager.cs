using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

//单例化
public class BattleManager : SingletonMono<BattleManager>
{
    /*--------------------------------------------------variant and init----------------------------------------------------------------------------------------*/

    private static readonly List<(ICanBattle combatant, int initiative)> _combatantsList = new();
    public static IReadOnlyList<(ICanBattle combatant, int initiative)> CombatantsList => _combatantsList.AsReadOnly();     //参战者列表                                                                   //参战者和先攻值 
    private static readonly Queue<ICanBattle> _initiativeQueue = new();
    public static IEnumerable<ICanBattle> InitiativeQueue => _initiativeQueue;                                      //先攻队列
    public static event Action CombatantsListChangeEvent;                       //参战者列表变化事件
    public static event Action InitiativeQueueChangeEvent;                      //先攻顺序变化事件

    public static int BattleTurn { get; private set; } = 0;                                                         //回合轮数
    public static ICanBattle CombatantOnTurn { get; private set; } = null;                                                 //正在回合的实例
    public static event Action BattleStartEvent;                                                                    //开始战斗事件
    public static event Action BattleEndEvent;                                                                      //结束战斗事件
    public static event Action NextTurnEvent;
    public static event Action TurnStartEvent;                                                                //实体开始回合事件
    public static event Action TurnEndEvent;                                                                  //实体结束回合事件

    private Coroutine _battleCoroutine = null;                                                                              //私有，战斗协程

    private List<ICanBattle> _startBattlePara = new();

    /*--------------------------------------------------functions--------------------------------------------------------------------------------------------*/

    private void OrderInitiativeQueue()                                                   //排序战斗实体
    {
        _initiativeQueue.Clear();
        //按先攻排序
        _combatantsList
            .OrderByDescending(i => i.initiative)
            .ToList()
            .ForEach(t => _initiativeQueue.Enqueue(t.combatant));

        InitiativeQueueChangeEvent?.Invoke();
    }

    public void AddToBattle(ICanBattle newCombatant)                                                           //将列表中实体添加到战斗
    {
        if (newCombatant == null) return;

        newCombatant.EnBattle();

        //添加到参战列表
        _combatantsList.Add((newCombatant, newCombatant.Getinitiative()));

        CombatantsListChangeEvent?.Invoke();
        OrderInitiativeQueue();
    }
    public void AddToBattle(List<ICanBattle> newCombatants)
    {
        if (newCombatants == null || newCombatants.Count() == 0) return;

        //添加到参战列表
        newCombatants.ForEach(c =>
        {
            c.EnBattle();
            _combatantsList.Add((c, c.Getinitiative()));
        });

        CombatantsListChangeEvent?.Invoke();
        OrderInitiativeQueue();
    }
    public void RemoveFromBattle(ICanBattle combatant)
    {
        if (combatant == null) return;

        combatant.DeBattle();
        _combatantsList.ForEach(t =>
        {
            if (t.combatant == combatant) _combatantsList.Remove(t);
        });

        CombatantsListChangeEvent?.Invoke();
        OrderInitiativeQueue();
    }
    public void RemoveFromBattle(List<ICanBattle> combatants)
    {
        if (combatants == null || combatants.Count() == 0) return;

        combatants.ForEach(c =>
        {
            c.DeBattle();
            _combatantsList.ForEach(t =>
            {
                if (t.combatant == c) _combatantsList.Remove(t);
            });
        });

        CombatantsListChangeEvent?.Invoke();
        OrderInitiativeQueue();
    }

    public void StartBattle(List<ICanBattle> battleEntities)                                                                //开始战斗方法
    {
        //保持协程单例
        if (_battleCoroutine == null)
        {
            //传入数据
            _startBattlePara = battleEntities;

            //启动战斗流程
            Battle();
        }
    }

    private void Battle()                                                                                                   //私有，仅组织战斗事件流程
    {
        //开始战斗事件
        BattleStartEvent?.Invoke();
        //开始战斗循环协程
        _battleCoroutine = StartCoroutine(BattleLoop());
        //结束战斗事件
        BattleEndEvent?.Invoke();
    }

    private bool IsBattleEnd()                                                                                              //判断战斗是否结束
    {
        return false;
    }

    private IEnumerator BattleLoop()                                                                                        //战斗循环
    {
        //判断战斗是否结束，循环
        while (!IsBattleEnd())
        {
            CombatantOnTurn = _initiativeQueue.Dequeue();

            NextTurnEvent?.Invoke();

            yield return CombatantOnTurn.TurnLoop();

            
        }
        yield return null;
    }

}
























/*
using System.Collections.Generic;
using UnityEngine;
// This class manages the turn logic.
// It handles the start of the battle, calculates initiative, and manages turns


The logic are as follows:

StartBattle calculates the initiative of the entities and starts the turn handling.
It calls TickTurn to set the first entity on turn.
TickTurn set up flag about the current entity in the class as EntityonTurn,
as well as the entity on turn as the isOnTurn flag.
Any other logic based on the turn system shall get its data from these two flags, and the entitiesOnBattle list.
When an entity ends its turn, it calls EndTurn,
which sets the endTurnExecuted flag to true.
This flag is checked in the Update method to determine if the turn should be ticked.
So at the very next frame, TickTurn is called to proceed to the next entity.

*/
/*
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Entity EntityonTurn;
    public List<ICanBattle> entitiesOnBattle = new List<ICanBattle>();
    public bool isBattleActive = false;
    public bool endTurnExecuted = false;

    public void StartBattle(List<ICanBattle> entities)
    {
        entitiesOnBattle = CalculateInitiative(entities);
        isBattleActive = true;
        TickTurn();
    }
    private List<ICanBattle> CalculateInitiative(List<ICanBattle> entities)
    {
        Debug.Log("Initiative not implemented");
        return entities;
    }
    // The tick turn method is called to handle the turn logic
    // It is called when the battle starts and when an entity ends its turn
    private void TickTurn()
    {
        // This checks if it is called at the start of the battle
        // if EntityonTurn is null, it means the battle has just started
        // Otherwise, we can pop the current entity from the list
        // and add it back to the end, as it just ended its turn
        if (EntityonTurn != null && EntityonTurn == entitiesOnBattle[0])
        {
            entitiesOnBattle.RemoveAt(0);
            entitiesOnBattle.Add(EntityonTurn);
        }
        // This sets the new entity on turn
        EntityonTurn = entitiesOnBattle[0];
        // and set the isOnTurn flag to true for the current entity
        EntityonTurn.isOnTurn = true;

        Debug.Log($"It's {EntityonTurn.name}'s turn.");
    }

    // Since no coroutine is used, we'll have to check the turn state every frame
    // This is done in the Update method
    // When a entity ends its turn, it will call EndTurn method, setting up the endTurnExecuted flag
    // This will allow us to check if the current entity has completed its action
    // and call TickTurn to proceed to the next entity
    void Update()
    {
        // When battle prerequisites are not met, we stop the battle
        if (entitiesOnBattle == null || entitiesOnBattle.Count == 0 || !isBattleActive)
        {
            Debug.LogWarning("No entities in battle.");
            EntityonTurn = null;
            isBattleActive = false;
            return;
        }
        if (isBattleActive && EntityonTurn != null && endTurnExecuted)
        {
            endTurnExecuted = false;
            TickTurn();
        }
    }

    public void EndTurn(Entity entity)
    {
        // The logic here prevents the endTurnExecuted flag from being set multiple times
        // or being called by entities that are not currently on turn
        if (EntityonTurn != null && EntityonTurn == entity && !endTurnExecuted)
        {
            endTurnExecuted = true;
            entity.isOnTurn = false;
        }
    }
}
*/