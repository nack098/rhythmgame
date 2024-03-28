using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Services.Interface;

namespace Services {
    public class FileSystemService: IDataService {
        public T GetData<T>(string path) {
            if (typeof(T) == typeof(string)) {
                return (T)(object)File.ReadAllText(path);
            }

            if (typeof(T) == typeof(byte[])) {
                return (T)(object)File.ReadAllBytes(path);
            }

            byte[] data = File.ReadAllBytes(path);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data)) {
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
        public void Save<T>(T data, string path) {
            throw new System.NotImplementedException();
        }
    }
}