using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Entities
{
    public class StandardList : BaseEntity
    {
        public string StandardListName { get; set; }
        public string DefaultWordLanguage { get; set; }
        public string DefaultSentenceLanguage { get; set; }

        private readonly List<StandardListItem> _standardListItems = new List<StandardListItem>();
        public IReadOnlyCollection<StandardListItem> StandardListItems => _standardListItems.AsReadOnly();

        public void AddStandardListItem(StandardListItem standardListItem)
        {
            standardListItem.StandardList = this;
            standardListItem.StandardListId = this.Id;
            this._standardListItems.Add(standardListItem);
        }
    }

    public class StandardListItem : BaseEntity
    {
        public Guid StandardListId { get; set; }
        public StandardList StandardList { get; set; }

        public string Sentence { get; set; }
        public string Word { get; set; }
        public byte[] SpokenWordAsMp3 { get; set; }
        public byte[] SpokenSentenceAsMp3 { get; set; }
        public string WordLanguage { get; set; }
        public string SentenceLanguage { get; set; }
    }
}
