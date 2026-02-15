using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000025 RID: 37
	public class ConverterGroup
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003F4F File Offset: 0x0000214F
		internal TypeConverterRegistry registry { get; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F57 File Offset: 0x00002157
		public ConverterGroup(string id, string displayName = null, string description = null)
		{
			this.<id>k__BackingField = id;
			this.<displayName>k__BackingField = displayName;
			this.<description>k__BackingField = description;
			this.registry = TypeConverterRegistry.Create();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F84 File Offset: 0x00002184
		public bool TryConvert<TSource, TDestination>(ref TSource source, out TDestination destination)
		{
			Delegate converter;
			TypeConverter<TSource, TDestination> typedConverter;
			bool flag;
			if (this.registry.TryGetConverter(typeof(TSource), typeof(TDestination), out converter))
			{
				typedConverter = converter as TypeConverter<TSource, TDestination>;
				flag = typedConverter != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			bool flag3;
			if (flag2)
			{
				destination = typedConverter(ref source);
				flag3 = true;
			}
			else
			{
				destination = default(TDestination);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FEC File Offset: 0x000021EC
		public bool TrySetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, TValue value, out VisitReturnCode returnCode)
		{
			bool isEmpty = path.IsEmpty;
			bool flag;
			if (isEmpty)
			{
				returnCode = VisitReturnCode.InvalidPath;
				flag = false;
			}
			else
			{
				SetValueVisitor<TValue> visitor = SetValueVisitor<TValue>.Pool.Get();
				visitor.group = this;
				visitor.Path = path;
				visitor.Value = value;
				try
				{
					bool flag2 = !PropertyContainer.TryAccept<TContainer>(visitor, ref container, out returnCode, default(VisitParameters));
					if (flag2)
					{
						return false;
					}
					returnCode = visitor.ReturnCode;
				}
				finally
				{
					SetValueVisitor<TValue>.Pool.Release(visitor);
				}
				flag = returnCode == VisitReturnCode.Ok;
			}
			return flag;
		}
	}
}
