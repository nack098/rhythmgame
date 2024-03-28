using Services.Interface;

namespace Services.Factory {
    public class DataServiceFactory {
        public static IDataService GetDataService(Location location) {
            switch (location) {
                case Location.FileSystem:
                    return new FileSystemService();
                case Location.Server:
                    throw new System.NotImplementedException();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}