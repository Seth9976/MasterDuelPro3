using System;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000159 RID: 345
	public class SourceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the display name and the default value for the source switch.</summary>
		/// <param name="displayName">The name of the source switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch. </param>
		// Token: 0x06000801 RID: 2049 RVA: 0x0002BF1C File Offset: 0x0002A11C
		public SourceSwitch(string displayName, string defaultSwitchValue)
			: base(displayName, string.Empty, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the level of the switch.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.SourceLevels" /> values that represents the event level of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002BF2B File Offset: 0x0002A12B
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002BF33 File Offset: 0x0002A133
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Invoked when the value of the <see cref="P:System.Diagnostics.Switch.Value" /> property changes.</summary>
		/// <exception cref="T:System.ArgumentException">The new value of <see cref="P:System.Diagnostics.Switch.Value" /> is not one of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</exception>
		// Token: 0x06000804 RID: 2052 RVA: 0x0002BF3C File Offset: 0x0002A13C
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}
	}
}
