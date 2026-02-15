using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D5 RID: 213
	[NullableContext(1)]
	[Nullable(0)]
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00021C87 File Offset: 0x0001FE87
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00021C90 File Offset: 0x0001FE90
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return ([Nullable(new byte[] { 1, 2 })] object[] a) => c.Invoke(a);
			}
			return ([Nullable(new byte[] { 1, 2 })] object[] a) => method.Invoke(null, a);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021CEC File Offset: 0x0001FEEC
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, [Nullable(new byte[] { 1, 2 })] object[] a) => c.Invoke(a);
			}
			return (T o, [Nullable(new byte[] { 1, 2 })] object[] a) => method.Invoke(o, a);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00021D48 File Offset: 0x0001FF48
		public override Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			if (constructorInfo == null)
			{
				throw new InvalidOperationException("Unable to find default constructor for " + type.FullName);
			}
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00021DD3 File Offset: 0x0001FFD3
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return (T o) => propertyInfo.GetValue(o, null);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00021DFC File Offset: 0x0001FFFC
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return (T o) => fieldInfo.GetValue(o);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021E25 File Offset: 0x00020025
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021E4E File Offset: 0x0002004E
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Token: 0x040004A6 RID: 1190
		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();
	}
}
