using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ILogger = BLLInterface.ILogger;

namespace BLLLogic
{
    public class ConcreteLogger:ILogger
    {
        private readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}
