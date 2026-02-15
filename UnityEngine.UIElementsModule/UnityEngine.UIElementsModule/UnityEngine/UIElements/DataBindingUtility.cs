using System;
using System.Text.RegularExpressions;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003C RID: 60
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class DataBindingUtility
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00009620 File Offset: 0x00007820
		public static bool TryGetBinding(VisualElement element, in BindingId bindingId, out BindingInfo bindingInfo)
		{
			Binding binding;
			bool flag = DataBindingManager.TryGetBindingRequest(element, in bindingId, out binding);
			bool flag3;
			if (flag)
			{
				bool flag2 = binding == null;
				if (flag2)
				{
					bindingInfo = default(BindingInfo);
					flag3 = false;
				}
				else
				{
					PropertyPath propertyPath = in bindingId;
					bindingInfo = BindingInfo.FromRequest(element, in propertyPath, binding);
					flag3 = true;
				}
			}
			else
			{
				bool flag4 = element.elementPanel != null;
				if (flag4)
				{
					DataBindingManager.BindingData bindingData;
					bool flag5 = element.elementPanel.dataBindingManager.TryGetBindingData(element, in bindingId, out bindingData);
					if (flag5)
					{
						bindingInfo = BindingInfo.FromBindingData(in bindingData);
						return true;
					}
				}
				bindingInfo = default(BindingInfo);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x04000136 RID: 310
		private static readonly ObjectPool<TypePathVisitor> k_TypeVisitors = new ObjectPool<TypePathVisitor>(() => new TypePathVisitor(), delegate(TypePathVisitor v)
		{
			v.Reset();
		}, null, null, true, 1, 10000);

		// Token: 0x04000137 RID: 311
		private static readonly ObjectPool<AutoCompletePathVisitor> k_AutoCompleteVisitors = new ObjectPool<AutoCompletePathVisitor>(() => new AutoCompletePathVisitor(), delegate(AutoCompletePathVisitor v)
		{
			v.Reset();
		}, null, null, true, 1, 10000);

		// Token: 0x04000138 RID: 312
		private static readonly Regex s_ReplaceIndices = new Regex("\\[[0-9]+\\]", RegexOptions.Compiled);
	}
}
