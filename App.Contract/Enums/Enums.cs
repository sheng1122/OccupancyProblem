using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.Contract.Enums
{
    public enum Status : int
    {
        Disabled = 0,
        Enabled = 1
    }
    
    public enum OrderDirection : int
    {
        Asc = 1,
        Desc = 2
    }
}
