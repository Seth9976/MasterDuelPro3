using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000026 RID: 38
	internal readonly struct TypeConverterRegistry : IEqualityComparer<TypeConverterRegistry>
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00004088 File Offset: 0x00002288
		private TypeConverterRegistry(Dictionary<TypeConverterRegistry.ConverterKey, Delegate> storage)
		{
			this.m_Converters = storage;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004094 File Offset: 0x00002294
		public static TypeConverterRegistry Create()
		{
			return new TypeConverterRegistry(new Dictionary<TypeConverterRegistry.ConverterKey, Delegate>(TypeConverterRegistry.k_Comparer));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000040B5 File Offset: 0x000022B5
		public void Register(Type source, Type destination, Delegate converter)
		{
			Dictionary<TypeConverterRegistry.ConverterKey, Delegate> converters = this.m_Converters;
			TypeConverterRegistry.ConverterKey converterKey = new TypeConverterRegistry.ConverterKey(source, destination);
			if (converter == null)
			{
				throw new ArgumentException("converter");
			}
			converters[converterKey] = converter;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040DC File Offset: 0x000022DC
		internal void Apply(TypeConverterRegistry registry)
		{
			foreach (KeyValuePair<TypeConverterRegistry.ConverterKey, Delegate> c in registry.m_Converters)
			{
				this.Register(c.Key.SourceType, c.Key.DestinationType, c.Value);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004154 File Offset: 0x00002354
		public Delegate GetConverter(Type source, Type destination)
		{
			TypeConverterRegistry.ConverterKey key = new TypeConverterRegistry.ConverterKey(source, destination);
			Delegate converter;
			return this.m_Converters.TryGetValue(key, out converter) ? converter : null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004184 File Offset: 0x00002384
		public bool TryGetConverter(Type source, Type destination, out Delegate converter)
		{
			converter = this.GetConverter(source, destination);
			return converter != null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000041A8 File Offset: 0x000023A8
		public bool Equals(TypeConverterRegistry x, TypeConverterRegistry y)
		{
			return x.m_Converters == y.m_Converters;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000041C8 File Offset: 0x000023C8
		public int GetHashCode(TypeConverterRegistry obj)
		{
			return (obj.m_Converters != null) ? obj.m_Converters.GetHashCode() : 0;
		}

		// Token: 0x04000054 RID: 84
		private static readonly TypeConverterRegistry.ConverterKeyComparer k_Comparer = new TypeConverterRegistry.ConverterKeyComparer();

		// Token: 0x04000055 RID: 85
		private readonly Dictionary<TypeConverterRegistry.ConverterKey, Delegate> m_Converters;

		// Token: 0x02000027 RID: 39
		private class ConverterKeyComparer : IEqualityComparer<TypeConverterRegistry.ConverterKey>
		{
			// Token: 0x060000C2 RID: 194 RVA: 0x000041FC File Offset: 0x000023FC
			public bool Equals(TypeConverterRegistry.ConverterKey x, TypeConverterRegistry.ConverterKey y)
			{
				return x.SourceType == y.SourceType && x.DestinationType == y.DestinationType;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004238 File Offset: 0x00002438
			public int GetHashCode(TypeConverterRegistry.ConverterKey obj)
			{
				return (((obj.SourceType != null) ? obj.SourceType.GetHashCode() : 0) * 397) ^ ((obj.DestinationType != null) ? obj.DestinationType.GetHashCode() : 0);
			}
		}

		// Token: 0x02000028 RID: 40
		private readonly struct ConverterKey
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x00004289 File Offset: 0x00002489
			public ConverterKey(Type source, Type destination)
			{
				this.SourceType = source;
				this.DestinationType = destination;
			}

			// Token: 0x04000056 RID: 86
			public readonly Type SourceType;

			// Token: 0x04000057 RID: 87
			public readonly Type DestinationType;
		}
	}
}
