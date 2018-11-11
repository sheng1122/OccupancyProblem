using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class PageHeader
    {
        public string Heading { get; set; }
        public List<Link> Breadcrumb { get; set; } = new List<Link>();
    }

    public class Link
    {
        public string StatusClass { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
    }
}
