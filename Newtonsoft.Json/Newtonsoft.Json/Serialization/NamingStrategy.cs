using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000134 RID: 308
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0002EA18 File Offset: 0x0002CC18
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0002EA20 File Offset: 0x0002CC20
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002EA29 File Offset: 0x0002CC29
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0002EA31 File Offset: 0x0002CC31
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0002EA3A File Offset: 0x0002CC3A
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0002EA42 File Offset: 0x0002CC42
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x06000981 RID: 2433 RVA: 0x0002EA4B File Offset: 0x0002CC4B
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002EA61 File Offset: 0x0002CC61
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002EA74 File Offset: 0x0002CC74
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000984 RID: 2436
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x06000985 RID: 2437 RVA: 0x0002EA88 File Offset: 0x0002CC88
		public override int GetHashCode()
		{
			return (((((base.GetType().GetHashCode() * 397) ^ this.ProcessDictionaryKeys.GetHashCode()) * 397) ^ this.ProcessExtensionDataNames.GetHashCode()) * 397) ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002EADF File Offset: 0x0002CCDF
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
