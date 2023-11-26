namespace Game.Ecs.Components
{
    public struct Skill
    {
        public SkillType SkillType { get; private set; }
        
        public Skill(SkillType skillType)
        {
            SkillType = skillType;
        }
    }
}