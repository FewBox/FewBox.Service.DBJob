using FewBox.Core.Utility.Formatter;

namespace FewBox.Service.DBJob
{
    public class SqlScript
    {
        public string Value { get; set; }
        public string Script
        {
            get
            {
                return Base64Utility.Deserialize(this.Value);
            }
        }
    }
}