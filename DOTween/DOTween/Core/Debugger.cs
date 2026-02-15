using System;
using DG.Tweening.Core.Enums;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000A2 RID: 162
	public static class Debugger
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001069C File Offset: 0x0000E89C
		public static int logPriority
		{
			get
			{
				return Debugger._logPriority;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000106A4 File Offset: 0x0000E8A4
		public static void Log(object message)
		{
			string text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + ((message != null) ? message.ToString() : null);
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Log, text))
			{
				return;
			}
			Debug.Log(text);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public static void LogWarning(object message, Tween t = null)
		{
			string text;
			if (DOTween.debugMode)
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + Debugger.GetDebugDataMessage(t) + ((message != null) ? message.ToString() : null);
			}
			else
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + ((message != null) ? message.ToString() : null);
			}
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Warning, text))
			{
				return;
			}
			Debug.LogWarning(text);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001074C File Offset: 0x0000E94C
		public static void LogError(object message, Tween t = null)
		{
			string text;
			if (DOTween.debugMode)
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + Debugger.GetDebugDataMessage(t) + ((message != null) ? message.ToString() : null);
			}
			else
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + ((message != null) ? message.ToString() : null);
			}
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Error, text))
			{
				return;
			}
			Debug.LogError(text);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public static void LogSafeModeCapturedError(object message, Tween t = null)
		{
			string text;
			if (DOTween.debugMode)
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + Debugger.GetDebugDataMessage(t) + ((message != null) ? message.ToString() : null);
			}
			else
			{
				text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + ((message != null) ? message.ToString() : null);
			}
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Log, text))
			{
				return;
			}
			switch (DOTween.safeModeLogBehaviour)
			{
			case SafeModeLogBehaviour.Normal:
				Debug.Log(text);
				return;
			case SafeModeLogBehaviour.Warning:
				Debug.LogWarning(text);
				return;
			case SafeModeLogBehaviour.Error:
				Debug.LogError(text);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010844 File Offset: 0x0000EA44
		public static void LogReport(object message)
		{
			string text = string.Format("<color=#00B500FF>{0} REPORT ►</color> {1}", "<color=#0099bc><b>DOTWEEN ► </b></color>", message);
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Log, text))
			{
				return;
			}
			Debug.Log(text);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010880 File Offset: 0x0000EA80
		public static void LogSafeModeReport(object message)
		{
			string text = string.Format("<color=#ff7337>{0} SAFE MODE ►</color> {1}", "<color=#0099bc><b>DOTWEEN ► </b></color>", message);
			if (DOTween.onWillLog != null && !DOTween.onWillLog.Invoke(LogType.Log, text))
			{
				return;
			}
			Debug.LogWarning(text);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000108BA File Offset: 0x0000EABA
		public static void LogInvalidTween(Tween t)
		{
			Debugger.LogWarning("This Tween has been killed and is now invalid", null);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000108C7 File Offset: 0x0000EAC7
		public static void LogNestedTween(Tween t)
		{
			Debugger.LogWarning("This Tween was added to a Sequence and can't be controlled directly", t);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public static void LogNullTween(Tween t)
		{
			Debugger.LogWarning("Null Tween", null);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000108E1 File Offset: 0x0000EAE1
		public static void LogNonPathTween(Tween t)
		{
			Debugger.LogWarning("This Tween is not a path tween", t);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000108EE File Offset: 0x0000EAEE
		public static void LogMissingMaterialProperty(string propertyName)
		{
			Debugger.LogWarning(string.Format("This material doesn't have a {0} property", propertyName), null);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010901 File Offset: 0x0000EB01
		public static void LogMissingMaterialProperty(int propertyId)
		{
			Debugger.LogWarning(string.Format("This material doesn't have a {0} property ID", propertyId), null);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010919 File Offset: 0x0000EB19
		public static void LogRemoveActiveTweenError(string errorInfo, Tween t)
		{
			Debugger.LogWarning(string.Format("Error in RemoveActiveTween ({0}). It's been taken care of so no problems, but Daniele (DOTween's author) is trying to pinpoint it (it's very rare and he can't reproduce it) so it would be awesome if you could reproduce this log in a sample project and send it to him. Or even just write him the complete log that was generated by this message. Fixing this would make DOTween slightly faster. Thanks.", errorInfo), t);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static void LogAddActiveTweenError(string errorInfo, Tween t)
		{
			Debugger.LogWarning(string.Format("Error in AddActiveTween ({0}). It's been taken care of so no problems, but Daniele (DOTween's author) is trying to pinpoint it (it's very rare and he can't reproduce it) so it would be awesome if you could reproduce this log in a sample project and send it to him. Or even just write him the complete log that was generated by this message. Fixing this would make DOTween slightly faster. Thanks.", errorInfo), t);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001093F File Offset: 0x0000EB3F
		public static void SetLogPriority(LogBehaviour logBehaviour)
		{
			if (logBehaviour == LogBehaviour.Default)
			{
				Debugger._logPriority = 1;
				return;
			}
			if (logBehaviour != LogBehaviour.Verbose)
			{
				Debugger._logPriority = 0;
				return;
			}
			Debugger._logPriority = 2;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010960 File Offset: 0x0000EB60
		public static bool ShouldLogSafeModeCapturedError()
		{
			SafeModeLogBehaviour safeModeLogBehaviour = DOTween.safeModeLogBehaviour;
			return safeModeLogBehaviour != SafeModeLogBehaviour.None && (safeModeLogBehaviour - SafeModeLogBehaviour.Normal > 1 || Debugger._logPriority >= 1);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010990 File Offset: 0x0000EB90
		private static string GetDebugDataMessage(Tween t)
		{
			string text = "";
			Debugger.AddDebugDataToMessage(ref text, t);
			return text;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000109AC File Offset: 0x0000EBAC
		private static void AddDebugDataToMessage(ref string message, Tween t)
		{
			if (t == null)
			{
				return;
			}
			bool flag = t.debugTargetId != null;
			bool flag2 = t.stringId != null;
			bool flag3 = t.intId != -999;
			if (flag || flag2 || flag3)
			{
				message += "DEBUG MODE INFO ► ";
				if (flag)
				{
					message += string.Format("[tween target: {0}]", t.debugTargetId);
				}
				if (flag2)
				{
					message += string.Format("[stringId: {0}]", t.stringId);
				}
				if (flag3)
				{
					message += string.Format("[intId: {0}]", t.intId);
				}
				message += "\n";
			}
		}

		// Token: 0x040001C8 RID: 456
		private static int _logPriority;

		// Token: 0x040001C9 RID: 457
		private const string _LogPrefix = "<color=#0099bc><b>DOTWEEN ► </b></color>";

		// Token: 0x020000A3 RID: 163
		internal static class Sequence
		{
			// Token: 0x060003D2 RID: 978 RVA: 0x00010A5E File Offset: 0x0000EC5E
			public static void LogAddToNullSequence()
			{
				Debugger.LogWarning("You can't add elements to a NULL Sequence", null);
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x00010A6B File Offset: 0x0000EC6B
			public static void LogAddToInactiveSequence()
			{
				Debugger.LogWarning("You can't add elements to an inactive/killed Sequence", null);
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x00010A78 File Offset: 0x0000EC78
			public static void LogAddToLockedSequence()
			{
				Debugger.LogWarning("The Sequence has started and is now locked, you can only elements to a Sequence before it starts", null);
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x00010A85 File Offset: 0x0000EC85
			public static void LogAddNullTween()
			{
				Debugger.LogWarning("You can't add a NULL tween to a Sequence", null);
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x00010A92 File Offset: 0x0000EC92
			public static void LogAddInactiveTween(Tween t)
			{
				Debugger.LogWarning("You can't add an inactive/killed tween to a Sequence", t);
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x00010A9F File Offset: 0x0000EC9F
			public static void LogAddAlreadySequencedTween(Tween t)
			{
				Debugger.LogWarning("You can't add a tween that is already nested into a Sequence to another Sequence", t);
			}
		}
	}
}
