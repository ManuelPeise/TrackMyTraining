using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Shared.Enums;

namespace Web.Core.Shared.Models
{
    public class DropdownItem
    {
        public DropdownItemTypeEnum ItemType { get; set; }
        public string? To { get; set; }
        public string? Label { get; set; }
        public Delegates.AsyncCallback? Callback { get; set; }

    }
}
