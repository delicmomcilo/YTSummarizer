using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTSummarizer.Auth.Security
{
    public interface ISecurity
    {
        string? GetUserIdFromAccessToken(HttpRequest req);
    }
}