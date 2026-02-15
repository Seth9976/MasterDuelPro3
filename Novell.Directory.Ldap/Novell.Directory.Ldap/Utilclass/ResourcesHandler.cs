using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200005F RID: 95
	public class ResourcesHandler
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00002499 File Offset: 0x00000699
		private ResourcesHandler()
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000F86B File Offset: 0x0000DA6B
		public static string getMessage(string messageOrKey, object[] arguments)
		{
			return ResourcesHandler.getMessage(messageOrKey, arguments, null);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000F878 File Offset: 0x0000DA78
		public static string getMessage(string messageOrKey, object[] arguments, CultureInfo locale)
		{
			if (ResourcesHandler.defaultMessages == null)
			{
				ResourcesHandler.defaultMessages = new ResourceManager("ExceptionMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			if (messageOrKey == null)
			{
				messageOrKey = "";
			}
			string text;
			try
			{
				text = ResourcesHandler.defaultMessages.GetString(messageOrKey, locale);
			}
			catch (MissingManifestResourceException)
			{
				text = messageOrKey;
			}
			if (arguments != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(text, arguments);
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000F908 File Offset: 0x0000DB08
		public static string getResultString(int code)
		{
			return ResourcesHandler.getResultString(code, null);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000F914 File Offset: 0x0000DB14
		public static string getResultString(int code, CultureInfo locale)
		{
			if (ResourcesHandler.defaultResultCodes == null)
			{
				ResourcesHandler.defaultResultCodes = new ResourceManager("ResultCodeMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			string text;
			try
			{
				text = ResourcesHandler.defaultResultCodes.GetString(Convert.ToString(code), ResourcesHandler.defaultLocale);
			}
			catch (ArgumentNullException)
			{
				text = ResourcesHandler.getMessage("UNKNOWN_RESULT", new object[] { code }, locale);
			}
			return text;
		}

		// Token: 0x0400021B RID: 539
		private static ResourceManager defaultResultCodes = null;

		// Token: 0x0400021C RID: 540
		private static ResourceManager defaultMessages = null;

		// Token: 0x0400021D RID: 541
		private static string pkg = "Novell.Directory.Ldap.Utilclass.";

		// Token: 0x0400021E RID: 542
		private static CultureInfo defaultLocale = Thread.CurrentThread.CurrentUICulture;
	}
}
