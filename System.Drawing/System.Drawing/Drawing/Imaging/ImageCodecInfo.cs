using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> class provides the necessary storage members and methods to retrieve all pertinent information about the installed image encoders and decoders (called codecs). Not inheritable. </summary>
	// Token: 0x02000089 RID: 137
	public sealed class ImageCodecInfo
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00003F24 File Offset: 0x00002124
		internal ImageCodecInfo()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000DEBD File Offset: 0x0000C0BD
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000DEC5 File Offset: 0x0000C0C5
		public Guid Clsid
		{
			get
			{
				return this._clsid;
			}
			set
			{
				this._clsid = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000DECE File Offset: 0x0000C0CE
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000DED6 File Offset: 0x0000C0D6
		public Guid FormatID
		{
			get
			{
				return this._formatID;
			}
			set
			{
				this._formatID = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the codec.</summary>
		/// <returns>A string that contains the name of the codec.</returns>
		// Token: 0x17000136 RID: 310
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000DEDF File Offset: 0x0000C0DF
		public string CodecName
		{
			set
			{
				this._codecName = value;
			}
		}

		/// <summary>Gets or sets string that contains the path name of the DLL that holds the codec. If the codec is not in a DLL, this pointer is null.</summary>
		/// <returns>A string that contains the path name of the DLL that holds the codec.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000137 RID: 311
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public string DllName
		{
			set
			{
				this._dllName = value;
			}
		}

		/// <summary>Gets or sets a string that describes the codec's file format.</summary>
		/// <returns>A string that describes the codec's file format.</returns>
		// Token: 0x17000138 RID: 312
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000DEF1 File Offset: 0x0000C0F1
		public string FormatDescription
		{
			set
			{
				this._formatDescription = value;
			}
		}

		/// <summary>Gets or sets string that contains the file name extension(s) used in the codec. The extensions are separated by semicolons.</summary>
		/// <returns>A string that contains the file name extension(s) used in the codec.</returns>
		// Token: 0x17000139 RID: 313
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000DEFA File Offset: 0x0000C0FA
		public string FilenameExtension
		{
			set
			{
				this._filenameExtension = value;
			}
		}

		/// <summary>Gets or sets a string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</summary>
		/// <returns>A string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</returns>
		// Token: 0x1700013A RID: 314
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000DF03 File Offset: 0x0000C103
		public string MimeType
		{
			set
			{
				this._mimeType = value;
			}
		}

		/// <summary>Gets or sets 32-bit value used to store additional information about the codec. This property returns a combination of flags from the <see cref="T:System.Drawing.Imaging.ImageCodecFlags" /> enumeration.</summary>
		/// <returns>A 32-bit value used to store additional information about the codec.</returns>
		// Token: 0x1700013B RID: 315
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000DF0C File Offset: 0x0000C10C
		public ImageCodecFlags Flags
		{
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the version number of the codec.</summary>
		/// <returns>The version number of the codec.</returns>
		// Token: 0x1700013C RID: 316
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000DF15 File Offset: 0x0000C115
		public int Version
		{
			set
			{
				this._version = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that represents the signature of the codec.</summary>
		/// <returns>A two dimensional array of bytes that represents the signature of the codec.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000DF1E File Offset: 0x0000C11E
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000DF26 File Offset: 0x0000C126
		[CLSCompliant(false)]
		public byte[][] SignaturePatterns
		{
			get
			{
				return this._signaturePatterns;
			}
			set
			{
				this._signaturePatterns = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that can be used as a filter.</summary>
		/// <returns>A two dimensional array of bytes that can be used as a filter.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000DF2F File Offset: 0x0000C12F
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000DF37 File Offset: 0x0000C137
		[CLSCompliant(false)]
		public byte[][] SignatureMasks
		{
			get
			{
				return this._signatureMasks;
			}
			set
			{
				this._signatureMasks = value;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image encoders built into GDI+.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image encoders.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600047B RID: 1147 RVA: 0x0000DF40 File Offset: 0x0000C140
		public static ImageCodecInfo[] GetImageEncoders()
		{
			int num2;
			int num3;
			int num = GDIPlus.GdipGetImageEncodersSize(out num2, out num3);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num3);
			ImageCodecInfo[] array;
			try
			{
				num = GDIPlus.GdipGetImageEncoders(num2, num3, intPtr);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				array = ImageCodecInfo.ConvertFromMemory(intPtr, num2);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return array;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private static ImageCodecInfo[] ConvertFromMemory(IntPtr memoryStart, int numCodecs)
		{
			ImageCodecInfo[] array = new ImageCodecInfo[numCodecs];
			for (int i = 0; i < numCodecs; i++)
			{
				IntPtr intPtr = (IntPtr)((long)memoryStart + (long)(Marshal.SizeOf(typeof(ImageCodecInfoPrivate)) * i));
				ImageCodecInfoPrivate imageCodecInfoPrivate = new ImageCodecInfoPrivate();
				Marshal.PtrToStructure<ImageCodecInfoPrivate>(intPtr, imageCodecInfoPrivate);
				array[i] = new ImageCodecInfo();
				array[i].Clsid = imageCodecInfoPrivate.Clsid;
				array[i].FormatID = imageCodecInfoPrivate.FormatID;
				array[i].CodecName = Marshal.PtrToStringUni(imageCodecInfoPrivate.CodecName);
				array[i].DllName = Marshal.PtrToStringUni(imageCodecInfoPrivate.DllName);
				array[i].FormatDescription = Marshal.PtrToStringUni(imageCodecInfoPrivate.FormatDescription);
				array[i].FilenameExtension = Marshal.PtrToStringUni(imageCodecInfoPrivate.FilenameExtension);
				array[i].MimeType = Marshal.PtrToStringUni(imageCodecInfoPrivate.MimeType);
				array[i].Flags = (ImageCodecFlags)imageCodecInfoPrivate.Flags;
				array[i].Version = imageCodecInfoPrivate.Version;
				array[i].SignaturePatterns = new byte[imageCodecInfoPrivate.SigCount][];
				array[i].SignatureMasks = new byte[imageCodecInfoPrivate.SigCount][];
				for (int j = 0; j < imageCodecInfoPrivate.SigCount; j++)
				{
					array[i].SignaturePatterns[j] = new byte[imageCodecInfoPrivate.SigSize];
					array[i].SignatureMasks[j] = new byte[imageCodecInfoPrivate.SigSize];
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigMask + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignatureMasks[j], 0, imageCodecInfoPrivate.SigSize);
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigPattern + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignaturePatterns[j], 0, imageCodecInfoPrivate.SigSize);
				}
			}
			return array;
		}

		// Token: 0x04000272 RID: 626
		private Guid _clsid;

		// Token: 0x04000273 RID: 627
		private Guid _formatID;

		// Token: 0x04000274 RID: 628
		private string _codecName;

		// Token: 0x04000275 RID: 629
		private string _dllName;

		// Token: 0x04000276 RID: 630
		private string _formatDescription;

		// Token: 0x04000277 RID: 631
		private string _filenameExtension;

		// Token: 0x04000278 RID: 632
		private string _mimeType;

		// Token: 0x04000279 RID: 633
		private ImageCodecFlags _flags;

		// Token: 0x0400027A RID: 634
		private int _version;

		// Token: 0x0400027B RID: 635
		private byte[][] _signaturePatterns;

		// Token: 0x0400027C RID: 636
		private byte[][] _signatureMasks;
	}
}
