using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015A RID: 346
	public abstract class Switch
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0002BF60 File Offset: 0x0002A160
		private object IntializedLock
		{
			get
			{
				if (this.m_intializedLock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref this.m_intializedLock, obj, null);
				}
				return this.m_intializedLock;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description for the switch. </param>
		// Token: 0x06000806 RID: 2054 RVA: 0x0002BF8F File Offset: 0x0002A18F
		protected Switch(string displayName, string description)
			: this(displayName, description, "0")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch. </summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06000807 RID: 2055 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002C020 File Offset: 0x0002A220
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						if ((Switch)Switch.switches[i].Target != null)
						{
							list2.Add(Switch.switches[i]);
						}
					}
					if (list2.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list2);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002C0FC File Offset: 0x0002A2FC
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0002C11C File Offset: 0x0002A31C
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is null.-or-The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.-or-The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002C17C File Offset: 0x0002A37C
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0002C18C File Offset: 0x0002A38C
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex);
				}
				catch (FormatException ex2)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex2);
				}
				catch (OverflowException ex3)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex3);
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002C23C File Offset: 0x0002A43C
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002C248 File Offset: 0x0002A448
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or null if there no custom attributes are supported.</returns>
		// Token: 0x06000811 RID: 2065 RVA: 0x000027B6 File Offset: 0x000009B6
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x06000812 RID: 2066 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void OnSwitchSettingChanged()
		{
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x06000813 RID: 2067 RVA: 0x0002C3E1 File Offset: 0x0002A5E1
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000629 RID: 1577
		private SwitchElementsCollection switchSettings;

		// Token: 0x0400062A RID: 1578
		private readonly string description;

		// Token: 0x0400062B RID: 1579
		private readonly string displayName;

		// Token: 0x0400062C RID: 1580
		private int switchSetting;

		// Token: 0x0400062D RID: 1581
		private volatile bool initialized;

		// Token: 0x0400062E RID: 1582
		private bool initializing;

		// Token: 0x0400062F RID: 1583
		private volatile string switchValueString = string.Empty;

		// Token: 0x04000630 RID: 1584
		private StringDictionary attributes;

		// Token: 0x04000631 RID: 1585
		private string defaultValue;

		// Token: 0x04000632 RID: 1586
		private object m_intializedLock;

		// Token: 0x04000633 RID: 1587
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x04000634 RID: 1588
		private static int s_LastCollectionCount;
	}
}
