using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C6 RID: 198
	[NullableContext(1)]
	[Nullable(0)]
	internal class FSharpUtils
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00020130 File Offset: 0x0001E330
		private FSharpUtils(Assembly fsharpCoreAssembly)
		{
			this.FSharpCoreAssembly = fsharpCoreAssembly;
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", BindingFlags.Static | BindingFlags.Public);
			this.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", BindingFlags.Static | BindingFlags.Public);
			this.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			this.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			this.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			this.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			this.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			this.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			this.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			this.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._ofSeq = type4.GetMethod("OfSeq");
			this._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00020279 File Offset: 0x0001E479
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00020280 File Offset: 0x0001E480
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00020288 File Offset: 0x0001E488
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00020291 File Offset: 0x0001E491
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00020299 File Offset: 0x0001E499
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> IsUnion
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000202A2 File Offset: 0x0001E4A2
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x000202AA File Offset: 0x0001E4AA
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> GetUnionCases
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000202B3 File Offset: 0x0001E4B3
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x000202BB File Offset: 0x0001E4BB
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionTagReader
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000202C4 File Offset: 0x0001E4C4
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x000202CC File Offset: 0x0001E4CC
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionReader
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000202D5 File Offset: 0x0001E4D5
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x000202DD File Offset: 0x0001E4DD
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionConstructor
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000202E6 File Offset: 0x0001E4E6
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x000202EE File Offset: 0x0001E4EE
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000202F7 File Offset: 0x0001E4F7
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x000202FF File Offset: 0x0001E4FF
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00020308 File Offset: 0x0001E508
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00020310 File Offset: 0x0001E510
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00020319 File Offset: 0x0001E519
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00020321 File Offset: 0x0001E521
		[Nullable(new byte[] { 1, 1, 2 })]
		public MethodCall<object, object> GetUnionCaseInfoFields
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 1, 1, 2 })]
			private set;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002032C File Offset: 0x0001E52C
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (FSharpUtils._instance == null)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (FSharpUtils._instance == null)
					{
						FSharpUtils._instance = new FSharpUtils(fsharpCoreAssembly);
					}
				}
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00020380 File Offset: 0x0001E580
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
			if (methodInfo == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				methodInfo = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return methodInfo;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000203B4 File Offset: 0x0001E5B4
		[return: Nullable(new byte[] { 1, 2, 1 })]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[] { 1, 2 })] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00020410 File Offset: 0x0001E610
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo methodInfo = this._ofSeq.MakeGenericMethod(new Type[] { t });
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002043E File Offset: 0x0001E63E
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(this, null);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00020474 File Offset: 0x0001E674
		[NullableContext(2)]
		[return: Nullable(1)]
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[] { typeof(IEnumerable<Tuple<TKey, TValue>>) });
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[] { 1, 2 })] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = ((IEnumerable<KeyValuePair<TKey, TValue>>)args[0]).Select((KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(new object[] { enumerable });
			};
		}

		// Token: 0x0400044B RID: 1099
		private static readonly object Lock = new object();

		// Token: 0x0400044C RID: 1100
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x0400044D RID: 1101
		private MethodInfo _ofSeq;

		// Token: 0x0400044E RID: 1102
		private Type _mapType;

		// Token: 0x04000459 RID: 1113
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x0400045A RID: 1114
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x0400045B RID: 1115
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}
