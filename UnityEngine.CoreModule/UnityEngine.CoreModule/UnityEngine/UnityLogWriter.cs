using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000141 RID: 321
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x00019BDC File Offset: 0x00017DDC
		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00019BFC File Offset: 0x00017DFC
		[FreeFunction(IsThreadSafe = true)]
		private unsafe static void WriteStringToUnityLogImpl(string s)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(s, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = s.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UnityLogWriter.WriteStringToUnityLogImpl_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00019C50 File Offset: 0x00017E50
		public static void Init()
		{
			TextWriter logWriter = TextWriter.Synchronized(new UnityLogWriter());
			Console.SetOut(logWriter);
			Console.SetError(logWriter);
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00019C78 File Offset: 0x00017E78
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00019C8F File Offset: 0x00017E8F
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00019C9F File Offset: 0x00017E9F
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00019CA9 File Offset: 0x00017EA9
		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}

		// Token: 0x06000D59 RID: 3417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteStringToUnityLogImpl_Injected(ref ManagedSpanWrapper s);
	}
}
