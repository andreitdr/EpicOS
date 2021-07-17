
namespace CosmosKernel1.Functions
{
    public class Conversion
    {

        public static long ToKB(long bytes)
        {
            return bytes / 1024;
        }

        public static long ToMB(long bytes)
        {
            return ToKB(bytes) / 1024;
        }

        public static long ToGB(long bytes)
        {
            return ToMB(bytes) / 1024;
        }

    }
}
