using AssignmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Domain.Interfaces;

public interface ICarModelService
{
    Task<IEnumerable<string>> GetModelsForMakeIdYear( int modelYear, string make);
}
