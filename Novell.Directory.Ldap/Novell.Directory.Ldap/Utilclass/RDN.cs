using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200005D RID: 93
	public class RDN
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		protected internal virtual string RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		public virtual string Type
		{
			get
			{
				return (string)this.types[0];
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000F404 File Offset: 0x0000D604
		public virtual string[] Types
		{
			get
			{
				string[] array = new string[this.types.Count];
				for (int i = 0; i < this.types.Count; i++)
				{
					array[i] = (string)this.types[i];
				}
				return array;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000F44D File Offset: 0x0000D64D
		public virtual string Value
		{
			get
			{
				return (string)this.values[0];
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000F460 File Offset: 0x0000D660
		public virtual string[] Values
		{
			get
			{
				string[] array = new string[this.values.Count];
				for (int i = 0; i < this.values.Count; i++)
				{
					array[i] = (string)this.values[i];
				}
				return array;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000F4A9 File Offset: 0x0000D6A9
		public virtual bool Multivalued
		{
			get
			{
				return this.values.Count > 1;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public RDN(string rdn)
		{
			this.rawValue = rdn;
			ArrayList rdns = new DN(rdn).RDNs;
			if (rdns.Count != 1)
			{
				throw new ArgumentException("Invalid RDN: see API documentation");
			}
			RDN rdn2 = (RDN)rdns[0];
			this.types = rdn2.types;
			this.values = rdn2.values;
			this.rawValue = rdn2.rawValue;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F525 File Offset: 0x0000D725
		public RDN()
		{
			this.types = new ArrayList();
			this.values = new ArrayList();
			this.rawValue = "";
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000F550 File Offset: 0x0000D750
		[CLSCompliant(false)]
		public virtual bool equals(RDN rdn)
		{
			if (this.values.Count != rdn.values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.values.Count; i++)
			{
				int num = 0;
				while (num < this.values.Count && (!((string)this.values[i]).ToUpper().Equals(((string)rdn.values[num]).ToUpper()) || !this.equalAttrType((string)this.types[i], (string)rdn.types[num])))
				{
					num++;
				}
				if (num >= rdn.values.Count)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000F617 File Offset: 0x0000D817
		private bool equalAttrType(string attr1, string attr2)
		{
			if (char.IsDigit(attr1[0]) ^ char.IsDigit(attr2[0]))
			{
				throw new ArgumentException("OID numbers are not currently compared to attribute names");
			}
			return attr1.ToUpper().Equals(attr2.ToUpper());
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000F650 File Offset: 0x0000D850
		public virtual void add(string attrType, string attrValue, string rawValue)
		{
			this.types.Add(attrType);
			this.values.Add(attrValue);
			this.rawValue += rawValue;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F67E File Offset: 0x0000D87E
		public override string ToString()
		{
			return this.toString(false);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F688 File Offset: 0x0000D888
		[CLSCompliant(false)]
		public virtual string toString(bool noTypes)
		{
			int count = this.types.Count;
			string text = "";
			if (count < 1)
			{
				return null;
			}
			if (!noTypes)
			{
				object obj = this.types[0];
				text = ((obj != null) ? obj.ToString() : null) + "=";
			}
			string text2 = text;
			object obj2 = this.values[0];
			text = text2 + ((obj2 != null) ? obj2.ToString() : null);
			for (int i = 1; i < count; i++)
			{
				text += "+";
				if (!noTypes)
				{
					string text3 = text;
					object obj3 = this.types[i];
					text = text3 + ((obj3 != null) ? obj3.ToString() : null) + "=";
				}
				string text4 = text;
				object obj4 = this.values[i];
				text = text4 + ((obj4 != null) ? obj4.ToString() : null);
			}
			return text;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F754 File Offset: 0x0000D954
		public virtual string[] explodeRDN(bool noTypes)
		{
			int count = this.types.Count;
			if (count < 1)
			{
				return null;
			}
			string[] array = new string[this.types.Count];
			if (!noTypes)
			{
				string[] array2 = array;
				int num = 0;
				object obj = this.types[0];
				array2[num] = ((obj != null) ? obj.ToString() : null) + "=";
			}
			string[] array3 = array;
			int num2 = 0;
			string text = array3[num2];
			object obj2 = this.values[0];
			array3[num2] = text + ((obj2 != null) ? obj2.ToString() : null);
			for (int i = 1; i < count; i++)
			{
				if (!noTypes)
				{
					string[] array4 = array;
					int num3 = i;
					string text2 = array4[num3];
					object obj3 = this.types[i];
					array4[num3] = text2 + ((obj3 != null) ? obj3.ToString() : null) + "=";
				}
				string[] array5 = array;
				int num4 = i;
				string text3 = array5[num4];
				object obj4 = this.values[i];
				array5[num4] = text3 + ((obj4 != null) ? obj4.ToString() : null);
			}
			return array;
		}

		// Token: 0x04000215 RID: 533
		private ArrayList types;

		// Token: 0x04000216 RID: 534
		private ArrayList values;

		// Token: 0x04000217 RID: 535
		private string rawValue;
	}
}
