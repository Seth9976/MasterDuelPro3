using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Provides methods to manage a collection of <see cref="T:System.Drawing.Image" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D3 RID: 211
	[DefaultProperty("Images")]
	[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[TypeConverter(typeof(ImageListConverter))]
	public sealed class ImageList : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class with default values for <see cref="P:System.Windows.Forms.ImageList.ColorDepth" />, <see cref="P:System.Windows.Forms.ImageList.ImageSize" />, and <see cref="P:System.Windows.Forms.ImageList.TransparentColor" />.</summary>
		// Token: 0x060007D4 RID: 2004 RVA: 0x000221EC File Offset: 0x000203EC
		public ImageList()
		{
			this.images = new ImageList.ImageCollection(this);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00022200 File Offset: 0x00020400
		private void OnRecreateHandle()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ImageList.RecreateHandleEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Gets the color depth of the image list.</summary>
		/// <returns>The number of available colors for the image. In the .NET Framework version 1.0, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth4Bit" />. In the .NET Framework version 1.1 or later, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth8Bit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The color depth is not a valid <see cref="T:System.Windows.Forms.ColorDepth" /> enumeration value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001ED RID: 493
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00022232 File Offset: 0x00020432
		public ColorDepth ColorDepth
		{
			set
			{
				this.images.ColorDepth = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> for this image list.</summary>
		/// <returns>The collection of images.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00022240 File Offset: 0x00020440
		[DefaultValue(null)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ImageList.ImageCollection Images
		{
			get
			{
				return this.images;
			}
		}

		/// <summary>Gets or sets the size of the images in the image list.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that defines the height and width, in pixels, of the images in the list. The default size is 16 by 16. The maximum size is 256 by 256.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is equal to <see cref="P:System.Drawing.Size.IsEmpty" />.-or- The value of the height or width is less than or equal to 0.-or- The value of the height or width is greater than 256. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new size has a dimension less than 0 or greater than 256.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00022248 File Offset: 0x00020448
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00022255 File Offset: 0x00020455
		[Localizable(true)]
		public Size ImageSize
		{
			get
			{
				return this.images.ImageSize;
			}
			set
			{
				this.images.ImageSize = value;
			}
		}

		/// <summary>Gets or sets the color to treat as transparent.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is Transparent.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F0 RID: 496
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00022263 File Offset: 0x00020463
		public Color TransparentColor
		{
			set
			{
				this.images.TransparentColor = value;
			}
		}

		/// <summary>Draws the image indicated by the specified index on the specified <see cref="T:System.Drawing.Graphics" /> at the given location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="pt">The location defined by a <see cref="T:System.Drawing.Point" /> at which to draw the image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007DB RID: 2011 RVA: 0x00022271 File Offset: 0x00020471
		public void Draw(Graphics g, Point pt, int index)
		{
			this.Draw(g, pt.X, pt.Y, index);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> at the specified location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The horizontal position at which to draw the image. </param>
		/// <param name="y">The vertical position at which to draw the image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007DC RID: 2012 RVA: 0x00022289 File Offset: 0x00020489
		public void Draw(Graphics g, int x, int y, int index)
		{
			g.DrawImage(this.images.GetImage(index), x, y);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> using the specified location and size.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The horizontal position at which to draw the image. </param>
		/// <param name="y">The vertical position at which to draw the image. </param>
		/// <param name="width">The width, in pixels, of the destination image. </param>
		/// <param name="height">The height, in pixels, of the destination image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007DD RID: 2013 RVA: 0x000222A0 File Offset: 0x000204A0
		public void Draw(Graphics g, int x, int y, int width, int height, int index)
		{
			g.DrawImage(this.images.GetImage(index), x, y, width, height);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007DE RID: 2014 RVA: 0x000222BC File Offset: 0x000204BC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				" Images.Count: ",
				this.images.Count.ToString(),
				", ImageSize: ",
				this.ImageSize.ToString()
			});
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002231A File Offset: 0x0002051A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.images.DestroyHandle();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000503 RID: 1283
		private static readonly Size DefaultImageSize = new Size(16, 16);

		// Token: 0x04000504 RID: 1284
		private static readonly Color DefaultTransparentColor = Color.Transparent;

		// Token: 0x04000505 RID: 1285
		private readonly ImageList.ImageCollection images;

		// Token: 0x04000506 RID: 1286
		private static object RecreateHandleEvent = new object();

		/// <summary>Encapsulates the collection of <see cref="T:System.Drawing.Image" /> objects in an <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		// Token: 0x020000D4 RID: 212
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public sealed class ImageCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060007E1 RID: 2017 RVA: 0x00022358 File Offset: 0x00020558
			internal ImageCollection(ImageList owner)
			{
				this.owner = owner;
			}

			// Token: 0x170001F1 RID: 497
			// (set) Token: 0x060007E2 RID: 2018 RVA: 0x000223AC File Offset: 0x000205AC
			internal ColorDepth ColorDepth
			{
				set
				{
					if (!Enum.IsDefined(typeof(ColorDepth), value))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
					}
					if (this.colorDepth != value)
					{
						this.colorDepth = value;
						this.RecreateHandle();
					}
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x060007E3 RID: 2019 RVA: 0x000223FC File Offset: 0x000205FC
			// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00022404 File Offset: 0x00020604
			internal Size ImageSize
			{
				get
				{
					return this.imageSize;
				}
				set
				{
					if (value.Width < 1 || value.Width > 256 || value.Height < 1 || value.Height > 256)
					{
						throw new ArgumentException("ImageSize.Width and Height must be between 1 and 256", "value");
					}
					if (this.imageSize != value)
					{
						this.imageSize = value;
						this.RecreateHandle();
					}
				}
			}

			// Token: 0x170001F3 RID: 499
			// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0002246C File Offset: 0x0002066C
			internal Color TransparentColor
			{
				set
				{
					this.transparentColor = value;
				}
			}

			/// <summary>Gets the number of images currently in the list.</summary>
			/// <returns>The number of images in the list. The default is 0.</returns>
			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00022475 File Offset: 0x00020675
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.handleCreated)
					{
						return this.count;
					}
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the list is read-only.</summary>
			/// <returns>Always false.</returns>
			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets an <see cref="T:System.Drawing.Image" /> at the specified index within the collection.</summary>
			/// <returns>The image in the list specified by <paramref name="index" />. </returns>
			/// <param name="index">The index of the image to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />. </exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="image" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be added to the list.</exception>
			// Token: 0x170001F6 RID: 502
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public Image this[int index]
			{
				get
				{
					return (Image)this.GetImage(index).Clone();
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException("Image must be a Bitmap.");
					}
					Image image = this.CreateImage(value, this.transparentColor);
					this.CreateHandle();
					this.list[index] = image;
				}
			}

			/// <summary>Gets an <see cref="T:System.Drawing.Image" /> with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Drawing.Image" /> with the specified key.</returns>
			/// <param name="key">The name of the image to retrieve from the collection.</param>
			// Token: 0x170001F7 RID: 503
			public Image this[string key]
			{
				get
				{
					int num;
					if ((num = this.IndexOfKey(key)) != -1)
					{
						return this[num];
					}
					return null;
				}
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x0002252E File Offset: 0x0002072E
			private static bool CompareKeys(string key1, string key2)
			{
				return key1 != null && key2 != null && key1.Length == key2.Length && string.Compare(key1, key2, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x00022558 File Offset: 0x00020758
			private int AddItem(string key, ImageList.ImageCollection.ImageListItem item)
			{
				int num;
				if (this.handleCreated)
				{
					num = this.AddItemInternal(item);
				}
				else
				{
					num = this.list.Add(item);
					this.count += item.ImageCount;
				}
				if ((item.Flags & ImageList.ImageCollection.ItemFlags.ImageStrip) == ImageList.ImageCollection.ItemFlags.None)
				{
					this.keys.Add(key);
				}
				else
				{
					for (int i = 0; i < item.ImageCount; i++)
					{
						this.keys.Add(null);
					}
				}
				return num;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x000225D0 File Offset: 0x000207D0
			private int AddItemInternal(ImageList.ImageCollection.ImageListItem item)
			{
				if (this.Changed != null)
				{
					this.Changed(this, EventArgs.Empty);
				}
				if (item.Image is Icon)
				{
					int width;
					int height;
					Bitmap bitmap = new Bitmap(width = this.imageSize.Width, height = this.imageSize.Height, PixelFormat.Format32bppArgb);
					Graphics graphics = Graphics.FromImage(bitmap);
					graphics.DrawIcon((Icon)item.Image, new Rectangle(0, 0, width, height));
					graphics.Dispose();
					this.ReduceColorDepth(bitmap);
					return this.list.Add(bitmap);
				}
				if ((item.Flags & ImageList.ImageCollection.ItemFlags.ImageStrip) == ImageList.ImageCollection.ItemFlags.None)
				{
					return this.list.Add(this.CreateImage((Image)item.Image, ((item.Flags & ImageList.ImageCollection.ItemFlags.UseTransparentColor) == ImageList.ImageCollection.ItemFlags.None) ? this.transparentColor : item.TransparentColor));
				}
				Image image;
				int width2;
				int width3;
				if ((width2 = (image = (Image)item.Image).Width) == 0 || width2 % (width3 = this.imageSize.Width) != 0)
				{
					throw new ArgumentException("Width of image strip must be a positive multiple of ImageSize.Width.", "value");
				}
				int height2;
				if (image.Height != (height2 = this.imageSize.Height))
				{
					throw new ArgumentException("Height of image strip must be equal to ImageSize.Height.", "value");
				}
				Rectangle rectangle = new Rectangle(0, 0, width3, height2);
				ImageAttributes imageAttributes;
				if (this.transparentColor.A == 0)
				{
					imageAttributes = null;
				}
				else
				{
					imageAttributes = new ImageAttributes();
					imageAttributes.SetColorKey(this.transparentColor, this.transparentColor);
				}
				int num = this.list.Count;
				for (int i = 0; i < width2; i += width3)
				{
					Bitmap bitmap2 = new Bitmap(width3, height2, PixelFormat.Format32bppArgb);
					Graphics graphics2 = Graphics.FromImage(bitmap2);
					graphics2.DrawImage(image, rectangle, i, 0, width3, height2, GraphicsUnit.Pixel, imageAttributes);
					graphics2.Dispose();
					this.ReduceColorDepth(bitmap2);
					this.list.Add(bitmap2);
				}
				if (imageAttributes != null)
				{
					imageAttributes.Dispose();
				}
				return num;
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x000227A8 File Offset: 0x000209A8
			private void CreateHandle()
			{
				if (!this.handleCreated)
				{
					ArrayList arrayList = this.list;
					this.list = new ArrayList(this.count);
					this.count = 0;
					this.handleCreated = true;
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.AddItemInternal((ImageList.ImageCollection.ImageListItem)arrayList[i]);
					}
				}
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x00022808 File Offset: 0x00020A08
			private Image CreateImage(Image value, Color transparentColor)
			{
				ImageAttributes imageAttributes;
				if (transparentColor.A == 0)
				{
					imageAttributes = null;
				}
				else
				{
					imageAttributes = new ImageAttributes();
					imageAttributes.SetColorKey(transparentColor, transparentColor);
				}
				int width;
				int height;
				Bitmap bitmap = new Bitmap(width = this.imageSize.Width, height = this.imageSize.Height, PixelFormat.Format32bppArgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(value, new Rectangle(0, 0, width, height), 0, 0, value.Width, value.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				if (imageAttributes != null)
				{
					imageAttributes.Dispose();
				}
				this.ReduceColorDepth(bitmap);
				return bitmap;
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x000228B0 File Offset: 0x00020AB0
			private void RecreateHandle()
			{
				if (this.handleCreated)
				{
					this.DestroyHandle();
					this.handleCreated = true;
					this.owner.OnRecreateHandle();
				}
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x000228D4 File Offset: 0x00020AD4
			private unsafe void ReduceColorDepth(Bitmap bitmap)
			{
				if (this.colorDepth < ColorDepth.Depth32Bit)
				{
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
					try
					{
						byte* ptr = (byte*)(void*)bitmapData.Scan0;
						int height = bitmapData.Height;
						int num = bitmapData.Width << 2;
						int stride = bitmapData.Stride;
						if (this.colorDepth < ColorDepth.Depth16Bit)
						{
							Color[] entries = ((this.colorDepth < ColorDepth.Depth8Bit) ? ImageList.ImageCollection.IndexedColorDepths.Palette4Bit : ImageList.ImageCollection.IndexedColorDepths.Palette8Bit).Entries;
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) == 0) ? 0 : ImageList.ImageCollection.IndexedColorDepths.GetNearestColor(entries, num2 | -16777216));
								}
								ptr += stride;
							}
						}
						else if (this.colorDepth < ColorDepth.Depth24Bit)
						{
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) == 0) ? 0 : ((num2 & 16316664) | -16777216));
								}
								ptr += stride;
							}
						}
						else
						{
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) == 0) ? 0 : (num2 | -16777216));
								}
								ptr += stride;
							}
						}
					}
					finally
					{
						bitmap.UnlockBits(bitmapData);
					}
				}
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x00022A5C File Offset: 0x00020C5C
			internal void DestroyHandle()
			{
				if (this.handleCreated)
				{
					this.list = new ArrayList();
					this.count = 0;
					this.handleCreated = false;
					this.keys = new ArrayList();
				}
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x00022A8A File Offset: 0x00020C8A
			internal Image GetImage(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CreateHandle();
				return (Image)this.list[index];
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
			/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			// Token: 0x060007F4 RID: 2036 RVA: 0x00022ABB File Offset: 0x00020CBB
			public void Add(Image value)
			{
				this.Add(null, value);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />, using the specified color to generate the mask.</summary>
			/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
			/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> to mask this image. </param>
			/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			// Token: 0x060007F5 RID: 2037 RVA: 0x00022AC5 File Offset: 0x00020CC5
			public int Add(Image value, Color transparentColor)
			{
				return this.AddItem(null, new ImageList.ImageCollection.ImageListItem(value, transparentColor));
			}

			/// <summary>Adds an image with the specified key to the end of the collection.</summary>
			/// <param name="key">The name of the image.</param>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="image" /> is null. </exception>
			// Token: 0x060007F6 RID: 2038 RVA: 0x00022AD5 File Offset: 0x00020CD5
			public void Add(string key, Image image)
			{
				this.AddItem(key, new ImageList.ImageCollection.ImageListItem(image));
			}

			/// <summary>Removes all the images and masks from the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			// Token: 0x060007F7 RID: 2039 RVA: 0x00022AE5 File Offset: 0x00020CE5
			public void Clear()
			{
				this.list.Clear();
				if (!this.handleCreated)
				{
					this.count = 0;
				}
				this.keys.Clear();
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Contains(System.Object)" /> method indicates whether a specified object is contained in the list.</summary>
			/// <returns>true if the image is found in the list; otherwise, false.</returns>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x060007F8 RID: 2040 RVA: 0x00022B0C File Offset: 0x00020D0C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public bool Contains(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x060007F9 RID: 2041 RVA: 0x00022B14 File Offset: 0x00020D14
			public IEnumerator GetEnumerator()
			{
				Image[] array = new Image[this.Count];
				if (array.Length != 0)
				{
					this.CreateHandle();
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = (Image)((Image)this.list[i]).Clone();
					}
				}
				return array.GetEnumerator();
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method returns the index of a specified object in the list.</summary>
			/// <returns>The index of the image in the list.</returns>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x060007FA RID: 2042 RVA: 0x00022B0C File Offset: 0x00020D0C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int IndexOf(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines the index of the first occurrence of an image with the specified key in the collection.</summary>
			/// <returns>The zero-based index of the first occurrence of an image with the specified key in the collection, if found; otherwise, -1.</returns>
			/// <param name="key">The key of the image to retrieve the index for.</param>
			// Token: 0x060007FB RID: 2043 RVA: 0x00022B6C File Offset: 0x00020D6C
			public int IndexOfKey(string key)
			{
				if (key != null && key.Length != 0)
				{
					if (this.lastKeyIndex >= 0 && this.lastKeyIndex < this.Count && ImageList.ImageCollection.CompareKeys((string)this.keys[this.lastKeyIndex], key))
					{
						return this.lastKeyIndex;
					}
					for (int i = 0; i < this.Count; i++)
					{
						if (ImageList.ImageCollection.CompareKeys((string)this.keys[i], key))
						{
							return this.lastKeyIndex = i;
						}
					}
				}
				return this.lastKeyIndex = -1;
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Remove(System.Object)" /> method removes a specified object from the list.</summary>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to remove from the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x060007FC RID: 2044 RVA: 0x00022B0C File Offset: 0x00020D0C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Remove(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes an image from the list.</summary>
			/// <param name="index">The index of the image to remove. </param>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be removed. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index value was less than 0.-or- The index value is greater than or equal to the <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" /> of images. </exception>
			// Token: 0x060007FD RID: 2045 RVA: 0x00022C00 File Offset: 0x00020E00
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CreateHandle();
				this.list.RemoveAt(index);
				this.keys.RemoveAt(index);
				if (this.Changed != null)
				{
					this.Changed(this, EventArgs.Empty);
				}
			}

			/// <summary>Gets or sets an image in an existing <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
			/// <returns>The image in the list specified by the index.</returns>
			/// <param name="index">The zero-based index of the image to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			/// <exception cref="T:System.Exception">The attempt to replace the image failed.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a bitmap.</exception>
			// Token: 0x170001F8 RID: 504
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is Image))
					{
						throw new ArgumentException("value");
					}
					this[index] = (Image)value;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000800 RID: 2048 RVA: 0x00002D70 File Offset: 0x00000F70
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <returns>The index of the newly added image, or -1 if the image could not be added.</returns>
			/// <param name="value">The image to add to the list.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			// Token: 0x06000803 RID: 2051 RVA: 0x00022C87 File Offset: 0x00020E87
			int IList.Add(object value)
			{
				if (!(value is Image))
				{
					throw new ArgumentException("value");
				}
				int num = this.Count;
				this.Add((Image)value);
				return num;
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to locate in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06000804 RID: 2052 RVA: 0x00022CAE File Offset: 0x00020EAE
			bool IList.Contains(object image)
			{
				return image is Image && this.Contains((Image)image);
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to find in the list.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06000805 RID: 2053 RVA: 0x00022CC6 File Offset: 0x00020EC6
			int IList.IndexOf(object image)
			{
				if (!(image is Image))
				{
					return -1;
				}
				return this.IndexOf((Image)image);
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06000806 RID: 2054 RVA: 0x00022B0C File Offset: 0x00020D0C
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" />. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06000807 RID: 2055 RVA: 0x00022CDE File Offset: 0x00020EDE
			void IList.Remove(object image)
			{
				if (image is Image)
				{
					this.Remove((Image)image);
				}
			}

			/// <summary>Copies the items in this collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
			/// <param name="index">The zero-based index in the <see cref="T:System.Array" /> at which copying begins.  </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dest" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="dest" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
			/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> cannot be cast automatically to the type of the destination array.</exception>
			// Token: 0x06000808 RID: 2056 RVA: 0x00022CF4 File Offset: 0x00020EF4
			void ICollection.CopyTo(Array dest, int index)
			{
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this[i], index++);
				}
			}

			// Token: 0x04000507 RID: 1287
			private ColorDepth colorDepth = ColorDepth.Depth8Bit;

			// Token: 0x04000508 RID: 1288
			private Size imageSize = ImageList.DefaultImageSize;

			// Token: 0x04000509 RID: 1289
			private Color transparentColor = ImageList.DefaultTransparentColor;

			// Token: 0x0400050A RID: 1290
			private ArrayList list = new ArrayList();

			// Token: 0x0400050B RID: 1291
			private ArrayList keys = new ArrayList();

			// Token: 0x0400050C RID: 1292
			private int count;

			// Token: 0x0400050D RID: 1293
			private bool handleCreated;

			// Token: 0x0400050E RID: 1294
			private int lastKeyIndex = -1;

			// Token: 0x0400050F RID: 1295
			private readonly ImageList owner;

			// Token: 0x04000510 RID: 1296
			[CompilerGenerated]
			private EventHandler Changed;

			// Token: 0x020000D5 RID: 213
			private static class IndexedColorDepths
			{
				// Token: 0x06000809 RID: 2057 RVA: 0x00022D28 File Offset: 0x00020F28
				static IndexedColorDepths()
				{
					Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format4bppIndexed);
					ImageList.ImageCollection.IndexedColorDepths.Palette4Bit = bitmap.Palette;
					bitmap.Dispose();
					Bitmap bitmap2 = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
					ImageList.ImageCollection.IndexedColorDepths.Palette8Bit = bitmap2.Palette;
					bitmap2.Dispose();
					ImageList.ImageCollection.IndexedColorDepths.squares = new int[511];
					for (int i = 0; i < 256; i++)
					{
						ImageList.ImageCollection.IndexedColorDepths.squares[255 + i] = (ImageList.ImageCollection.IndexedColorDepths.squares[255 - i] = i * i);
					}
				}

				// Token: 0x0600080A RID: 2058 RVA: 0x00022DAC File Offset: 0x00020FAC
				internal static int GetNearestColor(Color[] palette, int color)
				{
					int num = palette.Length;
					for (int i = 0; i < num; i++)
					{
						if (palette[i].ToArgb() == color)
						{
							return color;
						}
					}
					int num2 = (int)(((uint)color >> 16) & 255U);
					int num3 = (int)(((uint)color >> 8) & 255U);
					int num4 = color & 255;
					int num5 = -16777216;
					int num6 = int.MaxValue;
					for (int i = 0; i < num; i++)
					{
						int num7;
						if ((num7 = ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].R) - num2] + ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].G) - num3] + ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].B) - num4]) < num6)
						{
							num5 = palette[i].ToArgb();
							num6 = num7;
						}
					}
					return num5;
				}

				// Token: 0x04000511 RID: 1297
				internal static readonly ColorPalette Palette4Bit;

				// Token: 0x04000512 RID: 1298
				internal static readonly ColorPalette Palette8Bit;

				// Token: 0x04000513 RID: 1299
				private static readonly int[] squares;
			}

			// Token: 0x020000D6 RID: 214
			[Flags]
			private enum ItemFlags
			{
				// Token: 0x04000515 RID: 1301
				None = 0,
				// Token: 0x04000516 RID: 1302
				UseTransparentColor = 1,
				// Token: 0x04000517 RID: 1303
				ImageStrip = 2
			}

			// Token: 0x020000D7 RID: 215
			private sealed class ImageListItem
			{
				// Token: 0x0600080B RID: 2059 RVA: 0x00022E7E File Offset: 0x0002107E
				internal ImageListItem(Image value)
				{
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException("Image must be a Bitmap.");
					}
					this.Image = value;
				}

				// Token: 0x0600080C RID: 2060 RVA: 0x00022EB5 File Offset: 0x000210B5
				internal ImageListItem(Image value, Color transparentColor)
					: this(value)
				{
					this.Flags = ImageList.ImageCollection.ItemFlags.UseTransparentColor;
					this.TransparentColor = transparentColor;
				}

				// Token: 0x04000518 RID: 1304
				internal readonly object Image;

				// Token: 0x04000519 RID: 1305
				internal readonly ImageList.ImageCollection.ItemFlags Flags;

				// Token: 0x0400051A RID: 1306
				internal readonly Color TransparentColor;

				// Token: 0x0400051B RID: 1307
				internal readonly int ImageCount = 1;
			}
		}
	}
}
