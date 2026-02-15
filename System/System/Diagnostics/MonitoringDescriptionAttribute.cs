using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies a description for a property or event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
		/// <param name="description">The application-defined description text. </param>
		// Token: 0x06000984 RID: 2436 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public MonitoringDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets description text associated with the item monitored.</summary>
		/// <returns>An application-defined description.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00032204 File Offset: 0x00030404
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
