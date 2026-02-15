using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000157 RID: 343
	[NullableContext(1)]
	[Nullable(0)]
	public static class Extensions
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00032ACD File Offset: 0x00030CCD
		public static IJEnumerable<JToken> Ancestors<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Ancestors()).AsJEnumerable();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00032B04 File Offset: 0x00030D04
		public static IJEnumerable<JToken> AncestorsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00032B3B File Offset: 0x00030D3B
		public static IJEnumerable<JToken> Descendants<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Descendants()).AsJEnumerable();
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00032B72 File Offset: 0x00030D72
		public static IJEnumerable<JToken> DescendantsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00032BA9 File Offset: 0x00030DA9
		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00032BE0 File Offset: 0x00030DE0
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, [Nullable(2)] object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00032BEE File Offset: 0x00030DEE
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00032BF7 File Offset: 0x00030DF7
		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00032C00 File Offset: 0x00030E00
		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00032C09 File Offset: 0x00030E09
		[NullableContext(2)]
		public static U Value<U>([Nullable(1)] this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00032C11 File Offset: 0x00030E11
		[return: Nullable(2)]
		public static U Value<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00032C37 File Offset: 0x00030E37
		[return: Nullable(new byte[] { 1, 2 })]
		internal static IEnumerable<U> Values<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source, [Nullable(2)] object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			if (key == null)
			{
				foreach (T t in source)
				{
					JValue jvalue = t as JValue;
					if (jvalue != null)
					{
						yield return jvalue.Convert<JValue, U>();
					}
					else
					{
						foreach (JToken jtoken in t.Children())
						{
							yield return jtoken.Convert<JToken, U>();
						}
						IEnumerator<JToken> enumerator2 = null;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			else
			{
				foreach (T t2 in source)
				{
					JToken jtoken2 = t2[key];
					if (jtoken2 != null)
					{
						yield return jtoken2.Convert<JToken, U>();
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00032C4E File Offset: 0x00030E4E
		public static IJEnumerable<JToken> Children<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00032C5B File Offset: 0x00030E5B
		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Children<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T c) => c.Children()).Convert<JToken, U>();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00032C92 File Offset: 0x00030E92
		[return: Nullable(new byte[] { 1, 2 })]
		internal static IEnumerable<U> Convert<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t in source)
			{
				yield return t.Convert<JToken, U>();
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00032CA4 File Offset: 0x00030EA4
		[NullableContext(2)]
		internal static U Convert<[Nullable(0)] T, U>([Nullable(1)] this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U u = token as U;
				if (typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
				{
					return u;
				}
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value = jvalue.Value;
			if (value is U)
			{
				return (U)((object)value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)global::System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00032DB4 File Offset: 0x00030FB4
		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00032DBC File Offset: 0x00030FBC
		public static IJEnumerable<T> AsJEnumerable<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			IJEnumerable<T> ijenumerable = source as IJEnumerable<T>;
			if (ijenumerable != null)
			{
				return ijenumerable;
			}
			return new JEnumerable<T>(source);
		}
	}
}
