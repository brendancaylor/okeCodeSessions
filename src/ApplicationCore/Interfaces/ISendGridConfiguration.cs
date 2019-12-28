using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ISendGridConfiguration
    {
        string SendGridKey { get; set; }
        string SendGridUser { get; set; }
    }
}
