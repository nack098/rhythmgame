
namespace Services.Interface {
    public interface IDataService {
        public T GetData<T>(string path);
        public void Save<T>(T data, string path);
    }
}