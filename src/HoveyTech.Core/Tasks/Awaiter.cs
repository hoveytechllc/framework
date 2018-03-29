#if NETSTANDARD1_1 || NET451
using System;
using System.Threading.Tasks;

namespace HoveyTech.Core.Tasks
{
    public class Awaiter
    {
        public TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        public void WaitForCondition(Func<bool> condition, TimeSpan? timeout = null)
        {
            timeout = timeout ?? DefaultTimeout;

            Task.WaitAll(new[]{Task.Run(async () =>
            {
                while (!condition())
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
            })}, timeout.Value);
        }
    }
}
#endif