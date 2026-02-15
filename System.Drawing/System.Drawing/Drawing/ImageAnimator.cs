using System;
using System.Collections;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
	/// <summary>Animates an image that has time-based frames.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000051 RID: 81
	public sealed class ImageAnimator
	{
		/// <summary>Displays a multiple-frame image as an animation.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to animate. </param>
		/// <param name="onFrameChangedHandler">An EventHandler object that specifies the method that is called when the animation frame changes. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002FD RID: 765 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public static void Animate(Image image, EventHandler onFrameChangedHandler)
		{
			if (!ImageAnimator.CanAnimate(image))
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				return;
			}
			byte[] value = image.GetPropertyItem(20736).Value;
			int[] array = new int[value.Length >> 2];
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				int num2 = BitConverter.ToInt32(value, i) * 10;
				array[num] = ((num2 < 100) ? 100 : num2);
				i += 4;
				num++;
			}
			AnimateEventArgs animateEventArgs = new AnimateEventArgs(image);
			Thread thread = new Thread(new ThreadStart(new WorkerThread(onFrameChangedHandler, animateEventArgs, array).LoopHandler));
			thread.IsBackground = true;
			animateEventArgs.RunThread = thread;
			ImageAnimator.ht.Add(image, animateEventArgs);
			thread.Start();
		}

		/// <summary>Returns a Boolean value indicating whether the specified image contains time-based frames.</summary>
		/// <returns>This method returns true if the specified image contains time-based frames; otherwise, false.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002FE RID: 766 RVA: 0x0000B29C File Offset: 0x0000949C
		public static bool CanAnimate(Image image)
		{
			if (image == null)
			{
				return false;
			}
			int num = image.FrameDimensionsList.Length;
			if (num < 1)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (image.FrameDimensionsList[i].Equals(FrameDimension.Time.Guid))
				{
					return image.GetFrameCount(FrameDimension.Time) > 1;
				}
			}
			return false;
		}

		/// <summary>Terminates a running animation.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to stop animating. </param>
		/// <param name="onFrameChangedHandler">An EventHandler object that specifies the method that is called when the animation frame changes. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002FF RID: 767 RVA: 0x0000B2F6 File Offset: 0x000094F6
		public static void StopAnimate(Image image, EventHandler onFrameChangedHandler)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				((AnimateEventArgs)ImageAnimator.ht[image]).RunThread.Abort();
				ImageAnimator.ht.Remove(image);
			}
		}

		/// <summary>Advances the frame in the specified image. The new frame is drawn the next time the image is rendered. This method applies only to images with time-based frames.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object for which to update frames. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000300 RID: 768 RVA: 0x0000B32E File Offset: 0x0000952E
		public static void UpdateFrames(Image image)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				ImageAnimator.UpdateImageFrame(image);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B348 File Offset: 0x00009548
		private static void UpdateImageFrame(Image image)
		{
			AnimateEventArgs animateEventArgs = (AnimateEventArgs)ImageAnimator.ht[image];
			image.SelectActiveFrame(FrameDimension.Time, animateEventArgs.GetNextFrame());
		}

		// Token: 0x04000182 RID: 386
		private static Hashtable ht = Hashtable.Synchronized(new Hashtable());
	}
}
