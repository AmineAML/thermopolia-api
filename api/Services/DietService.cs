using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using api.Interfaces;

namespace api.Services
{
    public class DietService : IDietService
    {
        public DietsContent _diets;

        private readonly Random _random = new Random();

        public Diet _diet;

        public DietService()
        {
            
        }

        public async Task<Diet> GetDiet()
        {
            string fileName = $"{Directory.GetCurrentDirectory()}/diets.json";

            using FileStream openStream = File.OpenRead(fileName);

            _diets = await JsonSerializer.DeserializeAsync<DietsContent>(openStream);

            // Diets JSON file is in work in progress that's why include valid diet objects
            var diets = _diets.diets.Where(d => d.description != "");

            _diet = diets.Skip(_random.Next(_diets.diets.Length)).First();

            return _diet;
        }
    }
}