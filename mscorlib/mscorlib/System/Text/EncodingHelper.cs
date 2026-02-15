using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Text
{
	// Token: 0x0200030D RID: 781
	internal static class EncodingHelper
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0006E870 File Offset: 0x0006CA70
		internal static Encoding UTF8Unmarked
		{
			get
			{
				if (EncodingHelper.utf8EncodingWithoutMarkers == null)
				{
					object obj = EncodingHelper.lockobj;
					lock (obj)
					{
						if (EncodingHelper.utf8EncodingWithoutMarkers == null)
						{
							EncodingHelper.utf8EncodingWithoutMarkers = new UTF8Encoding(false, false);
							EncodingHelper.utf8EncodingWithoutMarkers.setReadOnly(true);
						}
					}
				}
				return EncodingHelper.utf8EncodingWithoutMarkers;
			}
		}

		// Token: 0x06001C45 RID: 7237
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalCodePage(ref int code_page);

		// Token: 0x06001C46 RID: 7238 RVA: 0x0006E8E0 File Offset: 0x0006CAE0
		internal static Encoding GetDefaultEncoding()
		{
			Encoding encoding = null;
			int num = 1;
			string text = EncodingHelper.InternalCodePage(ref num);
			try
			{
				if (num == -1)
				{
					encoding = Encoding.GetEncoding(text);
				}
				else
				{
					num &= 268435455;
					switch (num)
					{
					case 1:
						num = 20127;
						break;
					case 2:
						num = 65007;
						break;
					case 3:
						num = 65001;
						break;
					case 4:
						num = 1200;
						break;
					case 5:
						num = 1201;
						break;
					case 6:
						num = 1252;
						break;
					}
					encoding = Encoding.GetEncoding(num);
				}
			}
			catch (NotSupportedException)
			{
				encoding = EncodingHelper.UTF8Unmarked;
			}
			catch (ArgumentException)
			{
				encoding = EncodingHelper.UTF8Unmarked;
			}
			return encoding;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0006E998 File Offset: 0x0006CB98
		internal static object InvokeI18N(string name, params object[] args)
		{
			object obj = EncodingHelper.lockobj;
			object obj2;
			lock (obj)
			{
				if (EncodingHelper.i18nDisabled)
				{
					obj2 = null;
				}
				else
				{
					if (EncodingHelper.i18nAssembly == null)
					{
						try
						{
							try
							{
								EncodingHelper.i18nAssembly = Assembly.Load("I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
							}
							catch (NotImplementedException)
							{
								EncodingHelper.i18nDisabled = true;
								return null;
							}
							if (EncodingHelper.i18nAssembly == null)
							{
								return null;
							}
						}
						catch (SystemException)
						{
							return null;
						}
					}
					Type type;
					try
					{
						type = EncodingHelper.i18nAssembly.GetType("I18N.Common.Manager");
					}
					catch (NotImplementedException)
					{
						EncodingHelper.i18nDisabled = true;
						return null;
					}
					if (type == null)
					{
						obj2 = null;
					}
					else
					{
						object obj3;
						try
						{
							obj3 = type.InvokeMember("PrimaryManager", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, null, null, null);
							if (obj3 == null)
							{
								return null;
							}
						}
						catch (MissingMethodException)
						{
							return null;
						}
						catch (SecurityException)
						{
							return null;
						}
						catch (NotImplementedException)
						{
							EncodingHelper.i18nDisabled = true;
							return null;
						}
						try
						{
							obj2 = type.InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj3, args, null, null, null);
						}
						catch (MissingMethodException)
						{
							obj2 = null;
						}
						catch (SecurityException)
						{
							obj2 = null;
						}
					}
				}
			}
			return obj2;
		}

		// Token: 0x04000CFC RID: 3324
		private static volatile Encoding utf8EncodingWithoutMarkers;

		// Token: 0x04000CFD RID: 3325
		private static readonly object lockobj = new object();

		// Token: 0x04000CFE RID: 3326
		private static Assembly i18nAssembly;

		// Token: 0x04000CFF RID: 3327
		private static bool i18nDisabled;
	}
}
