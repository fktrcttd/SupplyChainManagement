namespace SCM.Core
{
    public interface ILogger
    {
        void Write(string category, string message);
    }
}