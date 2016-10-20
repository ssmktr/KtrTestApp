package com.ssmktr.KtrTestApp;


import android.content.Context;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    @Override
    public boolean dispatchKeyEvent(KeyEvent event){
        String keycodeint = String.valueOf(event.getKeyCode());
        UnityPlayer.UnitySendMessage("Main", "ReceiveKey", keycodeint);
        return super.dispatchKeyEvent(event);
    }
    
    public void GetPhoneNumber(){
        TelephonyManager telManager = (TelephonyManager) getSystemService(Context.TELEPHONY_SERVICE);
        String phoneNum = telManager.getLine1Number();
        UnityPlayer.UnitySendMessage("Main", "ReceivePhoneNumber", phoneNum);
    }
}
