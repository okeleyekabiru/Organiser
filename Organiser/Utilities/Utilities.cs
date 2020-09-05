using Newtonsoft.Json;


namespace Organiser.Utilities
{
    public static class Utilities
    {
    public static  string SerializeSettings<T>(this  T data)
    {
       return JsonConvert.SerializeObject(data);
    }
    public static T DeserializeSettings<T>(this string data)
    {
       return JsonConvert.DeserializeObject<T>(data);
    }

    }
}
