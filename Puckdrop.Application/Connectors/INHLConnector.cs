using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuckDrop.Application.Connectors
{
    public interface INHLConnector
    {
        Task<HttpResponseMessage> GetTodayGame(int teamId);
    }
}
