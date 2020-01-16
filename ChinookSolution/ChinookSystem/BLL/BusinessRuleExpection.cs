using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Web;
#endregion

namespace DMIT2018Common.UserControls
{
    /// <summary>
    /// Summary description for BusinessRuleException
    /// </summary>
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public List<string> RuleDetails { get; set; }
        public BusinessRuleException(string message, List<string> reasons)
            : base(message)
        {
            this.RuleDetails = reasons;
        }
    }

}
