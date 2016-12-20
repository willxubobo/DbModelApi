using System.Collections.Generic;
using System.Text;
using NET.Framework.Common.Extensions;

namespace NET.Framework.Common.ValidHelper
{
    public class ValidManager
    {
        private readonly List<string> _errorMessages = new List<string>();

        public ValidManager(params string[] validItems)
        {
            foreach (string item in validItems)
            {
                if (item.IsNotEmpty())
                {
                    _errorMessages.Add(item);
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ErrorMessages.Count == 0;
            }
        }

        public List<string> ErrorMessages
        {
            get { return _errorMessages; }
        }

        public string ToOutPut()
        {
            var sb = new StringBuilder();
            foreach (string item in ErrorMessages)
            {
                sb.AppendLine(item);
            }
            return sb.ToString();
        }
    }
}