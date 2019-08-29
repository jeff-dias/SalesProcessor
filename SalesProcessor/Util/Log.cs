using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcessor.Util
{
    public class Log
    {
        private readonly string _file;
        private readonly string _folder;

        public Log()
        {
            _folder = $@"{Environment.CurrentDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "").Replace(@"\bin\HML", "")}\Log";
            _file = $@"{_folder}\{GetDateTimeBrazil().ToString("yyyyMMddHH")}.txt";

            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            if (!File.Exists(_file))
                File.Create(_file).Close();

        }

        public void Info(string input)
        {
            File.AppendAllText(_file, $"{GetDateTimeBrazil().ToString()} : {input} \r\n");
        }

        public void Error(string input)
        {
            File.AppendAllText(_file, $"{GetDateTimeBrazil().ToString()} : #BUSINESS_ERROR {input} \r\n");
        }

        public void Error(Exception ex)
        {
            var innerException = ex.InnerException;
            var msg = ex.Message;
            while (innerException != null)
            {
                msg += $" => {innerException.Message}";
                innerException = innerException.InnerException;
            }

            if (!string.IsNullOrEmpty(ex.StackTrace))
                msg += $" => {ex.StackTrace}";

            File.AppendAllText(_file, $"{GetDateTimeBrazil().ToString()} : #EXCEPTION_ERROR {msg} \r\n");

            //SendEmailLog(msg, ex);
        }

        public string ReturnException(Exception ex)
        {
            var innerException = ex.InnerException;
            var msg = ex.Message;
            while (innerException != null)
            {
                msg += $" => {innerException.Message}";
                innerException = innerException.InnerException;
            }

            if (!string.IsNullOrEmpty(ex.StackTrace))
                msg += $" => {ex.StackTrace}";

            return msg;
        }

        internal DateTime GetDateTimeBrazil()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

    }
}
