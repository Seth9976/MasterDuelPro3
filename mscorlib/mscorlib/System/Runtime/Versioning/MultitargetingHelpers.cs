using System;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x020004A3 RID: 1187
	internal static class MultitargetingHelpers
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x0009A694 File Offset: 0x00098894
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = type.AssemblyQualifiedName;
				}
			}
			return text;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0009A6E0 File Offset: 0x000988E0
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0009A70D File Offset: 0x0009890D
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}
	}
}
