using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sol_Demo
{
    class Program
    {
        static async  Task  Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            await using (var demoObj=new Demo())
            {
                string content = await demoObj?.ReadTextFileAsync();
                Console.WriteLine(content);
            }
        }
    }

    public class Demo : IAsyncDisposable
    {
        private FileStream fileStream = null;

        public Task<String> ReadTextFileAsync()
        {
            string content = null;

            fileStream = File.OpenRead(@"D:/TestDemo.txt");
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            while (fileStream.Read(b, 0, b.Length) > 0)
            {
                content= temp.GetString(b);
            }

            return Task.FromResult<String>(content);

        }

        public ValueTask DisposeAsync()
        {
            fileStream.Close();
            fileStream.DisposeAsync();
            fileStream = null;
            GC.SuppressFinalize(this);

            return default;
        }

    }
}
