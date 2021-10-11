using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationObject : MonoBehaviour
{
    [HideInInspector]
    public string _name;
    [HideInInspector]
    public Notifications.NotificationType type;
    [HideInInspector]
    public double cost;
    public Text nameText, costText;

    public NotificationObject(Notifications.NotificationType _type, string __name, double _cost)
    {
        this.type = _type;
        this._name = __name;
        this.cost = _cost;
    }
}