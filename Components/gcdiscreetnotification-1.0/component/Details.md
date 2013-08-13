Discreet Notification (a.k.a. GCDiscreetNotificationView) is a discreet, non-modal notification view for iOS. You can use it to show an activity or state of you app without blocking the user interactions.

Here's an example:

```csharp
using GCDiscreetNotification;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	var notificationView = new GCDiscreetNotificationView (
		text: "Hello Notification",
	    activity: false,
	    presentationMode: GCDNPresentationMode.Top,
		view: View
	);

	notificationView.Show (animated: true);
}
```
