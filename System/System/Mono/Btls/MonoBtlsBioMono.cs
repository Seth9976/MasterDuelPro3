using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x0200009B RID: 155
	internal class MonoBtlsBioMono : MonoBtlsBio
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00009AFC File Offset: 0x00007CFC
		public MonoBtlsBioMono(IMonoBtlsBioMono backend)
			: base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMono.mono_btls_bio_mono_new()))
		{
			this.backend = backend;
			this.handle = GCHandle.Alloc(this);
			this.instance = GCHandle.ToIntPtr(this.handle);
			this.readFunc = new MonoBtlsBioMono.BioReadFunc(MonoBtlsBioMono.OnRead);
			this.writeFunc = new MonoBtlsBioMono.BioWriteFunc(MonoBtlsBioMono.OnWrite);
			this.controlFunc = new MonoBtlsBioMono.BioControlFunc(MonoBtlsBioMono.Control);
			this.readFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioReadFunc>(this.readFunc);
			this.writeFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioWriteFunc>(this.writeFunc);
			this.controlFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioControlFunc>(this.controlFunc);
			MonoBtlsBioMono.mono_btls_bio_mono_initialize(base.Handle.DangerousGetHandle(), this.instance, this.readFuncPtr, this.writeFuncPtr, this.controlFuncPtr);
		}

		// Token: 0x0600026D RID: 621
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mono_new();

		// Token: 0x0600026E RID: 622
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_mono_initialize(IntPtr handle, IntPtr instance, IntPtr readFunc, IntPtr writeFunc, IntPtr controlFunc);

		// Token: 0x0600026F RID: 623 RVA: 0x00009BCE File Offset: 0x00007DCE
		private long Control(MonoBtlsBioMono.ControlCommand command, long arg)
		{
			if (command == MonoBtlsBioMono.ControlCommand.Flush)
			{
				this.backend.Flush();
				return 1L;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009BE8 File Offset: 0x00007DE8
		private int OnRead(IntPtr data, int dataLength, out int wantMore)
		{
			byte[] array = new byte[dataLength];
			bool flag;
			int num = this.backend.Read(array, 0, dataLength, out flag);
			wantMore = (flag ? 1 : 0);
			if (num <= 0)
			{
				return num;
			}
			Marshal.Copy(array, 0, data, num);
			return num;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009C28 File Offset: 0x00007E28
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioReadFunc))]
		private static int OnRead(IntPtr instance, IntPtr data, int dataLength, out int wantMore)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsBioMono.OnRead(data, dataLength, out wantMore);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				wantMore = 0;
				num = -1;
			}
			return num;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009C78 File Offset: 0x00007E78
		private int OnWrite(IntPtr data, int dataLength)
		{
			byte[] array = new byte[dataLength];
			Marshal.Copy(data, array, 0, dataLength);
			if (!this.backend.Write(array, 0, dataLength))
			{
				return -1;
			}
			return dataLength;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009CA8 File Offset: 0x00007EA8
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioWriteFunc))]
		private static int OnWrite(IntPtr instance, IntPtr data, int dataLength)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsBioMono.OnWrite(data, dataLength);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				num = -1;
			}
			return num;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009CF4 File Offset: 0x00007EF4
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioControlFunc))]
		private static long Control(IntPtr instance, MonoBtlsBioMono.ControlCommand command, long arg)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			long num;
			try
			{
				num = monoBtlsBioMono.Control(command, arg);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				num = -1L;
			}
			return num;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009D40 File Offset: 0x00007F40
		protected override void Close()
		{
			try
			{
				if (this.backend != null)
				{
					this.backend.Close();
					this.backend = null;
				}
				if (this.handle.IsAllocated)
				{
					this.handle.Free();
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x0400026C RID: 620
		private GCHandle handle;

		// Token: 0x0400026D RID: 621
		private IntPtr instance;

		// Token: 0x0400026E RID: 622
		private MonoBtlsBioMono.BioReadFunc readFunc;

		// Token: 0x0400026F RID: 623
		private MonoBtlsBioMono.BioWriteFunc writeFunc;

		// Token: 0x04000270 RID: 624
		private MonoBtlsBioMono.BioControlFunc controlFunc;

		// Token: 0x04000271 RID: 625
		private IntPtr readFuncPtr;

		// Token: 0x04000272 RID: 626
		private IntPtr writeFuncPtr;

		// Token: 0x04000273 RID: 627
		private IntPtr controlFuncPtr;

		// Token: 0x04000274 RID: 628
		private IMonoBtlsBioMono backend;

		// Token: 0x0200009C RID: 156
		private enum ControlCommand
		{
			// Token: 0x04000276 RID: 630
			Flush = 1
		}

		// Token: 0x0200009D RID: 157
		// (Invoke) Token: 0x06000277 RID: 631
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BioReadFunc(IntPtr bio, IntPtr data, int dataLength, out int wantMore);

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x06000279 RID: 633
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BioWriteFunc(IntPtr bio, IntPtr data, int dataLength);

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x0600027B RID: 635
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long BioControlFunc(IntPtr bio, MonoBtlsBioMono.ControlCommand command, long arg);
	}
}
