namespace TaskManager.Data.Repositories
{
    public interface IRepository<Entity>
    {
        Entity Create(Entity entity);
    }
}