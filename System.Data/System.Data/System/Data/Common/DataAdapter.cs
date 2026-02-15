using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a set of SQL commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000E6 RID: 230
	public class DataAdapter : Component
	{
		/// <summary>Determines the action to take when incoming data does not have a matching table or column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00043045 File Offset: 0x00041245
		[DefaultValue(MissingMappingAction.Passthrough)]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
		}

		// Token: 0x040004EC RID: 1260
		private static readonly object s_eventFillError = new object();

		// Token: 0x040004ED RID: 1261
		private MissingMappingAction _missingMappingAction;
	}
}
