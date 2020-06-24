namespace Qmos.Entities.Abstractions
{
    public abstract class EntityBase<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
