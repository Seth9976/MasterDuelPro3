using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects.</summary>
	// Token: 0x02000023 RID: 35
	[Serializable]
	public sealed class ConfigurationSectionGroupCollection : NameObjectCollectionBase
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00005BCE File Offset: 0x00003DCE
		internal ConfigurationSectionGroupCollection(Configuration config, SectionGroupInfo group)
			: base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the names of all section groups in this collection.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005BE9 File Offset: 0x00003DE9
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.group.Groups.Keys;
			}
		}

		/// <summary>Gets the number of section groups in the collection.</summary>
		/// <returns>An integer that represents the number of section groups in the collection.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005BFB File Offset: 0x00003DFB
		public override int Count
		{
			get
			{
				return this.group.Groups.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class. </returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned. </param>
		// Token: 0x1700005A RID: 90
		public ConfigurationSectionGroup this[string name]
		{
			get
			{
				ConfigurationSectionGroup configurationSectionGroup = base.BaseGet(name) as ConfigurationSectionGroup;
				if (configurationSectionGroup == null)
				{
					SectionGroupInfo sectionGroupInfo = this.group.Groups[name] as SectionGroupInfo;
					if (sectionGroupInfo == null)
					{
						return null;
					}
					configurationSectionGroup = this.config.GetSectionGroupInstance(sectionGroupInfo);
					base.BaseSet(name, configurationSectionGroup);
				}
				return configurationSectionGroup;
			}
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</returns>
		// Token: 0x0600011A RID: 282 RVA: 0x00005C5F File Offset: 0x00003E5F
		public override IEnumerator GetEnumerator()
		{
			return this.group.Groups.AllKeys.GetEnumerator();
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600011B RID: 283 RVA: 0x000059C3 File Offset: 0x00003BC3
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000059D6 File Offset: 0x00003BD6
		internal ConfigurationSectionGroupCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400008E RID: 142
		private SectionGroupInfo group;

		// Token: 0x0400008F RID: 143
		private Configuration config;
	}
}
