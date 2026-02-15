using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001B RID: 27
	internal static class InputInteraction
	{
		// Token: 0x0600012F RID: 303 RVA: 0x000032E4 File Offset: 0x000014E4
		public static Type GetValueType(Type interactionType)
		{
			if (interactionType == null)
			{
				throw new ArgumentNullException("interactionType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(interactionType, typeof(IInputInteraction<>), 0);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000330C File Offset: 0x0000150C
		public static string GetDisplayName(string interaction)
		{
			if (string.IsNullOrEmpty(interaction))
			{
				throw new ArgumentNullException("interaction");
			}
			Type interactionType = InputInteraction.s_Interactions.LookupTypeRegistration(interaction);
			if (interactionType == null)
			{
				return interaction;
			}
			return InputInteraction.GetDisplayName(interactionType);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000334C File Offset: 0x0000154C
		public static string GetDisplayName(Type interactionType)
		{
			if (interactionType == null)
			{
				throw new ArgumentNullException("interactionType");
			}
			DisplayNameAttribute displayNameAttribute = interactionType.GetCustomAttribute<DisplayNameAttribute>();
			if (displayNameAttribute != null)
			{
				return displayNameAttribute.DisplayName;
			}
			if (interactionType.Name.EndsWith("Interaction"))
			{
				return interactionType.Name.Substring(0, interactionType.Name.Length - "Interaction".Length);
			}
			return interactionType.Name;
		}

		// Token: 0x04000084 RID: 132
		public static TypeTable s_Interactions;
	}
}
