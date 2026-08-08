using System.Text;

namespace AigioLTemplate.UnitTest;

sealed class Program
{
    /// <summary>
    /// 伪入口点，在测试项目中实现类似控制台程序的入口点行为，非静态函数避免被识别为入口点冲突
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    internal int Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        LogInit.InitLog("AigioLTemplate.Server.UnitTest");

        return 0;
    }
}