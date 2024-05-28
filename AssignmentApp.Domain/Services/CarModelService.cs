using AssignmentApp.Domain.Interfaces;
using AssignmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Domain.Services;

public class CarModelService : ICarModelService
{
    private readonly IIntegrationRepository _integrationRepository;
    private readonly ICarMakeRepository _carMakeRepository;

    public CarModelService(IIntegrationRepository integrationRepository, ICarMakeRepository carMakeRepository)
    {
        _integrationRepository = integrationRepository;
        _carMakeRepository = carMakeRepository;
    }
    public async Task<IEnumerable<string>> GetModelsForMakeIdYear(int modelYear, string make)
    {
        var makeId = _carMakeRepository.GetMakeIdByMake(make);
        if (makeId == 0 || makeId == null)
        {
           return Enumerable.Empty<string>();
        }

        var models = await _integrationRepository.GetModelsForMakeIdYear((int)makeId, modelYear);
        return models;
    }
}
