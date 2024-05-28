using AssignmentApp.Domain.Interfaces;
using AssignmentApp.Domain.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Infrastructure.Repository;

public class IntegrationRepository : IIntegrationRepository
{
    private readonly ILogger<IntegrationRepository> _logger;

    public IntegrationRepository(ILogger<IntegrationRepository> logger)
    {
        _logger = logger;
    }
    public async Task<IEnumerable<string>> GetModelsForMakeIdYear(int makeId, int modelYear)
    {
        var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{modelYear}?format=json");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return new List<string>();
            }
            var content = await response.Content.ReadAsStringAsync();
            var modelsForMakeIdYearResponse = JsonConvert.DeserializeObject<ModelsForMakeIdYearResponse>(content);
            var modelsName = modelsForMakeIdYearResponse.Results.Select(m => m.Model_Name).ToList();
            return modelsName;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "an error occurred while requesting the api");
            return new List<string>(); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "an error occurred");
            return new List<string>(); 
        }
       
    }
}
