package crc64a7d3113b67776f4e;


public class MyMediaController
	extends android.widget.MediaController
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_show:()V:GetShowHandler\n" +
			"n_hide:()V:GetHideHandler\n" +
			"";
		mono.android.Runtime.register ("Xamarians.MediaPlayer.Droid.MyMediaController, Xamarians.MediaPlayer.Droid", MyMediaController.class, __md_methods);
	}


	public MyMediaController (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MyMediaController.class) {
			mono.android.TypeManager.Activate ("Xamarians.MediaPlayer.Droid.MyMediaController, Xamarians.MediaPlayer.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public MyMediaController (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MyMediaController.class) {
			mono.android.TypeManager.Activate ("Xamarians.MediaPlayer.Droid.MyMediaController, Xamarians.MediaPlayer.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public MyMediaController (android.content.Context p0, boolean p1)
	{
		super (p0, p1);
		if (getClass () == MyMediaController.class) {
			mono.android.TypeManager.Activate ("Xamarians.MediaPlayer.Droid.MyMediaController, Xamarians.MediaPlayer.Droid", "Android.Content.Context, Mono.Android:System.Boolean, mscorlib", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public void show ()
	{
		n_show ();
	}

	private native void n_show ();


	public void hide ()
	{
		n_hide ();
	}

	private native void n_hide ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
