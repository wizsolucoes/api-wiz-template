namespace Wiz.Template.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        protected Entity(int? id)
        {
            Id = id ?? default;
        }
    }
}
