using System;
using System.Collections.Specialized;

namespace System.Configuration.Provider
{
	/// <summary>Provides a base implementation for the extensible provider model.</summary>
	// Token: 0x02000046 RID: 70
	public abstract class ProviderBase
	{
		/// <summary>Initializes the provider.</summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
		/// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> on a provider after the provider has already been initialized.</exception>
		// Token: 0x060001D3 RID: 467 RVA: 0x00007B80 File Offset: 0x00005D80
		public virtual void Initialize(string name, NameValueCollection config)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Provider name cannot be null or empty.", "name");
			}
			if (this.alreadyInitialized)
			{
				throw new InvalidOperationException("This provider instance has already been initialized.");
			}
			this.alreadyInitialized = true;
			this._name = name;
			if (config != null)
			{
				this._description = config["description"];
				config.Remove("description");
			}
			if (string.IsNullOrEmpty(this._description))
			{
				this._description = this._name;
			}
		}

		/// <summary>Gets the friendly name used to refer to the provider during configuration.</summary>
		/// <returns>The friendly name used to refer to the provider during configuration.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007C0C File Offset: 0x00005E0C
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x040000EB RID: 235
		private bool alreadyInitialized;

		// Token: 0x040000EC RID: 236
		private string _description;

		// Token: 0x040000ED RID: 237
		private string _name;
	}
}
