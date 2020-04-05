using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinHelper.Core
{
    public class DeviceHelper : IDeviceHelper
    {
        public Task BeginInvokeOnMainThreadAsync(Action action)
        {
            return Task.Run(() => Device.BeginInvokeOnMainThread(action));
        }
    }

    public interface IDeviceHelper
    {
        Task BeginInvokeOnMainThreadAsync(Action action);
    }
}
