using System;
using System.IO;
using System.Reflection;


public class Logger
{
    private string path = string.Empty;
    private string LoggerPath = string.Empty;
    public Logger(string className)
    {
        path = className;
    }
    public void Info(string logMessage)
    {
        LoggerPath = Path.GetDirectoryName("./");
        Console.WriteLine(LoggerPath);
        try
        {
            using (StreamWriter w = File.AppendText(LoggerPath + "\\" + "Logger.log"))
            {
                Log(logMessage, w, "INFO  ");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void Error(string logMessage)
    {
        LoggerPath = Path.GetDirectoryName("./");
        Console.WriteLine(LoggerPath);
        try
        {
            using (StreamWriter w = File.AppendText(LoggerPath + "\\" + "Logger.log"))
            {
                Log(logMessage, w, "ERROR  ");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void Log(string logMessage, TextWriter txtWriter, string level)
    {
        try
        {
            txtWriter.WriteLine("{0} {1} {2}  {3} - {4}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), level, path, logMessage);
        }
        catch (Exception ex)
        {
        }
    }


}
