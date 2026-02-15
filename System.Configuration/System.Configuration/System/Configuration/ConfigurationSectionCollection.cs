using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of related sections within a configuration file.</summary>
	// Token: 0x02000020 RID: 32
	[Serializable]
	public sealed class ConfigurationSectionCollection : NameObjectCollectionBase
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000058F0 File Offset: 0x00003AF0
		internal ConfigurationSectionCollection(Configuration config, SectionGroupInfo group)
			: base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSection" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the keys of all sections in this collection.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000590B File Offset: 0x00003B0B
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.group.Sections.Keys;
			}
		}

		/// <summary>Gets the number of sections in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An integer that represents the number of sections in the collection.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000591D File Offset: 0x00003B1D
		public override int Count
		{
			get
			{
				return this.group.Sections.Count;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned. </param>
		// Token: 0x17000052 RID: 82
		public ConfigurationSection this[string name]
		{
			get
			{
				ConfigurationSection configurationSection = base.BaseGet(name) as ConfigurationSection;
				if (configurationSection == null)
				{
					SectionInfo sectionInfo = this.group.Sections[name] as SectionInfo;
					if (sectionInfo == null)
					{
						return null;
					}
					configurationSection = this.config.GetSectionInstance(sectionInfo, true);
					if (configurationSection == null)
					{
						return null;
					}
					object obj = ConfigurationSectionCollection.lockObject;
					lock (obj)
					{
						base.BaseSet(name, configurationSection);
					}
				}
				return configurationSection;
			}
		}

		/// <summary>Gets an enumerator that can iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</returns>
		// Token: 0x06000106 RID: 262 RVA: 0x000059B4 File Offset: 0x00003BB4
		public override IEnumerator GetEnumerator()
		{
			foreach (object obj in this.group.Sections.AllKeys)
			{
				string text = (string)obj;
				yield return this[text];
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06000107 RID: 263 RVA: 0x000059C3 File Offset: 0x00003BC3
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000059D6 File Offset: 0x00003BD6
		internal ConfigurationSectionCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000082 RID: 130
		private SectionGroupInfo group;

		// Token: 0x04000083 RID: 131
		private Configuration config;

		// Token: 0x04000084 RID: 132
		private static readonly object lockObject = new object();
	}
}
