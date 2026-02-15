using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001D RID: 29
	public class LdapAttribute : ICloneable, IComparable
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004A28 File Offset: 0x00002C28
		public virtual IEnumerator ByteValues
		{
			get
			{
				object[] byteValueArray = this.ByteValueArray;
				return new ArrayEnumeration(byteValueArray);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004A44 File Offset: 0x00002C44
		public virtual IEnumerator StringValues
		{
			get
			{
				object[] stringValueArray = this.StringValueArray;
				return new ArrayEnumeration(stringValueArray);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004A60 File Offset: 0x00002C60
		[CLSCompliant(false)]
		public virtual sbyte[][] ByteValueArray
		{
			get
			{
				if (this.values == null)
				{
					return new sbyte[0][];
				}
				int num = this.values.Length;
				sbyte[][] array = new sbyte[num][];
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					array[i] = new sbyte[((sbyte[])this.values[i]).Length];
					Array.Copy((Array)this.values[i], 0, array[i], 0, array[i].Length);
					i++;
				}
				return array;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004ACC File Offset: 0x00002CCC
		public virtual string[] StringValueArray
		{
			get
			{
				if (this.values == null)
				{
					return new string[0];
				}
				int num = this.values.Length;
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					try
					{
						char[] chars = Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[i]));
						array[i] = new string(chars);
					}
					catch (IOException ex)
					{
						throw new SystemException(ex.ToString());
					}
				}
				return array;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004B50 File Offset: 0x00002D50
		public virtual string StringValue
		{
			get
			{
				string text = null;
				if (this.values != null)
				{
					try
					{
						text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[0])));
					}
					catch (IOException ex)
					{
						throw new SystemException(ex.ToString());
					}
				}
				return text;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004BAC File Offset: 0x00002DAC
		[CLSCompliant(false)]
		public virtual sbyte[] ByteValue
		{
			get
			{
				sbyte[] array = null;
				if (this.values != null)
				{
					array = new sbyte[((sbyte[])this.values[0]).Length];
					Array.Copy((Array)this.values[0], 0, array, 0, array.Length);
				}
				return array;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public virtual string LangSubtype
		{
			get
			{
				if (this.subTypes != null)
				{
					for (int i = 0; i < this.subTypes.Length; i++)
					{
						if (this.subTypes[i].StartsWith("lang-"))
						{
							return this.subTypes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004C3A File Offset: 0x00002E3A
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000026 RID: 38
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004C44 File Offset: 0x00002E44
		protected internal virtual string Value
		{
			set
			{
				this.values = null;
				try
				{
					sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(value));
					this.add(array);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004C94 File Offset: 0x00002E94
		public LdapAttribute(LdapAttribute attr)
		{
			if (attr == null)
			{
				throw new ArgumentException("LdapAttribute class cannot be null");
			}
			this.name = attr.name;
			this.baseName = attr.baseName;
			if (attr.subTypes != null)
			{
				this.subTypes = new string[attr.subTypes.Length];
				Array.Copy(attr.subTypes, 0, this.subTypes, 0, this.subTypes.Length);
			}
			if (attr.values != null)
			{
				this.values = new object[attr.values.Length];
				Array.Copy(attr.values, 0, this.values, 0, this.values.Length);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004D39 File Offset: 0x00002F39
		public LdapAttribute(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			this.name = attrName;
			this.baseName = LdapAttribute.getBaseName(attrName);
			this.subTypes = LdapAttribute.getSubtypes(attrName);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004D70 File Offset: 0x00002F70
		[CLSCompliant(false)]
		public LdapAttribute(string attrName, sbyte[] attrBytes)
			: this(attrName)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			sbyte[] array = new sbyte[attrBytes.Length];
			Array.Copy(attrBytes, 0, array, 0, attrBytes.Length);
			this.add(array);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public LdapAttribute(string attrName, string attrString)
			: this(attrName)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.add(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004E10 File Offset: 0x00003010
		public LdapAttribute(string attrName, string[] attrStrings)
			: this(attrName)
		{
			if (attrStrings == null)
			{
				throw new ArgumentException("Attribute values array cannot be null");
			}
			int i = 0;
			int num = attrStrings.Length;
			while (i < num)
			{
				try
				{
					if (attrStrings[i] == null)
					{
						throw new ArgumentException("Attribute value at array index " + i.ToString() + " cannot be null");
					}
					sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrStrings[i]));
					this.add(array);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
				i++;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004EA0 File Offset: 0x000030A0
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.values != null)
				{
					Array.Copy(this.values, 0, ((LdapAttribute)obj).values, 0, this.values.Length);
				}
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004F00 File Offset: 0x00003100
		public virtual void addValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.add(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004F58 File Offset: 0x00003158
		[CLSCompliant(false)]
		public virtual void addValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(attrBytes);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004F6F File Offset: 0x0000316F
		public virtual void addBase64Value(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F8B File Offset: 0x0000318B
		public virtual void addBase64Value(StringBuilder attrString, int start, int end)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString, start, end));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004FA9 File Offset: 0x000031A9
		public virtual void addBase64Value(char[] attrChars)
		{
			if (attrChars == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrChars));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004FC5 File Offset: 0x000031C5
		public virtual void addURLValue(string url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			this.addURLValue(new Uri(url));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004FE4 File Offset: 0x000031E4
		public virtual void addURLValue(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			try
			{
				Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
				ArrayList arrayList = new ArrayList();
				sbyte[] array = new sbyte[4096];
				int num = 0;
				int num2;
				while ((num2 = SupportClass.ReadInput(responseStream, ref array, 0, 4096)) != -1)
				{
					arrayList.Add(new LdapAttribute.URLData(this, array, num2));
					array = new sbyte[4096];
					num += num2;
				}
				sbyte[] array2 = new sbyte[num];
				int num3 = 0;
				for (int i = 0; i < arrayList.Count; i++)
				{
					LdapAttribute.URLData urldata = (LdapAttribute.URLData)arrayList[i];
					num2 = urldata.getLength();
					Array.Copy(urldata.getData(), 0, array2, num3, num2);
					num3 += num2;
				}
				this.add(array2);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000050D0 File Offset: 0x000032D0
		public virtual string getBaseName()
		{
			return this.baseName;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000050D8 File Offset: 0x000032D8
		public static string getBaseName(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			int num = attrName.IndexOf(';');
			if (-1 == num)
			{
				return attrName;
			}
			return attrName.Substring(0, num);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000510A File Offset: 0x0000330A
		public virtual string[] getSubtypes()
		{
			return this.subTypes;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005114 File Offset: 0x00003314
		public static string[] getSubtypes(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(attrName, ";");
			string[] array = null;
			int count = tokenizer.Count;
			if (count > 0)
			{
				tokenizer.NextToken();
				array = new string[count - 1];
				int num = 0;
				while (tokenizer.HasMoreTokens())
				{
					array[num++] = tokenizer.NextToken();
				}
			}
			return array;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005174 File Offset: 0x00003374
		public virtual bool hasSubtype(string subtype)
		{
			if (subtype == null)
			{
				throw new ArgumentException("subtype cannot be null");
			}
			if (this.subTypes != null)
			{
				for (int i = 0; i < this.subTypes.Length; i++)
				{
					if (this.subTypes[i].ToUpper().Equals(subtype.ToUpper()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000051C8 File Offset: 0x000033C8
		public virtual bool hasSubtypes(string[] subtypes)
		{
			if (subtypes == null)
			{
				throw new ArgumentException("subtypes cannot be null");
			}
			int i = 0;
			IL_006D:
			while (i < subtypes.Length)
			{
				for (int j = 0; j < this.subTypes.Length; j++)
				{
					if (this.subTypes[j] == null)
					{
						throw new ArgumentException("subtype at array index " + i.ToString() + " cannot be null");
					}
					if (this.subTypes[j].ToUpper().Equals(subtypes[i].ToUpper()))
					{
						i++;
						goto IL_006D;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000524C File Offset: 0x0000344C
		public virtual void removeValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(attrString));
				this.removeValue(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052A4 File Offset: 0x000034A4
		[CLSCompliant(false)]
		public virtual void removeValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			int i = 0;
			while (i < this.values.Length)
			{
				if (this.equals(attrBytes, (sbyte[])this.values[i]))
				{
					if (i == 0 && 1 == this.values.Length)
					{
						this.values = null;
						return;
					}
					if (this.values.Length == 1)
					{
						this.values = null;
						return;
					}
					int num = this.values.Length - i - 1;
					object[] array = new object[this.values.Length - 1];
					if (i != 0)
					{
						Array.Copy(this.values, 0, array, 0, i);
					}
					if (num != 0)
					{
						Array.Copy(this.values, i + 1, array, i, num);
					}
					this.values = array;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005364 File Offset: 0x00003564
		public virtual int size()
		{
			if (this.values != null)
			{
				return this.values.Length;
			}
			return 0;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005378 File Offset: 0x00003578
		public virtual int CompareTo(object attribute)
		{
			return this.name.CompareTo(((LdapAttribute)attribute).name);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005390 File Offset: 0x00003590
		private void add(sbyte[] bytes)
		{
			if (this.values == null)
			{
				this.values = new object[] { bytes };
				return;
			}
			for (int i = 0; i < this.values.Length; i++)
			{
				if (this.equals(bytes, (sbyte[])this.values[i]))
				{
					return;
				}
			}
			object[] array = new object[this.values.Length + 1];
			Array.Copy(this.values, 0, array, 0, this.values.Length);
			array[this.values.Length] = bytes;
			this.values = array;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000541C File Offset: 0x0000361C
		private bool equals(sbyte[] e1, sbyte[] e2)
		{
			if (e1 == e2)
			{
				return true;
			}
			if (e1 == null || e2 == null)
			{
				return false;
			}
			int num = e1.Length;
			if (e2.Length != num)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (e1[i] != e2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000545C File Offset: 0x0000365C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttribute: ");
			try
			{
				stringBuilder.Append("{type='" + this.name + "'");
				if (this.values != null)
				{
					stringBuilder.Append(", ");
					if (this.values.Length == 1)
					{
						stringBuilder.Append("value='");
					}
					else
					{
						stringBuilder.Append("values='");
					}
					for (int i = 0; i < this.values.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append("','");
						}
						if (((sbyte[])this.values[i]).Length != 0)
						{
							string text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray((sbyte[])this.values[i])));
							if (text.Length == 0)
							{
								stringBuilder.Append("<binary value, length:" + text.Length.ToString());
							}
							else
							{
								stringBuilder.Append(text);
							}
						}
					}
					stringBuilder.Append("'");
				}
				stringBuilder.Append("}");
			}
			catch (Exception ex)
			{
				throw new SystemException(ex.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400007E RID: 126
		private string name;

		// Token: 0x0400007F RID: 127
		private string baseName;

		// Token: 0x04000080 RID: 128
		private string[] subTypes;

		// Token: 0x04000081 RID: 129
		private object[] values;

		// Token: 0x0200001E RID: 30
		private class URLData
		{
			// Token: 0x0600010C RID: 268 RVA: 0x00005594 File Offset: 0x00003794
			private void InitBlock(LdapAttribute enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600010D RID: 269 RVA: 0x0000559D File Offset: 0x0000379D
			public LdapAttribute Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600010E RID: 270 RVA: 0x000055A5 File Offset: 0x000037A5
			public URLData(LdapAttribute enclosingInstance, sbyte[] data, int length)
			{
				this.InitBlock(enclosingInstance);
				this.length = length;
				this.data = data;
			}

			// Token: 0x0600010F RID: 271 RVA: 0x000055C2 File Offset: 0x000037C2
			public int getLength()
			{
				return this.length;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x000055CA File Offset: 0x000037CA
			public sbyte[] getData()
			{
				return this.data;
			}

			// Token: 0x04000082 RID: 130
			private LdapAttribute enclosingInstance;

			// Token: 0x04000083 RID: 131
			private int length;

			// Token: 0x04000084 RID: 132
			private sbyte[] data;
		}
	}
}
