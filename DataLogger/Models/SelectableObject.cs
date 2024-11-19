
namespace DataLogger.Models
{
    public class SelectableObject<T>
    {
        public T Object { get; set; }
        public bool IsSelected { get; set; }

        public SelectableObject(T obj, bool isSelected) 
        {
            Object = obj;
            IsSelected = isSelected;
        }
    }
}
