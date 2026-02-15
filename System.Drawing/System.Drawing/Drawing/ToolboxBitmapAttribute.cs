using System;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>Allows you to specify an icon to represent a control in a container, such as the Microsoft Visual Studio Form Designer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.Class)]
	public class ToolboxBitmapAttribute : Attribute
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000C798 File Offset: 0x0000A998
		private ToolboxBitmapAttribute()
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object with an image from a specified file.</summary>
		/// <param name="imageFile">The name of a file that contains a 16 by 16 bitmap. </param>
		// Token: 0x06000360 RID: 864 RVA: 0x0000C798 File Offset: 0x0000A998
		public ToolboxBitmapAttribute(string imageFile)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 by 16 bitmap that is embedded as a resource in a specified assembly.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource. </param>
		/// <param name="name">The name of the embedded bitmap resource. </param>
		// Token: 0x06000361 RID: 865 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public ToolboxBitmapAttribute(Type t, string name)
		{
			this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(t, name, false);
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>This method returns true if <paramref name="value" /> is both a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000362 RID: 866 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		public override bool Equals(object value)
		{
			return value is ToolboxBitmapAttribute && (value == this || ((ToolboxBitmapAttribute)value).smallImage == this.smallImage);
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000363 RID: 867 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		public override int GetHashCode()
		{
			return this.smallImage.GetHashCode() ^ this.bigImage.GetHashCode();
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> object based on a bitmap resource that is embedded in an assembly.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> object based on the retrieved bitmap.</returns>
		/// <param name="t">This method searches for an embedded bitmap resource in the assembly that defines the type specified by the t parameter. For example, if you pass typeof(ControlA) to the t parameter, then this method searches the assembly that defines ControlA. </param>
		/// <param name="imageName">The name of the embedded bitmap resource. </param>
		/// <param name="large">Specifies whether this method returns a large image (true)or a small image (false). The small image is 16 by 16, and the large image is 32 x 32. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000364 RID: 868 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public static Image GetImageFromResource(Type t, string imageName, bool large)
		{
			if (imageName == null)
			{
				imageName = t.Name + ".bmp";
			}
			Image image;
			try
			{
				Bitmap bitmap;
				using (Stream manifestResourceStream = t.GetTypeInfo().Assembly.GetManifestResourceStream(t.Namespace + "." + imageName))
				{
					if (manifestResourceStream == null)
					{
						return null;
					}
					bitmap = new Bitmap(manifestResourceStream, false);
				}
				if (large)
				{
					image = new Bitmap(bitmap, 32, 32);
				}
				else
				{
					image = bitmap;
				}
			}
			catch
			{
				image = null;
			}
			return image;
		}

		// Token: 0x04000199 RID: 409
		private Image smallImage;

		// Token: 0x0400019A RID: 410
		private Image bigImage;

		/// <summary>A <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object that has its small image and its large image set to null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400019B RID: 411
		public static readonly ToolboxBitmapAttribute Default = new ToolboxBitmapAttribute();
	}
}
