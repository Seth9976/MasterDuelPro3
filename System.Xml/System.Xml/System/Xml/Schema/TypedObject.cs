using System;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x02000217 RID: 535
	internal class TypedObject
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00099D62 File Offset: 0x00097F62
		public int Dim
		{
			get
			{
				return this.dim;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00099D6A File Offset: 0x00097F6A
		public bool IsList
		{
			get
			{
				return this.isList;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00099D72 File Offset: 0x00097F72
		public bool IsDecimal
		{
			get
			{
				return this.dstruct.IsDecimal;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00099D7F File Offset: 0x00097F7F
		public decimal[] Dvalue
		{
			get
			{
				return this.dstruct.Dvalue;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00099D8C File Offset: 0x00097F8C
		public object Value
		{
			get
			{
				return this.ovalue;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00099D94 File Offset: 0x00097F94
		public XmlSchemaDatatype Type
		{
			get
			{
				return this.xsdtype;
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00099D9C File Offset: 0x00097F9C
		public TypedObject(object obj, string svalue, XmlSchemaDatatype xsdtype)
		{
			this.ovalue = obj;
			this.svalue = svalue;
			this.xsdtype = xsdtype;
			if (xsdtype.Variety == XmlSchemaDatatypeVariety.List || xsdtype is Datatype_base64Binary || xsdtype is Datatype_hexBinary)
			{
				this.isList = true;
				this.dim = ((Array)obj).Length;
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00099DFC File Offset: 0x00097FFC
		public override string ToString()
		{
			return this.svalue;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00099E04 File Offset: 0x00098004
		public void SetDecimal()
		{
			if (this.dstruct != null)
			{
				return;
			}
			XmlTypeCode typeCode = this.xsdtype.TypeCode;
			if (typeCode == XmlTypeCode.Decimal || typeCode - XmlTypeCode.Integer <= 12)
			{
				if (this.isList)
				{
					this.dstruct = new TypedObject.DecimalStruct(this.dim);
					for (int i = 0; i < this.dim; i++)
					{
						this.dstruct.Dvalue[i] = Convert.ToDecimal(((Array)this.ovalue).GetValue(i), NumberFormatInfo.InvariantInfo);
					}
				}
				else
				{
					this.dstruct = new TypedObject.DecimalStruct();
					this.dstruct.Dvalue[0] = Convert.ToDecimal(this.ovalue, NumberFormatInfo.InvariantInfo);
				}
				this.dstruct.IsDecimal = true;
				return;
			}
			if (this.isList)
			{
				this.dstruct = new TypedObject.DecimalStruct(this.dim);
				return;
			}
			this.dstruct = new TypedObject.DecimalStruct();
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00099EEC File Offset: 0x000980EC
		private bool ListDValueEquals(TypedObject other)
		{
			for (int i = 0; i < this.Dim; i++)
			{
				if (this.Dvalue[i] != other.Dvalue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00099F2C File Offset: 0x0009812C
		public bool Equals(TypedObject other)
		{
			if (this.Dim != other.Dim)
			{
				return false;
			}
			if (this.Type != other.Type)
			{
				if (!this.Type.IsComparable(other.Type))
				{
					return false;
				}
				other.SetDecimal();
				this.SetDecimal();
				if (this.IsDecimal && other.IsDecimal)
				{
					return this.ListDValueEquals(other);
				}
			}
			if (this.IsList)
			{
				if (other.IsList)
				{
					return this.Type.Compare(this.Value, other.Value) == 0;
				}
				Array array = this.Value as Array;
				XmlAtomicValue[] array2 = array as XmlAtomicValue[];
				if (array2 != null)
				{
					return array2.Length == 1 && array2.GetValue(0).Equals(other.Value);
				}
				return array.Length == 1 && array.GetValue(0).Equals(other.Value);
			}
			else
			{
				if (!other.IsList)
				{
					return this.Value.Equals(other.Value);
				}
				Array array3 = other.Value as Array;
				XmlAtomicValue[] array4 = array3 as XmlAtomicValue[];
				if (array4 != null)
				{
					return array4.Length == 1 && array4.GetValue(0).Equals(this.Value);
				}
				return array3.Length == 1 && array3.GetValue(0).Equals(this.Value);
			}
		}

		// Token: 0x04000B4F RID: 2895
		private TypedObject.DecimalStruct dstruct;

		// Token: 0x04000B50 RID: 2896
		private object ovalue;

		// Token: 0x04000B51 RID: 2897
		private string svalue;

		// Token: 0x04000B52 RID: 2898
		private XmlSchemaDatatype xsdtype;

		// Token: 0x04000B53 RID: 2899
		private int dim = 1;

		// Token: 0x04000B54 RID: 2900
		private bool isList;

		// Token: 0x02000218 RID: 536
		private class DecimalStruct
		{
			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0009A072 File Offset: 0x00098272
			// (set) Token: 0x06001A7B RID: 6779 RVA: 0x0009A07A File Offset: 0x0009827A
			public bool IsDecimal
			{
				get
				{
					return this.isDecimal;
				}
				set
				{
					this.isDecimal = value;
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0009A083 File Offset: 0x00098283
			public decimal[] Dvalue
			{
				get
				{
					return this.dvalue;
				}
			}

			// Token: 0x06001A7D RID: 6781 RVA: 0x0009A08B File Offset: 0x0009828B
			public DecimalStruct()
			{
				this.dvalue = new decimal[1];
			}

			// Token: 0x06001A7E RID: 6782 RVA: 0x0009A09F File Offset: 0x0009829F
			public DecimalStruct(int dim)
			{
				this.dvalue = new decimal[dim];
			}

			// Token: 0x04000B55 RID: 2901
			private bool isDecimal;

			// Token: 0x04000B56 RID: 2902
			private decimal[] dvalue;
		}
	}
}
