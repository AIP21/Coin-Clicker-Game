using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    public GameObject NotificationPrefab;
    List<NotificationObject> NotificationList = new List<NotificationObject>();
    bool lvlShown, multShown, cpcShown, cpsShown, prestigeShown;

    public void CreateNotification(NotificationType type, string name, double cost)
    {
        if (type == NotificationType.Level && !lvlShown)
        {
            lvlShown = true;
            print("New notification created with name: " + name + " and cost: $" + cost);
            GameObject go = Instantiate(NotificationPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0 + (-105 * NotificationList.Count), 0);
            NotificationObject obj = go.GetComponent<NotificationObject>();
            NotificationList.Add(obj);

            obj.type = type;
            obj._name = name;
            obj.cost = cost;
            obj.nameText.text = name;
            obj.costText.text = "Cost: $" + cost;
            go.LeanMoveLocalX(-60, 0.5f).setEaseOutCubic();
        }
        else if (type == NotificationType.Mult && !multShown)
        {
            multShown = true;
            print("New notification created with name: " + name + " and cost: $" + cost);
            GameObject go = Instantiate(NotificationPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0 + (-105 * NotificationList.Count), 0);
            NotificationObject obj = go.GetComponent<NotificationObject>();
            NotificationList.Add(obj);

            obj.type = type;
            obj._name = name;
            obj.cost = cost;
            obj.nameText.text = name;
            obj.costText.text = "Cost: $" + cost;
            go.LeanMoveLocalX(-60, 0.5f).setEaseOutCubic();
        }
        else if (type == NotificationType.CPC && !cpcShown)
        {
            cpcShown = true;
            print("New notification created with name: " + name + " and cost: $" + cost);
            GameObject go = Instantiate(NotificationPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0 + (-105 * NotificationList.Count), 0);
            NotificationObject obj = go.GetComponent<NotificationObject>();
            NotificationList.Add(obj);

            obj.type = type;
            obj._name = name;
            obj.cost = cost;
            obj.nameText.text = name;
            obj.costText.text = "Cost: $" + cost;
            go.LeanMoveLocalX(-60, 0.5f).setEaseOutCubic();
        }
        else if (type == NotificationType.CPS && !cpsShown)
        {
            cpsShown = true;
            print("New notification created with name: " + name + " and cost: $" + cost);
            GameObject go = Instantiate(NotificationPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0 + (-105 * NotificationList.Count), 0);
            NotificationObject obj = go.GetComponent<NotificationObject>();
            NotificationList.Add(obj);

            obj.type = type;
            obj._name = name;
            obj.cost = cost;
            obj.nameText.text = name;
            obj.costText.text = "Cost: $" + cost;
            go.LeanMoveLocalX(-60, 0.5f).setEaseOutCubic();
        }
        else if (type == NotificationType.Prestige && !prestigeShown)
        {
            prestigeShown = true;
            print("New notification created with name: " + name + " and cost: $" + cost);
            GameObject go = Instantiate(NotificationPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0 + (-105 * NotificationList.Count), 0);
            NotificationObject obj = go.GetComponent<NotificationObject>();
            NotificationList.Add(obj);

            obj.type = type;
            obj._name = name;
            obj.cost = cost;
            obj.nameText.text = name;
            obj.costText.text = "Cost: $" + cost;
            go.LeanMoveLocalX(-60, 0.5f).setEaseOutCubic();
        }
    }

    public void DestroyAllNotifications()
    {
        for (int i = 0; i < NotificationList.Count; i++)
        {
            GameObject go = NotificationList[i].gameObject;
            go.LeanMoveLocalX(50, 0.5f).setEaseInCubic().setOnComplete(() =>
            {
                NotificationList.Remove(NotificationList[i]);
                Destroy(go);
            });
        }
    }

    public void DestroyNotification(Notifications.NotificationType type)
    {
        for (int i = 0; i < NotificationList.Count; i++)
        {
            if (NotificationList[i].type == type)
            {
                if (type == NotificationType.Level && lvlShown)
                    lvlShown = false;
                else if (type == NotificationType.Mult && multShown)
                    multShown = false;
                else if (type == NotificationType.CPC && cpcShown)
                    cpcShown = false;
                else if (type == NotificationType.CPS && cpsShown)
                    cpsShown = false;
                else if (type == NotificationType.Prestige && prestigeShown)
                    prestigeShown = false;

                GameObject go = NotificationList[i].gameObject;
                NotificationList.Remove(NotificationList[i]);
                go.LeanMoveLocalX(50, 0.5f).setEaseInCubic().setOnComplete(() =>
                {
                    Destroy(go);
                });
            }
        }
    }

    public enum NotificationType
    {
        Level,
        Mult,
        CPC,
        CPS,
        Prestige,
    }
}