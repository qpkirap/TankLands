using System.Collections.Generic;
using Game.Configs;

namespace Game
{
    public class BattleInfo
    {
        public IEnumerable<UnitData> EnemyDatas { get; private set; }
        public IEnumerable<UnitData> PlayerDatas { get; private set; }
        public int LocationId { get; private set; }
        public int MaxEnemyCount { get; private set; }
        
        public BattleInfo(
            IEnumerable<UnitData> playerDatas,
            IEnumerable<UnitData> enemyDatas,
            int locationId,
            int maxEnemyCount)
        {
            EnemyDatas = enemyDatas;
            PlayerDatas = playerDatas;
            LocationId = locationId;
            MaxEnemyCount = maxEnemyCount;
        }
    }
}