using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200015D RID: 349
	internal abstract class Accessor
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x00002127 File Offset: 0x00000327
		internal Accessor()
		{
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00053C58 File Offset: 0x00051E58
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00053C60 File Offset: 0x00051E60
		internal TypeMapping Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00053C69 File Offset: 0x00051E69
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x00053C71 File Offset: 0x00051E71
		internal object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00053C7A File Offset: 0x00051E7A
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue != DBNull.Value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00053C96 File Offset: 0x00051E96
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00053CAC File Offset: 0x00051EAC
		internal virtual string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00053CB5 File Offset: 0x00051EB5
		// (set) Token: 0x06001103 RID: 4355 RVA: 0x00053CBD File Offset: 0x00051EBD
		internal bool Any
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00053CC6 File Offset: 0x00051EC6
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x00053CCE File Offset: 0x00051ECE
		internal string AnyNamespaces
		{
			get
			{
				return this.anyNs;
			}
			set
			{
				this.anyNs = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00053CD7 File Offset: 0x00051ED7
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x00053CDF File Offset: 0x00051EDF
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00053CE8 File Offset: 0x00051EE8
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x00053CF0 File Offset: 0x00051EF0
		internal XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00053CF9 File Offset: 0x00051EF9
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x00053D01 File Offset: 0x00051F01
		internal bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00053D0A File Offset: 0x00051F0A
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x00053D12 File Offset: 0x00051F12
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
			set
			{
				this.isOptional = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00053D1B File Offset: 0x00051F1B
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00053D23 File Offset: 0x00051F23
		internal bool IsTopLevelInSchema
		{
			get
			{
				return this.topLevelInSchema;
			}
			set
			{
				this.topLevelInSchema = value;
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00053D2C File Offset: 0x00051F2C
		internal static string EscapeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00053D44 File Offset: 0x00051F44
		internal static string EscapeQName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			int num = name.LastIndexOf(':');
			if (num < 0)
			{
				return XmlConvert.EncodeLocalName(name);
			}
			if (num == 0 || num == name.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }), "name");
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(name.Substring(num + 1)), XmlConvert.EncodeLocalName(name.Substring(0, num))).ToString();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00053DC4 File Offset: 0x00051FC4
		internal static string UnescapeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00053DCC File Offset: 0x00051FCC
		internal string ToString(string defaultNs)
		{
			if (this.Any)
			{
				return ((this.Namespace == null) ? "##any" : this.Namespace) + ":" + this.Name;
			}
			if (!(this.Namespace == defaultNs))
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x0400082D RID: 2093
		private string name;

		// Token: 0x0400082E RID: 2094
		private object defaultValue;

		// Token: 0x0400082F RID: 2095
		private string ns;

		// Token: 0x04000830 RID: 2096
		private TypeMapping mapping;

		// Token: 0x04000831 RID: 2097
		private bool any;

		// Token: 0x04000832 RID: 2098
		private string anyNs;

		// Token: 0x04000833 RID: 2099
		private bool topLevelInSchema;

		// Token: 0x04000834 RID: 2100
		private bool isFixed;

		// Token: 0x04000835 RID: 2101
		private bool isOptional;

		// Token: 0x04000836 RID: 2102
		private XmlSchemaForm form;
	}
}
