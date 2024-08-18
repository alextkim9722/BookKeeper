using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Runtime.CompilerServices;

namespace BackEnd.Services.ErrorHandling
{
	public class Results<T>
	{
		public T? payload { get; set; } = default(T?);
		public string msg { get; set; } = string.Empty;
		public bool success { get; set; } = false;

		public Results()
		{
			//empty
		}

		protected void successfulResult(T? payload)
		{
			this.payload = payload;
			this.msg = string.Empty;
			this.success = true;
		}

		protected void failedResult(string msg)
		{
			this.payload = default(T?);
			appendErrorMessage(msg);
			this.success = false;
		}

		private void appendErrorMessage(string msg)
			=> this.msg += ("[ERROR]: " + msg + "\n");
	}
}
