using System;

namespace System.Xml.Schema
{
	// Token: 0x0200028F RID: 655
	internal sealed class SchemaAttDef : SchemaDeclBase, IDtdDefaultAttributeInfo, IDtdAttributeInfo
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x000AB0FC File Offset: 0x000A92FC
		public SchemaAttDef(XmlQualifiedName name, string prefix)
			: base(name, prefix)
		{
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000AB106 File Offset: 0x000A9306
		public SchemaAttDef(XmlQualifiedName name)
			: base(name, null)
		{
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000AB110 File Offset: 0x000A9310
		private SchemaAttDef()
		{
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x000AB118 File Offset: 0x000A9318
		string IDtdAttributeInfo.Prefix
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x000AB120 File Offset: 0x000A9320
		string IDtdAttributeInfo.LocalName
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x000AB12D File Offset: 0x000A932D
		int IDtdAttributeInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x000AB135 File Offset: 0x000A9335
		int IDtdAttributeInfo.LinePosition
		{
			get
			{
				return this.LinePosition;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000AB13D File Offset: 0x000A933D
		bool IDtdAttributeInfo.IsNonCDataType
		{
			get
			{
				return this.TokenizedType > XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000AB148 File Offset: 0x000A9348
		bool IDtdAttributeInfo.IsDeclaredInExternal
		{
			get
			{
				return this.IsDeclaredInExternal;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x000AB150 File Offset: 0x000A9350
		bool IDtdAttributeInfo.IsXmlAttribute
		{
			get
			{
				return this.Reserved > SchemaAttDef.Reserve.None;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000AB15B File Offset: 0x000A935B
		string IDtdDefaultAttributeInfo.DefaultValueExpanded
		{
			get
			{
				return this.DefaultValueExpanded;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x000AB163 File Offset: 0x000A9363
		object IDtdDefaultAttributeInfo.DefaultValueTyped
		{
			get
			{
				return this.DefaultValueTyped;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000AB16B File Offset: 0x000A936B
		int IDtdDefaultAttributeInfo.ValueLineNumber
		{
			get
			{
				return this.ValueLineNumber;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000AB173 File Offset: 0x000A9373
		int IDtdDefaultAttributeInfo.ValueLinePosition
		{
			get
			{
				return this.ValueLinePosition;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x000AB17B File Offset: 0x000A937B
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x000AB183 File Offset: 0x000A9383
		internal int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x000AB18C File Offset: 0x000A938C
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x000AB194 File Offset: 0x000A9394
		internal int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000AB19D File Offset: 0x000A939D
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x000AB1A5 File Offset: 0x000A93A5
		internal int ValueLinePosition
		{
			get
			{
				return this.valueLinePos;
			}
			set
			{
				this.valueLinePos = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000AB1AE File Offset: 0x000A93AE
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x000AB1B6 File Offset: 0x000A93B6
		internal int ValueLineNumber
		{
			get
			{
				return this.valueLineNum;
			}
			set
			{
				this.valueLineNum = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000AB1BF File Offset: 0x000A93BF
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x000AB1D5 File Offset: 0x000A93D5
		internal string DefaultValueExpanded
		{
			get
			{
				if (this.defExpanded == null)
				{
					return string.Empty;
				}
				return this.defExpanded;
			}
			set
			{
				this.defExpanded = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000AB1DE File Offset: 0x000A93DE
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x000AB1EB File Offset: 0x000A93EB
		internal XmlTokenizedType TokenizedType
		{
			get
			{
				return base.Datatype.TokenizedType;
			}
			set
			{
				base.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(value);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000AB1F9 File Offset: 0x000A93F9
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x000AB201 File Offset: 0x000A9401
		internal SchemaAttDef.Reserve Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000AB20A File Offset: 0x000A940A
		internal bool DefaultValueChecked
		{
			get
			{
				return this.defaultValueChecked;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000AB212 File Offset: 0x000A9412
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x000AB21A File Offset: 0x000A941A
		internal XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000AB224 File Offset: 0x000A9424
		internal void CheckXmlSpace(IValidationEventHandling validationEventHandling)
		{
			if (this.datatype.TokenizedType == XmlTokenizedType.ENUMERATION && this.values != null && this.values.Count <= 2)
			{
				string text = this.values[0].ToString();
				if (this.values.Count == 2)
				{
					string text2 = this.values[1].ToString();
					if ((text == "default" || text2 == "default") && (text == "preserve" || text2 == "preserve"))
					{
						return;
					}
				}
				else if (text == "default" || text == "preserve")
				{
					return;
				}
			}
			validationEventHandling.SendEvent(new XmlSchemaException("Invalid xml:space syntax.", string.Empty), XmlSeverityType.Error);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000AB2F7 File Offset: 0x000A94F7
		internal SchemaAttDef Clone()
		{
			return (SchemaAttDef)base.MemberwiseClone();
		}

		// Token: 0x04000CC9 RID: 3273
		private string defExpanded;

		// Token: 0x04000CCA RID: 3274
		private int lineNum;

		// Token: 0x04000CCB RID: 3275
		private int linePos;

		// Token: 0x04000CCC RID: 3276
		private int valueLineNum;

		// Token: 0x04000CCD RID: 3277
		private int valueLinePos;

		// Token: 0x04000CCE RID: 3278
		private SchemaAttDef.Reserve reserved;

		// Token: 0x04000CCF RID: 3279
		private bool defaultValueChecked;

		// Token: 0x04000CD0 RID: 3280
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04000CD1 RID: 3281
		public static readonly SchemaAttDef Empty = new SchemaAttDef();

		// Token: 0x02000290 RID: 656
		internal enum Reserve
		{
			// Token: 0x04000CD3 RID: 3283
			None,
			// Token: 0x04000CD4 RID: 3284
			XmlSpace,
			// Token: 0x04000CD5 RID: 3285
			XmlLang
		}
	}
}
