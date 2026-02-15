using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates which <see cref="T:System.Windows.Forms.ImageList" /> a property is related to.</summary>
	// Token: 0x0200016F RID: 367
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class RelatedImageListAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RelatedImageListAttribute" /> class. </summary>
		/// <param name="relatedImageList">The name of the <see cref="T:System.Windows.Forms.ImageList" /> the property relates to.</param>
		// Token: 0x06000E06 RID: 3590 RVA: 0x0003F26C File Offset: 0x0003D46C
		public RelatedImageListAttribute(string relatedImageList)
		{
			this.related_image_list = relatedImageList;
		}

		// Token: 0x040008EB RID: 2283
		private string related_image_list;
	}
}
