﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Projections
{

    public class OxfordDictionaryResponse
    {
        public string id { get; set; }
        public Metadata metadata { get; set; }
        public List<Result> results { get; set; } = new List<Result>();
        public string word { get; set; }
    }

    public class Metadata
    {
        public string operation { get; set; }
        public string provider { get; set; }
        public string schema { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string language { get; set; }
        public Lexicalentry[] lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class Lexicalentry
    {
        public Entry[] entries { get; set; }
        public string language { get; set; }
        public Lexicalcategory lexicalCategory { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public string text { get; set; }
    }

    public class Lexicalcategory
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Entry
    {
        public string[] etymologies { get; set; }
        public Sens[] senses { get; set; }
    }

    public class Sens
    {
        public string[] definitions { get; set; }
        public Example[] examples { get; set; }
        public string id { get; set; }
        public string[] shortDefinitions { get; set; }
        public Subsens[] subsenses { get; set; }
        public Thesauruslink1[] thesaurusLinks { get; set; }
        public Construction1[] constructions { get; set; }
        public Domain[] domains { get; set; }
    }

    public class Example
    {
        public string text { get; set; }
        public Note[] notes { get; set; }
    }

    public class Note
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Subsens
    {
        public string[] definitions { get; set; }
        public Example1[] examples { get; set; }
        public string id { get; set; }
        public string[] shortDefinitions { get; set; }
        public Thesauruslink[] thesaurusLinks { get; set; }
        public Register[] registers { get; set; }
        public Construction[] constructions { get; set; }
        public Note1[] notes { get; set; }
    }

    public class Example1
    {
        public string text { get; set; }
    }

    public class Thesauruslink
    {
        public string entry_id { get; set; }
        public string sense_id { get; set; }
    }

    public class Register
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Construction
    {
        public string text { get; set; }
    }

    public class Note1
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Thesauruslink1
    {
        public string entry_id { get; set; }
        public string sense_id { get; set; }
    }

    public class Construction1
    {
        public string text { get; set; }
    }

    public class Domain
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Pronunciation
    {
        public string audioFile { get; set; }
        public string[] dialects { get; set; }
        public string phoneticNotation { get; set; }
        public string phoneticSpelling { get; set; }
    }

}