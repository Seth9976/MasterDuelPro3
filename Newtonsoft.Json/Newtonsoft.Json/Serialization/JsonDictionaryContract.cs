using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200011C RID: 284
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonDictionaryContract : JsonContainerContract
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x000276D2 File Offset: 0x000258D2
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x000276DA File Offset: 0x000258DA
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> DictionaryKeyResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x000276E3 File Offset: 0x000258E3
		public Type DictionaryKeyType { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000276EB File Offset: 0x000258EB
		public Type DictionaryValueType { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000276F3 File Offset: 0x000258F3
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x000276FB File Offset: 0x000258FB
		internal JsonContract KeyContract { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00027704 File Offset: 0x00025904
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002770C File Offset: 0x0002590C
		[Nullable(new byte[] { 2, 1 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				if (this._parameterizedCreator == null && this._parameterizedConstructor != null)
				{
					this._parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(this._parameterizedConstructor);
				}
				return this._parameterizedCreator;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00027740 File Offset: 0x00025940
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00027748 File Offset: 0x00025948
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00027751 File Offset: 0x00025951
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00027759 File Offset: 0x00025959
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00027762 File Offset: 0x00025962
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00027784 File Offset: 0x00025984
		[NullableContext(1)]
		public JsonDictionaryContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Dictionary;
			Type type;
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IDictionary<, >), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IDictionary<, >)))
				{
					base.CreatedType = typeof(Dictionary<, >).MakeGenericType(new Type[] { type, type2 });
				}
				else if (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.ConcurrentDictionary`2")
				{
					this.ShouldCreateWrapper = true;
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(this.NonNullableUnderlyingType, typeof(ReadOnlyDictionary<, >));
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >)))
				{
					base.CreatedType = typeof(ReadOnlyDictionary<, >).MakeGenericType(new Type[] { type, type2 });
				}
				this.IsReadOnlyOrFixedSize = true;
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(this.NonNullableUnderlyingType, out type, out type2);
				if (this.NonNullableUnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (type != null && type2 != null)
			{
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(KeyValuePair<, >).MakeGenericType(new Type[] { type, type2 }), typeof(IDictionary<, >).MakeGenericType(new Type[] { type, type2 }));
				if (!this.HasParameterizedCreatorInternal && this.NonNullableUnderlyingType.Name == "FSharpMap`2")
				{
					FSharpUtils.EnsureInitialized(this.NonNullableUnderlyingType.Assembly());
					this._parameterizedCreator = FSharpUtils.Instance.CreateMap(type, type2);
				}
			}
			if (!typeof(IDictionary).IsAssignableFrom(base.CreatedType))
			{
				this.ShouldCreateWrapper = true;
			}
			this.DictionaryKeyType = type;
			this.DictionaryValueType = type2;
			Type type3;
			ObjectConstructor<object> objectConstructor;
			if (this.DictionaryKeyType != null && this.DictionaryValueType != null && ImmutableCollectionsUtils.TryBuildImmutableForDictionaryContract(this.NonNullableUnderlyingType, this.DictionaryKeyType, this.DictionaryValueType, out type3, out objectConstructor))
			{
				base.CreatedType = type3;
				this._parameterizedCreator = objectConstructor;
				this.IsReadOnlyOrFixedSize = true;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00027A38 File Offset: 0x00025C38
		[NullableContext(1)]
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(new Type[] { this.DictionaryKeyType, this.DictionaryValueType });
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { this._genericCollectionDefinitionType });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedDictionary)this._genericWrapperCreator(new object[] { dictionary });
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00027AC0 File Offset: 0x00025CC0
		[NullableContext(1)]
		internal IDictionary CreateTemporaryDictionary()
		{
			if (this._genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary<, >).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType ?? typeof(object),
					this.DictionaryValueType ?? typeof(object)
				});
				this._genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)this._genericTemporaryDictionaryCreator();
		}

		// Token: 0x04000547 RID: 1351
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x04000548 RID: 1352
		private Type _genericWrapperType;

		// Token: 0x04000549 RID: 1353
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x0400054A RID: 1354
		[Nullable(new byte[] { 2, 1 })]
		private Func<object> _genericTemporaryDictionaryCreator;

		// Token: 0x0400054C RID: 1356
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x0400054D RID: 1357
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x0400054E RID: 1358
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;
	}
}
