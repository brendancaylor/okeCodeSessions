using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Projections;
using System.Linq;

namespace Infrastructure.Services
{
    public class CsvUtils
    {
        public List<WordItem> ReadWordItemCsv(IFormFile file)
        {
            var result = new List<WordItem>();
            using (var csvReader = new CsvReader(new StreamReader(file.OpenReadStream())))
            {
                result = csvReader.GetRecords<WordItem>().ToList();
            }
            return result;
        }

        public List<WordOnly> ReadWordOnlyCsv(IFormFile file)
        {
            var result = new List<WordOnly>();
            using (var csvReader = new CsvReader(new StreamReader(file.OpenReadStream())))
            {
                result = csvReader.GetRecords<WordOnly>().ToList();
            }
            return result;
        }
    }
}
