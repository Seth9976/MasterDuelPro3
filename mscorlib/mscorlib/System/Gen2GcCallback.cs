using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000E5 RID: 229
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0001E7DB File Offset: 0x0001C9DB
		private Gen2GcCallback()
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001E7E3 File Offset: 0x0001C9E3
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			new Gen2GcCallback().Setup(callback, targetObj);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001E7F1 File Offset: 0x0001C9F1
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this._callback = callback;
			this._weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001E808 File Offset: 0x0001CA08
		protected override void Finalize()
		{
			try
			{
				object target = this._weakTargetObj.Target;
				if (target == null)
				{
					this._weakTargetObj.Free();
				}
				else
				{
					try
					{
						if (!this._callback(target))
						{
							return;
						}
					}
					catch
					{
					}
					if (!Environment.HasShutdownStarted)
					{
						GC.ReRegisterForFinalize(this);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000336 RID: 822
		private Func<object, bool> _callback;

		// Token: 0x04000337 RID: 823
		private GCHandle _weakTargetObj;
	}
}
