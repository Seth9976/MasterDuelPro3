using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000055 RID: 85
	public abstract class InputBindingComposite
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003FF RID: 1023
		public abstract Type valueType { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000400 RID: 1024
		public abstract int valueSizeInBytes { get; }

		// Token: 0x06000401 RID: 1025
		public unsafe abstract void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize);

		// Token: 0x06000402 RID: 1026
		public abstract object ReadValueAsObject(ref InputBindingCompositeContext context);

		// Token: 0x06000403 RID: 1027 RVA: 0x00010C8F File Offset: 0x0000EE8F
		public virtual float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return -1f;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void FinishSetup(ref InputBindingCompositeContext context)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010C96 File Offset: 0x0000EE96
		internal void CallFinishSetup(ref InputBindingCompositeContext context)
		{
			this.FinishSetup(ref context);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		internal static Type GetValueType(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type compositeType = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (compositeType == null)
			{
				return null;
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(compositeType, typeof(InputBindingComposite<>), 0);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public static string GetExpectedControlLayoutName(string composite, string part)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			if (string.IsNullOrEmpty(part))
			{
				throw new ArgumentNullException("part");
			}
			Type compositeType = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (compositeType == null)
			{
				return null;
			}
			FieldInfo field = compositeType.GetField(part, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (field == null)
			{
				return null;
			}
			InputControlAttribute customAttribute = field.GetCustomAttribute(false);
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.layout;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010D59 File Offset: 0x0000EF59
		internal static IEnumerable<string> GetPartNames(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type compositeType = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (compositeType == null)
			{
				yield break;
			}
			foreach (FieldInfo field in compositeType.GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (field.GetCustomAttribute<InputControlAttribute>() != null)
				{
					yield return field.Name;
				}
			}
			FieldInfo[] array = null;
			yield break;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010D6C File Offset: 0x0000EF6C
		internal static string GetDisplayFormatString(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type compositeType = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (compositeType == null)
			{
				return null;
			}
			DisplayStringFormatAttribute displayFormatAttribute = compositeType.GetCustomAttribute<DisplayStringFormatAttribute>();
			if (displayFormatAttribute == null)
			{
				return null;
			}
			return displayFormatAttribute.formatString;
		}

		// Token: 0x04000201 RID: 513
		internal static TypeTable s_Composites;
	}
}
