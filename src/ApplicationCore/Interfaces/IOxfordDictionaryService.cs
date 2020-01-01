using ApplicationCore.Projections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOxfordDictionaryService
    {
        Task<OxfordDictionaryResponse> GetDefinitionAsync(string word);
    }
}
