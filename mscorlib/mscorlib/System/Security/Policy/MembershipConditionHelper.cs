using System;

namespace System.Security.Policy
{
	// Token: 0x02000344 RID: 836
	internal sealed class MembershipConditionHelper
	{
		// Token: 0x06001D7D RID: 7549 RVA: 0x00073D70 File Offset: 0x00071F70
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != MembershipConditionHelper.XmlTag)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}, expected {1}."), se.Tag, MembershipConditionHelper.XmlTag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			return num;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00073E00 File Offset: 0x00072000
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement(MembershipConditionHelper.XmlTag);
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x04000DA1 RID: 3489
		private static readonly string XmlTag = "IMembershipCondition";
	}
}
