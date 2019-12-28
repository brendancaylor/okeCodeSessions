using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class SendGridConfiguration : ISendGridConfiguration
    {
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
    }
}
