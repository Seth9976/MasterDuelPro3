using System;

namespace System.Runtime.Serialization
{
	/// <summary>Holds the value, <see cref="T:System.Type" />, and name of a serialized object. </summary>
	// Token: 0x020004A9 RID: 1193
	public readonly struct SerializationEntry
	{
		// Token: 0x06002632 RID: 9778 RVA: 0x0009A76C File Offset: 0x0009896C
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this._name = entryName;
			this._value = entryValue;
			this._type = entryType;
		}

		/// <summary>Gets the value contained in the object.</summary>
		/// <returns>The value contained in the object.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x0009A783 File Offset: 0x00098983
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x0009A78B File Offset: 0x0009898B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04001244 RID: 4676
		private readonly string _name;

		// Token: 0x04001245 RID: 4677
		private readonly object _value;

		// Token: 0x04001246 RID: 4678
		private readonly Type _type;
	}
}
