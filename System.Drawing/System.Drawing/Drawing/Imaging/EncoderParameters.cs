using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
	// Token: 0x02000085 RID: 133
	public sealed class EncoderParameters
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000DB58 File Offset: 0x0000BD58
		internal IntPtr ConvertToMemory()
		{
			int num = Marshal.SizeOf(typeof(EncoderParameter));
			int num2 = this._param.Length;
			IntPtr intPtr;
			long num3;
			checked
			{
				intPtr = Marshal.AllocHGlobal(num2 * num + Marshal.SizeOf(typeof(IntPtr)));
				if (intPtr == IntPtr.Zero)
				{
					throw SafeNativeMethods.Gdip.StatusException(3);
				}
				Marshal.WriteIntPtr(intPtr, (IntPtr)num2);
				num3 = (long)intPtr + unchecked((long)Marshal.SizeOf(typeof(IntPtr)));
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.StructureToPtr<EncoderParameter>(this._param[i], (IntPtr)(num3 + (long)(i * num)), false);
			}
			return intPtr;
		}

		// Token: 0x04000262 RID: 610
		private EncoderParameter[] _param;
	}
}
