using Microsoft.JSInterop;
using System.Diagnostics;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Shared.Services
{
    public class JwtTokenService: IJwtTokenService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string JWT = "jwt-token";

        public JwtTokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string?> GetToken()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", JWT);
            
                return token;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
