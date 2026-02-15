using System;
using System.Runtime.CompilerServices;

namespace Unity.Properties
{
	// Token: 0x0200001A RID: 26
	public readonly struct PropertyPathPart : IEquatable<PropertyPathPart>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028F5 File Offset: 0x00000AF5
		public bool IsName
		{
			get
			{
				return this.Kind == PropertyPathPartKind.Name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002900 File Offset: 0x00000B00
		public bool IsIndex
		{
			get
			{
				return this.Kind == PropertyPathPartKind.Index;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000290B File Offset: 0x00000B0B
		public PropertyPathPartKind Kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002914 File Offset: 0x00000B14
		public string Name
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Name);
				return this.m_Name;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002934 File Offset: 0x00000B34
		public int Index
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Index);
				return this.m_Index;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002954 File Offset: 0x00000B54
		public object Key
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Key);
				return this.m_Key;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002974 File Offset: 0x00000B74
		public PropertyPathPart(string name)
		{
			this.m_Kind = PropertyPathPartKind.Name;
			this.m_Name = name;
			this.m_Index = -1;
			this.m_Key = null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002993 File Offset: 0x00000B93
		public PropertyPathPart(int index)
		{
			this.m_Kind = PropertyPathPartKind.Index;
			this.m_Name = string.Empty;
			this.m_Index = index;
			this.m_Key = null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029B6 File Offset: 0x00000BB6
		public PropertyPathPart(object key)
		{
			this.m_Kind = PropertyPathPartKind.Key;
			this.m_Name = string.Empty;
			this.m_Index = -1;
			this.m_Key = key;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029DC File Offset: 0x00000BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckKind(PropertyPathPartKind type)
		{
			bool flag = type != this.Kind;
			if (flag)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A00 File Offset: 0x00000C00
		public override string ToString()
		{
			PropertyPathPartKind kind = this.Kind;
			if (!true)
			{
			}
			string text;
			switch (kind)
			{
			case PropertyPathPartKind.Name:
				text = this.m_Name;
				break;
			case PropertyPathPartKind.Index:
				text = "[" + this.m_Index.ToString() + "]";
				break;
			case PropertyPathPartKind.Key:
			{
				string text2 = "[\"";
				object key = this.m_Key;
				text = text2 + ((key != null) ? key.ToString() : null) + "\"]";
				break;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (!true)
			{
			}
			return text;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A88 File Offset: 0x00000C88
		public bool Equals(PropertyPathPart other)
		{
			return this.m_Kind == other.m_Kind && this.m_Name == other.m_Name && this.m_Index == other.m_Index && object.Equals(this.m_Key, other.m_Key);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is PropertyPathPart)
			{
				PropertyPathPart other = (PropertyPathPart)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B0C File Offset: 0x00000D0C
		public override int GetHashCode()
		{
			int hashCode = (int)this.m_Kind;
			PropertyPathPartKind kind = this.m_Kind;
			if (!true)
			{
			}
			int num;
			switch (kind)
			{
			case PropertyPathPartKind.Name:
				num = (hashCode * 397) ^ ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0);
				break;
			case PropertyPathPartKind.Index:
				num = (hashCode * 397) ^ this.m_Index;
				break;
			case PropertyPathPartKind.Key:
				num = (hashCode * 397) ^ ((this.m_Key != null) ? this.m_Key.GetHashCode() : 0);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (!true)
			{
			}
			return num;
		}

		// Token: 0x04000028 RID: 40
		private readonly PropertyPathPartKind m_Kind;

		// Token: 0x04000029 RID: 41
		private readonly string m_Name;

		// Token: 0x0400002A RID: 42
		private readonly int m_Index;

		// Token: 0x0400002B RID: 43
		private readonly object m_Key;
	}
}
