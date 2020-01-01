using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Projections
{
    public class WordItem
    {
        public string Word { get; set; }
        public string Sentence { get; set; }
        public string WordLanguage { get; set; }
        public string SentenceLanguage { get; set; }
    }

    public class WordOnly
    {
        public string Word { get; set; }
    }
}
