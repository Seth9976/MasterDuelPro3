using System;

namespace System
{
	/// <summary>Supports a value type that can be assigned null. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200012A RID: 298
	public static class Nullable
	{
		/// <summary>Returns the underlying type argument of the specified nullable type.</summary>
		/// <returns>The type argument of the <paramref name="nullableType" /> parameter, if the <paramref name="nullableType" /> parameter is a closed generic nullable type; otherwise, null. </returns>
		/// <param name="nullableType">A <see cref="T:System.Type" /> object that describes a closed generic nullable type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nullableType" /> is null.</exception>
		// Token: 0x060009CD RID: 2509 RVA: 0x00029A66 File Offset: 0x00027C66
		public static Type GetUnderlyingType(Type nullableType)
		{
			if (nullableType == null)
			{
				throw new ArgumentNullException("nullableType");
			}
			if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && nullableType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return nullableType.GetGenericArguments()[0];
			}
			return null;
		}
	}
}
