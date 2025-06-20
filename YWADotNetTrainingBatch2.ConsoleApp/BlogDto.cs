using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWADotNetTrainingBatch2.ConsoleApp
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = string.Empty;
        public string BlogAuthor { get; set; } = string.Empty;
        public string BlogContent { get; set; } = string.Empty;
    }
}
