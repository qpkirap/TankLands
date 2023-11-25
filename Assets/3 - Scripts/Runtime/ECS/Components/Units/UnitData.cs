namespace Game.Ecs.Components
{
    public struct UnitData
    {
        public Configs.UnitData Data { get; private set; }
        
        public UnitData(Configs.UnitData data)
        {
            Data = data;
        }
    }
}