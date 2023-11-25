using System;

namespace Game.Configs
{
    public interface IEntity : IEquatable<IEntity>
    {
        string Id { get; }
        void GenerateId(bool force = false);
    }
}