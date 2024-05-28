using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Domain.Interfaces;

public interface ICarMakeRepository
{
    int? GetMakeIdByMake(string make);
}
