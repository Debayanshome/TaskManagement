using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Shared.Web.Models
{
    public class ErrorMessage
    {
        //
        // Summary:
        //     Gets or sets the field for this error.       
        public string Field { get; set; }

        //
        // Summary:
        //     Gets or sets the description for this error.                                                                                   
        public List<string> Description { get; set; }
    }
}
