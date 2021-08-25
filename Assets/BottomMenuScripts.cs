using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomMenuScripts : MyButton
{

	public void Rate()
	{
		base.OnButtonClick();
		// "market" works for android  (iOS: put your app link
		Application.OpenURL("");
	}

	public void More()
	{
		base.OnButtonClick();
		// Android  ,(iOS: put you app store link)
		Application.OpenURL("");
	}

	public void AppExit()
    {
		base.OnButtonClick();
		Application.Quit();
    }

}
