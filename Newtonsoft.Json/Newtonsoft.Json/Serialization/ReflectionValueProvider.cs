using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000138 RID: 312
	[NullableContext(1)]
	[Nullable(0)]
	public class ReflectionValueProvider : IValueProvider
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x0002EB79 File Offset: 0x0002CD79
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002EB94 File Offset: 0x0002CD94
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
				if (propertyInfo != null && propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
			return memberValue;
		}

		// Token: 0x040005BD RID: 1469
		private readonly MemberInfo _memberInfo;
	}
}
