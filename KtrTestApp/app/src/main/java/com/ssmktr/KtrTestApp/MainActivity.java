package com.ssmktr.KtrTestApp;


import android.app.Notification;
import android.app.NotificationManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Vibrator;
import android.provider.CalendarContract;
import android.provider.Telephony;
//import android.support.v7.app.NotificationCompat;
import android.support.v4.app.NotificationCompat;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;
import android.widget.Toast;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import java.util.Calendar;
import java.util.jar.Manifest;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    private static int ONE_MINUTE = 5626;

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

    public void OnVibrator(long time)
    {
        Vibrator v = (Vibrator)getSystemService(Context.VIBRATOR_SERVICE);
        v.vibrate(time);
    }

    public void LocalAlarm(int time)
    {
//        NotificationManager nm = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);
//        NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
//                .setContentTitle("제목")
//                .setContentText("내용")
//                .setSmallIcon(R.mipmap.ic_launcher)
//                .setAutoCancel(true)
//                .setWhen(System.currentTimeMillis())
//                .setDefaults(Notification.DEFAULT_SOUND | Notification.DEFAULT_VIBRATE | Notification.DEFAULT_LIGHTS);
//
//        Notification n = builder.build();
//        n.defaults = Notification.DEFAULT_LIGHTS;
//        nm.notify(time, n);

        
    }
}
