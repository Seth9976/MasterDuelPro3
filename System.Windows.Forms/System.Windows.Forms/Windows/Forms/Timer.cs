using System;
using System.ComponentModel;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Implements a timer that raises an event at user-defined intervals. This timer is optimized for use in Windows Forms applications and must be used in a window.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AD RID: 429
	[DefaultProperty("Interval")]
	[DefaultEvent("Tick")]
	[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
	public class Timer : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class.</summary>
		// Token: 0x060011F3 RID: 4595 RVA: 0x0005D286 File Offset: 0x0005B486
		public Timer()
		{
			this.enabled = false;
		}

		/// <summary>Gets or sets whether the timer is running.</summary>
		/// <returns>true if the timer is currently enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0005D29D File Offset: 0x0005B49D
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
		[DefaultValue(false)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (value != this.enabled)
				{
					this.enabled = value;
					if (value)
					{
						this.expires = DateTime.UtcNow.AddMilliseconds((double)((this.interval > Timer.Minimum) ? this.interval : Timer.Minimum));
						this.thread = Thread.CurrentThread;
						XplatUI.SetTimer(this);
						return;
					}
					XplatUI.KillTimer(this);
					this.thread = null;
				}
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <returns>An <see cref="T:System.Int32" /> specifying the number of milliseconds before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event. The value cannot be less than one.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0005D315 File Offset: 0x0005B515
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x0005D320 File Offset: 0x0005B520
		[DefaultValue(100)]
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("Interval", string.Format("'{0}' is not a valid value for Interval. Interval must be greater than 0.", value));
				}
				if (this.interval == value)
				{
					return;
				}
				this.interval = value;
				this.expires = DateTime.UtcNow.AddMilliseconds((double)((this.interval > Timer.Minimum) ? this.interval : Timer.Minimum));
				if (this.enabled)
				{
					XplatUI.KillTimer(this);
					XplatUI.SetTimer(this);
				}
			}
		}

		/// <summary>Starts the timer.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011F8 RID: 4600 RVA: 0x0005D39F File Offset: 0x0005B59F
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops the timer.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011F9 RID: 4601 RVA: 0x0005D3A8 File Offset: 0x0005B5A8
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		internal DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		/// <summary>Occurs when the specified timer interval has elapsed and the timer is enabled.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060011FB RID: 4603 RVA: 0x0005D3BC File Offset: 0x0005B5BC
		// (remove) Token: 0x060011FC RID: 4604 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
		public event EventHandler Tick;

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Timer" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Timer" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FD RID: 4605 RVA: 0x0005D429 File Offset: 0x0005B629
		public override string ToString()
		{
			return base.ToString() + ", Interval: " + this.Interval;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0005D446 File Offset: 0x0005B646
		internal void Update(DateTime update)
		{
			this.expires = update.AddMilliseconds((double)((this.interval > Timer.Minimum) ? this.interval : Timer.Minimum));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0005D470 File Offset: 0x0005B670
		internal void FireTick()
		{
			this.OnTick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. This is always <see cref="F:System.EventArgs.Empty" />. </param>
		// Token: 0x06001200 RID: 4608 RVA: 0x0005D47D File Offset: 0x0005B67D
		protected virtual void OnTick(EventArgs e)
		{
			if (this.Tick != null)
			{
				this.Tick(this, e);
			}
		}

		/// <summary>Disposes of the resources, other than memory, used by the timer.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources. false to release only the unmanaged resources.</param>
		// Token: 0x06001201 RID: 4609 RVA: 0x0005D494 File Offset: 0x0005B694
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.Enabled = false;
		}

		// Token: 0x04000B44 RID: 2884
		private bool enabled;

		// Token: 0x04000B45 RID: 2885
		private int interval = 100;

		// Token: 0x04000B46 RID: 2886
		private DateTime expires;

		// Token: 0x04000B47 RID: 2887
		internal Thread thread;

		// Token: 0x04000B48 RID: 2888
		internal bool Busy;

		// Token: 0x04000B49 RID: 2889
		internal IntPtr window;

		// Token: 0x04000B4A RID: 2890
		internal static readonly int Minimum = 15;
	}
}
