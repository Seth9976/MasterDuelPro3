using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace Unity.Properties
{
	// Token: 0x02000059 RID: 89
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal readonly struct ConversionRegistry : IEqualityComparer<ConversionRegistry>
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00005910 File Offset: 0x00003B10
		private ConversionRegistry(Dictionary<ConversionRegistry.ConverterKey, Delegate> storage)
		{
			this.m_Converters = storage;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000591C File Offset: 0x00003B1C
		public static ConversionRegistry Create()
		{
			return new ConversionRegistry(new Dictionary<ConversionRegistry.ConverterKey, Delegate>(ConversionRegistry.Comparer));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000593D File Offset: 0x00003B3D
		public void Register(Type source, Type destination, Delegate converter)
		{
			Dictionary<ConversionRegistry.ConverterKey, Delegate> converters = this.m_Converters;
			ConversionRegistry.ConverterKey converterKey = new ConversionRegistry.ConverterKey(source, destination);
			if (converter == null)
			{
				throw new ArgumentException("converter");
			}
			converters[converterKey] = converter;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005964 File Offset: 0x00003B64
		public Delegate GetConverter(Type source, Type destination)
		{
			ConversionRegistry.ConverterKey key = new ConversionRegistry.ConverterKey(source, destination);
			Delegate converter;
			return this.m_Converters.TryGetValue(key, out converter) ? converter : null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005994 File Offset: 0x00003B94
		public bool TryGetConverter(Type source, Type destination, out Delegate converter)
		{
			converter = this.GetConverter(source, destination);
			return converter != null;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000059B8 File Offset: 0x00003BB8
		public bool Equals(ConversionRegistry x, ConversionRegistry y)
		{
			return x.m_Converters == y.m_Converters;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000059D8 File Offset: 0x00003BD8
		public int GetHashCode(ConversionRegistry obj)
		{
			return (obj.m_Converters != null) ? obj.m_Converters.GetHashCode() : 0;
		}

		// Token: 0x04000080 RID: 128
		private static readonly ConversionRegistry.ConverterKeyComparer Comparer = new ConversionRegistry.ConverterKeyComparer();

		// Token: 0x04000081 RID: 129
		private readonly Dictionary<ConversionRegistry.ConverterKey, Delegate> m_Converters;

		// Token: 0x0200005A RID: 90
		private class ConverterKeyComparer : IEqualityComparer<ConversionRegistry.ConverterKey>
		{
			// Token: 0x06000153 RID: 339 RVA: 0x00005A0C File Offset: 0x00003C0C
			public bool Equals(ConversionRegistry.ConverterKey x, ConversionRegistry.ConverterKey y)
			{
				return x.SourceType == y.SourceType && x.DestinationType == y.DestinationType;
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00005A48 File Offset: 0x00003C48
			public int GetHashCode(ConversionRegistry.ConverterKey obj)
			{
				return (((obj.SourceType != null) ? obj.SourceType.GetHashCode() : 0) * 397) ^ ((obj.DestinationType != null) ? obj.DestinationType.GetHashCode() : 0);
			}
		}

		// Token: 0x0200005B RID: 91
		private readonly struct ConverterKey
		{
			// Token: 0x06000156 RID: 342 RVA: 0x00005A99 File Offset: 0x00003C99
			public ConverterKey(Type source, Type destination)
			{
				this.SourceType = source;
				this.DestinationType = destination;
			}

			// Token: 0x04000082 RID: 130
			public readonly Type SourceType;

			// Token: 0x04000083 RID: 131
			public readonly Type DestinationType;
		}
	}
}
