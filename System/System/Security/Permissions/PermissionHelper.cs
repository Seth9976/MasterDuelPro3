using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x0200019B RID: 411
	internal sealed class PermissionHelper
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00033268 File Offset: 0x00031468
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000332C0 File Offset: 0x000314C0
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None && state != PermissionState.Unrestricted)
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("Invalid enum {0}"), state), "state");
			}
			return state;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000332EC File Offset: 0x000314EC
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0003331E File Offset: 0x0003151E
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(global::Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
