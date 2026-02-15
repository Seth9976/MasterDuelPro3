using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003C RID: 60
	public static class TaskTracker
	{
		// Token: 0x0600012A RID: 298 RVA: 0x000030EE File Offset: 0x000012EE
		[Conditional("UNITY_EDITOR")]
		public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000030EE File Offset: 0x000012EE
		[Conditional("UNITY_EDITOR")]
		public static void RemoveTracking(IUniTaskSource task)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000044B6 File Offset: 0x000026B6
		public static bool CheckAndResetDirty()
		{
			bool flag = TaskTracker.dirty;
			TaskTracker.dirty = false;
			return flag;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000044C4 File Offset: 0x000026C4
		public static void ForEachActiveTask(Action<int, string, UniTaskStatus, DateTime, string> action)
		{
			List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>> list = TaskTracker.listPool;
			lock (list)
			{
				int count = TaskTracker.tracking.ToList(ref TaskTracker.listPool, false);
				try
				{
					for (int i = 0; i < count; i++)
					{
						action(TaskTracker.listPool[i].Value.Item2, TaskTracker.listPool[i].Value.Item1, TaskTracker.listPool[i].Key.UnsafeGetStatus(), TaskTracker.listPool[i].Value.Item3, TaskTracker.listPool[i].Value.Item4);
						TaskTracker.listPool[i] = default(KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>);
					}
				}
				catch
				{
					TaskTracker.listPool.Clear();
					throw;
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000045D4 File Offset: 0x000027D4
		private static void TypeBeautify(Type type, StringBuilder sb)
		{
			if (type.IsNested)
			{
				sb.Append(type.DeclaringType.Name.ToString());
				sb.Append(".");
			}
			if (type.IsGenericType)
			{
				int genericsStart = type.Name.IndexOf("`");
				if (genericsStart != -1)
				{
					sb.Append(type.Name.Substring(0, genericsStart));
				}
				else
				{
					sb.Append(type.Name);
				}
				sb.Append("<");
				bool first = true;
				foreach (Type type2 in type.GetGenericArguments())
				{
					if (!first)
					{
						sb.Append(", ");
					}
					first = false;
					TaskTracker.TypeBeautify(type2, sb);
				}
				sb.Append(">");
				return;
			}
			sb.Append(type.Name);
		}

		// Token: 0x04000099 RID: 153
		[TupleElementNames(new string[] { "formattedType", "trackingId", "addTime", "stackTrace" })]
		private static List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>> listPool = new List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>>();

		// Token: 0x0400009A RID: 154
		[TupleElementNames(new string[] { "formattedType", "trackingId", "addTime", "stackTrace" })]
		private static readonly WeakDictionary<IUniTaskSource, ValueTuple<string, int, DateTime, string>> tracking = new WeakDictionary<IUniTaskSource, ValueTuple<string, int, DateTime, string>>(4, 0.75f, null);

		// Token: 0x0400009B RID: 155
		private static bool dirty;
	}
}
