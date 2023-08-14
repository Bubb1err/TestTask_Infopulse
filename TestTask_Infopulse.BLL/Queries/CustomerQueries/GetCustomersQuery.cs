using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.CustomerQueries
{
    public class GetCustomersQuery : IRequest<List<CustomerDTO>>
    {
    }
}
