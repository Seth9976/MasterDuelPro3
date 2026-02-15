using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000A1 RID: 161
	[ReflectedPropertyBag]
	internal class ReflectedPropertyBag<TContainer> : ContainerPropertyBag<TContainer>
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000AD10 File Offset: 0x00008F10
		internal new void AddProperty<TValue>(Property<TContainer, TValue> property)
		{
			TContainer container = default(TContainer);
			IProperty<TContainer> existing;
			bool flag = base.TryGetProperty(ref container, property.Name, out existing);
			if (flag)
			{
				bool flag2 = existing.DeclaredValueType() == typeof(TValue);
				if (!flag2)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Detected multiple return types for PropertyBag=[",
						TypeUtility.GetTypeDisplayName(typeof(TContainer)),
						"] Property=[",
						property.Name,
						"]. The property will use the most derived Type=[",
						TypeUtility.GetTypeDisplayName(existing.DeclaredValueType()),
						"] and IgnoreType=[",
						TypeUtility.GetTypeDisplayName(property.DeclaredValueType()),
						"]."
					}));
				}
			}
			else
			{
				base.AddProperty<TValue>(property);
			}
		}
	}
}
