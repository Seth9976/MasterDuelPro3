using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000112 RID: 274
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonArrayContract : JsonContainerContract
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00026AE1 File Offset: 0x00024CE1
		public Type CollectionItemType { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00026AE9 File Offset: 0x00024CE9
		public bool IsMultidimensionalArray { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00026AF1 File Offset: 0x00024CF1
		internal bool IsArray { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00026AF9 File Offset: 0x00024CF9
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00026B01 File Offset: 0x00024D01
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00026B09 File Offset: 0x00024D09
		internal bool CanDeserialize { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00026B12 File Offset: 0x00024D12
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00026B46 File Offset: 0x00024D46
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00026B4E File Offset: 0x00024D4E
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
				this.CanDeserialize = true;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00026B5E File Offset: 0x00024D5E
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00026B66 File Offset: 0x00024D66
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00026B6F File Offset: 0x00024D6F
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00026B90 File Offset: 0x00024D90
		[NullableContext(1)]
		public JsonArrayContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Array;
			this.IsArray = base.CreatedType.IsArray || (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Linq.EmptyPartition`1");
			bool flag;
			Type type;
			if (this.IsArray)
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[] { this.CollectionItemType });
				flag = true;
				this.IsMultidimensionalArray = base.CreatedType.IsArray && base.UnderlyingType.GetArrayRank() > 1;
			}
			else if (typeof(IList).IsAssignableFrom(this.NonNullableUnderlyingType))
			{
				if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
				{
					this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					this.CollectionItemType = ReflectionUtils.GetCollectionItemType(this.NonNullableUnderlyingType);
				}
				if (this.NonNullableUnderlyingType == typeof(IList))
				{
					base.CreatedType = typeof(List<object>);
				}
				if (this.CollectionItemType != null)
				{
					this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(this.NonNullableUnderlyingType, typeof(ReadOnlyCollection<>));
				flag = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>)) || ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IList<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ISet<>)))
				{
					base.CreatedType = typeof(HashSet<>).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				flag = true;
				this.ShouldCreateWrapper = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyCollection<>), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyCollection<>)) || ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyList<>)))
				{
					base.CreatedType = typeof(ReadOnlyCollection<>).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[] { this.CollectionItemType });
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(this.NonNullableUnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				flag = this.HasParameterizedCreatorInternal;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IEnumerable<>), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IEnumerable<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(this.NonNullableUnderlyingType);
				if (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					this._genericCollectionDefinitionType = type;
					this.IsReadOnlyOrFixedSize = false;
					this.ShouldCreateWrapper = false;
					flag = true;
				}
				else
				{
					this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[] { this.CollectionItemType });
					this.IsReadOnlyOrFixedSize = true;
					this.ShouldCreateWrapper = true;
					flag = this.HasParameterizedCreatorInternal;
				}
			}
			else
			{
				flag = false;
				this.ShouldCreateWrapper = true;
			}
			this.CanDeserialize = flag;
			Type type2;
			ObjectConstructor<object> objectConstructor;
			if (this.CollectionItemType != null && ImmutableCollectionsUtils.TryBuildImmutableForArrayContract(this.NonNullableUnderlyingType, this.CollectionItemType, out type2, out objectConstructor))
			{
				base.CreatedType = type2;
				this._parameterizedCreator = objectConstructor;
				this.IsReadOnlyOrFixedSize = true;
				this.CanDeserialize = true;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00027038 File Offset: 0x00025238
		[NullableContext(1)]
		internal IWrappedCollection CreateWrapper(object list)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(CollectionWrapper<>).MakeGenericType(new Type[] { this.CollectionItemType });
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List<>)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					type = typeof(ICollection<>).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { type });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedCollection)this._genericWrapperCreator(new object[] { list });
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00027110 File Offset: 0x00025310
		[NullableContext(1)]
		internal IList CreateTemporaryCollection()
		{
			if (this._genericTemporaryCollectionCreator == null)
			{
				Type type = ((this.IsMultidimensionalArray || this.CollectionItemType == null) ? typeof(object) : this.CollectionItemType);
				Type type2 = typeof(List<>).MakeGenericType(new Type[] { type });
				this._genericTemporaryCollectionCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (IList)this._genericTemporaryCollectionCreator();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00027189 File Offset: 0x00025389
		[NullableContext(1)]
		private void StoreFSharpListCreatorIfNecessary(Type underlyingType)
		{
			if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpList`1")
			{
				FSharpUtils.EnsureInitialized(underlyingType.Assembly());
				this._parameterizedCreator = FSharpUtils.Instance.CreateSeq(this.CollectionItemType);
			}
		}

		// Token: 0x04000511 RID: 1297
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x04000512 RID: 1298
		private Type _genericWrapperType;

		// Token: 0x04000513 RID: 1299
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x04000514 RID: 1300
		[Nullable(new byte[] { 2, 1 })]
		private Func<object> _genericTemporaryCollectionCreator;

		// Token: 0x04000518 RID: 1304
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x04000519 RID: 1305
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x0400051A RID: 1306
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;
	}
}
