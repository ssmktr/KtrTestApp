package com.ssmktr.KtrTestApp;


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
}
