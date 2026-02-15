using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	/// <summary>Represents an XML section in a configuration file that contains default values of a transaction. This class cannot be inherited.</summary>
	// Token: 0x02000013 RID: 19
	public class DefaultSettingsSection : ConfigurationSection
	{
		/// <summary>Gets or sets a default time after which a transaction times out.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object. The default property is 00:01:00. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt to set this property to negative values.</exception>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002445 File Offset: 0x00000645
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("timeout", DefaultValue = "00:01:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base["timeout"];
			}
		}
	}
}
