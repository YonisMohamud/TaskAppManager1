using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskAppManager1
{
    public class JsonDataService
    {
        private readonly string _filePath;

        public JsonDataService(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Skapar en tom JSON-fil om den saknas
            }
        }

        public void SaveData(List<Task> tasks)
        {
            try
            {
                var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparning: {ex.Message}");
            }
        }

        public List<Task> LoadData()
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid laddning: {ex.Message}");
                return new List<Task>();
            }
        }
    }
}