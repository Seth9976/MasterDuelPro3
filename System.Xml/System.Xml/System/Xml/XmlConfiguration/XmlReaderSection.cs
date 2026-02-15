using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	/// <summary>Represents an XML reader section.</summary>
	// Token: 0x02000201 RID: 513
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlReaderSection : ConfigurationSection
	{
		/// <summary>Gets or sets the string that represents the prohibit default resolver.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the prohibit default resolver.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00097C3A File Offset: 0x00095E3A
		[ConfigurationProperty("prohibitDefaultResolver", DefaultValue = "false")]
		public string ProhibitDefaultResolverString
		{
			get
			{
				return (string)base["prohibitDefaultResolver"];
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00097C4C File Offset: 0x00095E4C
		private bool _ProhibitDefaultResolver
		{
			get
			{
				bool flag;
				XmlConvert.TryToBoolean(this.ProhibitDefaultResolverString, out flag);
				return flag;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00097C68 File Offset: 0x00095E68
		internal static bool ProhibitDefaultUrlResolver
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00097C90 File Offset: 0x00095E90
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver)
			{
				return null;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00097CA0 File Offset: 0x00095EA0
		[ConfigurationProperty("CollapseWhiteSpaceIntoEmptyString", DefaultValue = "false")]
		public string CollapseWhiteSpaceIntoEmptyStringString
		{
			get
			{
				return (string)base["CollapseWhiteSpaceIntoEmptyString"];
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00097CB4 File Offset: 0x00095EB4
		private bool _CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				bool flag;
				XmlConvert.TryToBoolean(this.CollapseWhiteSpaceIntoEmptyStringString, out flag);
				return flag;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00097CD0 File Offset: 0x00095ED0
		internal static bool CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._CollapseWhiteSpaceIntoEmptyString;
			}
		}
	}
}
