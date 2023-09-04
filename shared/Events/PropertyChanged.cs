namespace shared.Events
{
    public abstract class PropertyChanged<T>
    {
        public Guid EntityId { get; set; }
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }
}
