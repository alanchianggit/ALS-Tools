using Entity;

namespace DAL
{
    public interface IEventsDAL
    {
        void Add(EventEntity obj);
        void Update(EventEntity obj);
    }
}