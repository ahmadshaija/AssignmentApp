using AssignmentApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Infrastructure.Repository;

public class CarMakeRepository : ICarMakeRepository
{
    private readonly Dictionary<string, int> _carMakes;
    private readonly ILogger<CarMakeRepository> _logger;

    public CarMakeRepository(ILogger<CarMakeRepository> logger)
    {
        _logger = logger;

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "Resources", "Excel", "CarMake.csv");
        if (!File.Exists(filePath))
        {
            _logger.LogError("exel file not exist");
            throw new FileNotFoundException($"The file at path {filePath} was not found.");
        }
        _carMakes = LoadCarMakesFromCsv(filePath);
    }

    private Dictionary<string, int> LoadCarMakesFromCsv(string filePath)
    {
        var carMakes = new Dictionary<string, int>();
        int rowCount = 0;

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                var headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length >= 2)
                    {
                        var makeId = int.Parse(values[0].Trim());
                        var makeName = values[1].Trim().Trim('"').ToUpper();

                        carMakes[makeName] = makeId;
                        rowCount++;
                    }

                }
            }
            return carMakes;
        }
        catch (Exception ex)
        {

            _logger.LogError("error while reading excel file");
            return new Dictionary<string, int>();
        }

    }

    public int? GetMakeIdByMake(string make)
    {
        var carMake = make.ToUpper();
        _carMakes.TryGetValue(carMake, out var makeId);
        return makeId;
    }
}
