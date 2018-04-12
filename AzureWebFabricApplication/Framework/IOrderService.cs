using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Framework
{
    public interface IOrderService : IService
    {
        Task<int> GetOrderNumber();
    }
}
