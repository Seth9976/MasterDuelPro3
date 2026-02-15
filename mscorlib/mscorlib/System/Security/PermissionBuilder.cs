using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000322 RID: 802
	internal static class PermissionBuilder
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x0006F250 File Offset: 0x0006D450
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0006F2B0 File Offset: 0x0006D4B0
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0006F2F4 File Offset: 0x0006D4F4
		public static IPermission Create(string fullname, SecurityElement se)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			return PermissionBuilder.CreatePermission(fullname, se);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006F319 File Offset: 0x0006D519
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006F32B File Offset: 0x0006D52B
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				throw new TypeLoadException(string.Format(Locale.GetText("Can't create an instance of permission class {0}."), fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x04000D1F RID: 3359
		private static object[] psNone = new object[] { PermissionState.None };
	}
}
