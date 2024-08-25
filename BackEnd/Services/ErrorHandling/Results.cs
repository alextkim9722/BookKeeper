using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Runtime.CompilerServices;

namespace BackEnd.Services.ErrorHandling
{
    public class Results<T>
    {
        public T? payload { get; set; } = default;
        public string msg { get; set; } = string.Empty;
        public bool success { get; set; } = false;

        public Results()
        {
            //empty
        }

        protected void successfulResult(T? payload)
        {
            this.payload = payload;
            msg = string.Empty;
            success = true;
        }

        protected void failedResult(string msg)
        {
            payload = default;
            appendErrorMessage(msg);
            success = false;
        }

        private void appendErrorMessage(string msg)
            => this.msg += "[ERROR]: " + msg + Environment.NewLine;
    }
}
