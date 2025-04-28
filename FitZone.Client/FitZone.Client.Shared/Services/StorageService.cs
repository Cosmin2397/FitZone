using Microsoft.JSInterop;

namespace FitZone.Client.Shared.Services
{

    public class StorageService
    {
        private readonly IJSRuntime _js;

        public StorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetItemAsync(string key, string value)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task<string> GetItemAsync(string key)
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }

}
