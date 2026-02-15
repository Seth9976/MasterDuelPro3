using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to create an instance of a configuration element collection. This class cannot be inherited.</summary>
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ConfigurationCollectionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> class.</summary>
		/// <param name="itemType">The type of the property collection to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="itemType" /> is null.</exception>
		// Token: 0x06000035 RID: 53 RVA: 0x00002944 File Offset: 0x00000B44
		public ConfigurationCollectionAttribute(Type itemType)
		{
			this.itemType = itemType;
		}

		/// <summary>Gets or sets the name of the &lt;add&gt; configuration element.</summary>
		/// <returns>The name that substitutes the standard name "add" for the configuration item.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002974 File Offset: 0x00000B74
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000297C File Offset: 0x00000B7C
		public string AddItemName
		{
			get
			{
				return this.addItemName;
			}
			set
			{
				this.addItemName = value;
			}
		}

		/// <summary>Gets or sets the name for the &lt;clear&gt; configuration element.</summary>
		/// <returns>The name that replaces the standard name "clear" for the configuration item.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002985 File Offset: 0x00000B85
		public string ClearItemsName
		{
			get
			{
				return this.clearItemsName;
			}
		}

		/// <summary>Gets or sets the name for the &lt;remove&gt; configuration element.</summary>
		/// <returns>The name that replaces the standard name "remove" for the configuration element.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000298D File Offset: 0x00000B8D
		public string RemoveItemName
		{
			get
			{
				return this.removeItemName;
			}
		}

		/// <summary>Gets or sets the type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" />.</returns>
		// Token: 0x17000013 RID: 19
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002995 File Offset: 0x00000B95
		public ConfigurationElementCollectionType CollectionType
		{
			set
			{
				this.collectionType = value;
			}
		}

		// Token: 0x04000022 RID: 34
		private string addItemName = "add";

		// Token: 0x04000023 RID: 35
		private string clearItemsName = "clear";

		// Token: 0x04000024 RID: 36
		private string removeItemName = "remove";

		// Token: 0x04000025 RID: 37
		private ConfigurationElementCollectionType collectionType;

		// Token: 0x04000026 RID: 38
		private Type itemType;
	}
}
