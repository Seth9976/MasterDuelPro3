using System;
using System.Configuration;
using System.Transactions.Configuration;

namespace System.Transactions
{
	/// <summary>Contains methods used for transaction management. This class cannot be inherited.</summary>
	// Token: 0x0200000F RID: 15
	public static class TransactionManager
	{
		/// <summary>Gets the default timeout interval for new transactions.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout interval for new transactions.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023A8 File Offset: 0x000005A8
		public static TimeSpan DefaultTimeout
		{
			get
			{
				if (TransactionManager.defaultSettings != null)
				{
					return TransactionManager.defaultSettings.Timeout;
				}
				return TransactionManager.defaultTimeout;
			}
		}

		// Token: 0x0400001B RID: 27
		private static DefaultSettingsSection defaultSettings = ConfigurationManager.GetSection("system.transactions/defaultSettings") as DefaultSettingsSection;

		// Token: 0x0400001C RID: 28
		private static MachineSettingsSection machineSettings = ConfigurationManager.GetSection("system.transactions/machineSettings") as MachineSettingsSection;

		// Token: 0x0400001D RID: 29
		private static TimeSpan defaultTimeout = new TimeSpan(0, 1, 0);

		// Token: 0x0400001E RID: 30
		private static TimeSpan maxTimeout = new TimeSpan(0, 10, 0);
	}
}
