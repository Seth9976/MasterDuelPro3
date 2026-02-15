using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CA RID: 202
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableCollectionsUtils
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00020580 File Offset: 0x0001E780
		internal static bool TryBuildImmutableForArrayContract(Type underlyingType, Type collectionItemType, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Type createdType, [Nullable(new byte[] { 2, 1 })] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out ObjectConstructor<object> parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = ImmutableCollectionsUtils.ArrayContractImmutableCollectionDefinitions.FirstOrDefault((ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						MethodInfo methodInfo = type2.GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "CreateRange" && m.GetParameters().Length == 1);
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(new Type[] { collectionItemType });
							MethodInfo methodInfo2 = methodInfo.MakeGenericMethod(new Type[] { collectionItemType });
							parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo2);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002067C File Offset: 0x0001E87C
		internal static bool TryBuildImmutableForDictionaryContract(Type underlyingType, Type keyItemType, Type valueItemType, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Type createdType, [Nullable(new byte[] { 2, 1 })] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out ObjectConstructor<object> parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = ImmutableCollectionsUtils.DictionaryContractImmutableCollectionDefinitions.FirstOrDefault((ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						MethodInfo methodInfo = type2.GetMethods().FirstOrDefault(delegate(MethodInfo m)
						{
							ParameterInfo[] parameters = m.GetParameters();
							return m.Name == "CreateRange" && parameters.Length == 1 && parameters[0].ParameterType.IsGenericType() && parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
						});
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(new Type[] { keyItemType, valueItemType });
							MethodInfo methodInfo2 = methodInfo.MakeGenericMethod(new Type[] { keyItemType, valueItemType });
							parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo2);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}

		// Token: 0x04000461 RID: 1121
		private const string ImmutableListGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableList`1";

		// Token: 0x04000462 RID: 1122
		private const string ImmutableQueueGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableQueue`1";

		// Token: 0x04000463 RID: 1123
		private const string ImmutableStackGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableStack`1";

		// Token: 0x04000464 RID: 1124
		private const string ImmutableSetGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableSet`1";

		// Token: 0x04000465 RID: 1125
		private const string ImmutableArrayTypeName = "System.Collections.Immutable.ImmutableArray";

		// Token: 0x04000466 RID: 1126
		private const string ImmutableArrayGenericTypeName = "System.Collections.Immutable.ImmutableArray`1";

		// Token: 0x04000467 RID: 1127
		private const string ImmutableListTypeName = "System.Collections.Immutable.ImmutableList";

		// Token: 0x04000468 RID: 1128
		private const string ImmutableListGenericTypeName = "System.Collections.Immutable.ImmutableList`1";

		// Token: 0x04000469 RID: 1129
		private const string ImmutableQueueTypeName = "System.Collections.Immutable.ImmutableQueue";

		// Token: 0x0400046A RID: 1130
		private const string ImmutableQueueGenericTypeName = "System.Collections.Immutable.ImmutableQueue`1";

		// Token: 0x0400046B RID: 1131
		private const string ImmutableStackTypeName = "System.Collections.Immutable.ImmutableStack";

		// Token: 0x0400046C RID: 1132
		private const string ImmutableStackGenericTypeName = "System.Collections.Immutable.ImmutableStack`1";

		// Token: 0x0400046D RID: 1133
		private const string ImmutableSortedSetTypeName = "System.Collections.Immutable.ImmutableSortedSet";

		// Token: 0x0400046E RID: 1134
		private const string ImmutableSortedSetGenericTypeName = "System.Collections.Immutable.ImmutableSortedSet`1";

		// Token: 0x0400046F RID: 1135
		private const string ImmutableHashSetTypeName = "System.Collections.Immutable.ImmutableHashSet";

		// Token: 0x04000470 RID: 1136
		private const string ImmutableHashSetGenericTypeName = "System.Collections.Immutable.ImmutableHashSet`1";

		// Token: 0x04000471 RID: 1137
		private static readonly IList<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> ArrayContractImmutableCollectionDefinitions = new List<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
		{
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableList`1", "System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableStack`1", "System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableSet`1", "System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableSortedSet`1", "System.Collections.Immutable.ImmutableSortedSet`1", "System.Collections.Immutable.ImmutableSortedSet"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableArray`1", "System.Collections.Immutable.ImmutableArray`1", "System.Collections.Immutable.ImmutableArray")
		};

		// Token: 0x04000472 RID: 1138
		private const string ImmutableDictionaryGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableDictionary`2";

		// Token: 0x04000473 RID: 1139
		private const string ImmutableDictionaryTypeName = "System.Collections.Immutable.ImmutableDictionary";

		// Token: 0x04000474 RID: 1140
		private const string ImmutableDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableDictionary`2";

		// Token: 0x04000475 RID: 1141
		private const string ImmutableSortedDictionaryTypeName = "System.Collections.Immutable.ImmutableSortedDictionary";

		// Token: 0x04000476 RID: 1142
		private const string ImmutableSortedDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableSortedDictionary`2";

		// Token: 0x04000477 RID: 1143
		private static readonly IList<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> DictionaryContractImmutableCollectionDefinitions = new List<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
		{
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableSortedDictionary`2", "System.Collections.Immutable.ImmutableSortedDictionary`2", "System.Collections.Immutable.ImmutableSortedDictionary"),
			new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary")
		};

		// Token: 0x020000CB RID: 203
		[Nullable(0)]
		internal class ImmutableCollectionTypeInfo
		{
			// Token: 0x06000635 RID: 1589 RVA: 0x000208F3 File Offset: 0x0001EAF3
			public ImmutableCollectionTypeInfo(string contractTypeName, string createdTypeName, string builderTypeName)
			{
				this.ContractTypeName = contractTypeName;
				this.CreatedTypeName = createdTypeName;
				this.BuilderTypeName = builderTypeName;
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000636 RID: 1590 RVA: 0x00020910 File Offset: 0x0001EB10
			// (set) Token: 0x06000637 RID: 1591 RVA: 0x00020918 File Offset: 0x0001EB18
			public string ContractTypeName { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x00020921 File Offset: 0x0001EB21
			// (set) Token: 0x06000639 RID: 1593 RVA: 0x00020929 File Offset: 0x0001EB29
			public string CreatedTypeName { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x00020932 File Offset: 0x0001EB32
			// (set) Token: 0x0600063B RID: 1595 RVA: 0x0002093A File Offset: 0x0001EB3A
			public string BuilderTypeName { get; set; }
		}
	}
}
