using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
public class AndroidNotificationHandler : MonoBehaviour
{
    #if UNITY_ANDROID
    private const string ChanelId="notification_chanel";
   public void ScheduleNotification(DateTime dateTime)
   {
       AndroidNotificationChannel notificationChannel= new AndroidNotificationChannel
       {
           Id=ChanelId,
           Name="Notification Chanel",
           Description="Some random description",
           Importance=Importance.Default
       };
       AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

       AndroidNotification notification=new AndroidNotification{
           Title="Stormi, baby!",
            Text="Your energy is full now. Come and play baby.",
            SmallIcon="icon_0",
            LargeIcon="default",
            FireTime=dateTime
       };
       AndroidNotificationCenter.SendNotification(notification,ChanelId);
   }
   #endif
}
