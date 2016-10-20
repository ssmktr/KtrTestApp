package com.ssmktr.KtrTestApp;


import android.content.Context;
import android.provider.Telephony;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    @Override
    public boolean dispatchKeyEvent(KeyEvent event){
        String keycodeint = String.valueOf(event.getKeyCode());
        UnityPlayer.UnitySendMessage("Main", "ReceiveKey", keycodeint);
        return super.dispatchKeyEvent(event);
    }

    public String GetPhoneNumber(){
        TelephonyManager telManager = (TelephonyManager) getSystemService(Context.TELEPHONY_SERVICE);
        String phoneNum = telManager.getLine1Number();
//        UnityPlayer.UnitySendMessage("Main", "ReceivePhoneNumber", phoneNum);
        return phoneNum;
    }

    public String GetPhoneModelID(){
        TelephonyManager telManager = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE);
        String PhoneID = telManager.getDeviceId();
        return PhoneID;
    }

    public void PlayToast(final String msg){
        this.runOnUiThread(new Runnable(){
            public void run(){
                int duration = 1000;
                Toast.makeText(getApplicationContext(), msg, Toast.LENGTH_LONG).show();
            }
        });
    }
}
