using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Domain.Interfaces;

public interface IIntegrationRepository
{
    Task<IEnumerable<string>> GetModelsForMakeIdYear(int makeId, int modelYear);
}
