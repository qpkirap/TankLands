using System.Collections.Generic;
using Game.Configs;

namespace Game
{
    public class GameState
    {
        public BattleInfo BattleInfo { get; private set; }
        public readonly InputState InputState = new(); 

        public BattleInfo CreateBattle(IEnumerable<UnitData> player, IEnumerable<UnitData> enemy, int locationId, int maxEnemyCount)
        {
            BattleInfo = new BattleInfo(player, enemy, locationId, maxEnemyCount);

            return BattleInfo;
        }
    }
}