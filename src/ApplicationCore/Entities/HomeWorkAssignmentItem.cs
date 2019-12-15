using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class HomeWorkAssignmentItem : BaseEntityFull
    {
        public Guid HomeWorkAssignmentId { get; set; }
        public HomeWorkAssignment HomeWorkAssignment { get; set; }

        public string Sentence { get; set; }
        public string Word { get; set; }
        public byte[] SpokenWordAsMp3 { get; set; }
        public byte[] SpokenSentenceAsMp3 { get; set; }
        public string WordLanguage { get; set; }
        public string SentenceLanguage { get; set; }

        private readonly List<GoogleSpeechApiRequest> _googleSpeechApiRequests = new List<GoogleSpeechApiRequest>();
        public IReadOnlyCollection<GoogleSpeechApiRequest> GoogleSpeechApiRequests => _googleSpeechApiRequests.AsReadOnly();

        public void AddGoogleSpeechApiRequest()
        {
            var googleSpeechApiRequest = new GoogleSpeechApiRequest();
            googleSpeechApiRequest.WordCount = this.Word.Length;
            googleSpeechApiRequest.SentenceCount = this.Sentence.Length;
            this._googleSpeechApiRequests.Add(googleSpeechApiRequest);
        }

    }
}
