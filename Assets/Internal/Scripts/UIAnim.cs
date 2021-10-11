using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnim : MonoBehaviour
{
    LTDescr tween;
    bool animating = false;

    void Awake()
    {
        LeanTween.delayedCall(0, () => { });
    }

    public void UISlideIn(string command)
    {
        print("UISlideIn called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Vector3 move = StringToVector3(valueString, trans);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanMoveLocal(trans, trans.localPosition + move, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanMoveLocal(trans, trans.localPosition + move, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            });
        }
    }

    public void UISlideOut(string command)
    {
        print("UISlideOut called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Vector3 move = StringToVector3(valueString, trans);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanMoveLocal(trans, trans.localPosition + move, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanMoveLocal(trans, trans.localPosition + move, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            });
        }
    }

    public void UIScaleIn(string command)
    {
        print("UIScaleIn called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Vector3 scale = StringToVector3Scale(valueString, trans);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanScale(trans, trans.localScale + scale, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanScale(trans, trans.localScale + scale, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            });
        }
    }

    public void UIScaleOut(string command)
    {
        print("UIScaleOut called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Vector3 scale = StringToVector3Scale(valueString, trans);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanScale(trans, trans.localScale + scale, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanScale(trans, trans.localScale + scale, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            });
        }
    }

    public void UIFadeIn(string command)
    {
        print("UIFadeIn called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        print("valuestring: " + valueString);
        float value = float.Parse(valueString);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanAlpha(trans.GetComponent<CanvasGroup>(), value, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanAlpha(trans.GetComponent<CanvasGroup>(), value, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            });
        }
    }

    public void UIFadeOut(string command)
    {
        print("UIFadeOut called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        float value = float.Parse(valueString);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanAlpha(trans.GetComponent<CanvasGroup>(), value, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            }).setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanAlpha(trans.GetComponent<CanvasGroup>(), value, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            });
        }
    }

    public void UIBlurIn(string command)
    {
        print("UIBlurIn called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Color color = StringToColor(valueString);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanBlur(trans.gameObject, color.a, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            }).setDelay(delay);
            LeanTweenExt.LeanColorMaterial(trans.gameObject, color, time).setEaseOutCubic().setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanBlur(trans.gameObject, color.a, time).setEaseOutCubic().setOnStart(() =>
            {
                trans.gameObject.SetActive(true);
            });
            LeanTweenExt.LeanColorMaterial(trans.gameObject, color, time).setEaseOutCubic();
        }
    }

    public void UIBlurOut(string command)
    {
        print("UIBlurOut called. Command is: " + command + ". And animating is: " + animating);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string valueString = command.Replace(nameString + "*", "");
        valueString = valueString.Remove(valueString.IndexOf("*"));
        Color color = StringToColor(valueString);

        string timeString = command.Replace(nameString + "*" + valueString + "*", "");
        if (timeString.Contains("*"))
        {
            timeString = timeString.Remove(timeString.IndexOf("*"));
            float time = float.Parse(timeString);

            string delayString = command.Substring(command.LastIndexOf("*") + 1);
            float delay = float.Parse(delayString);

            LeanTweenExt.LeanBlur(trans.gameObject, color.a, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            }).setDelay(delay);
            LeanTweenExt.LeanColorMaterial(trans.gameObject, color, time).setEaseOutCubic().setDelay(delay);
        }
        else
        {
            float time = float.Parse(timeString);

            LeanTweenExt.LeanBlur(trans.gameObject, color.a, time).setEaseOutCubic().setOnComplete(() =>
            {
                trans.gameObject.SetActive(false);
            });
            LeanTweenExt.LeanColorMaterial(trans.gameObject, color, time).setEaseOutCubic();
        }
    }

    public void UIDelayedOff(string command)
    {
        print("UIDelayedOff called. Command is: " + command);
        if (tween != null)
            LeanTween.cancel(tween.uniqueId);
        int splitterIndex = command.IndexOf("*");
        string nameString = command.Remove(splitterIndex);
        Transform trans = transform.Find(nameString);

        string delayString = command.Substring(command.LastIndexOf("*") + 1);
        float delay = float.Parse(delayString);

        LeanTween.delayedCall(delay, () =>
        {
            trans.gameObject.SetActive(false);
        });
    }

    Vector3 StringToVector3(string input, Transform trans)
    {
        if (input.StartsWith("(") && input.EndsWith(")"))
            input = input.Substring(1, input.Length - 2);

        string[] nums = input.Split(',');
        Vector3 vector = new Vector3(
            nums[0] == "~" ? trans.localPosition.x : float.Parse(nums[0]),
            nums[1] == "~" ? trans.localPosition.y : float.Parse(nums[1]),
            nums[2] == "~" ? trans.localPosition.z : float.Parse(nums[2]));

        print("Successfully parsed " + vector + " using the string: " + input + " and transform: " + trans);
        return vector;
    }

    Vector3 StringToVector3Scale(string input, Transform trans)
    {
        if (input.StartsWith("(") && input.EndsWith(")"))
            input = input.Substring(1, input.Length - 2);

        string[] nums = input.Split(',');
        Vector3 vector = new Vector3(
            nums[0] == "~" ? trans.localScale.x : float.Parse(nums[0]),
            nums[1] == "~" ? trans.localScale.y : float.Parse(nums[1]),
            nums[2] == "~" ? trans.localScale.z : float.Parse(nums[2]));

        print("Successfully parsed " + vector + " using the string: " + input + " and transform: " + trans);
        return vector;
    }

    Color StringToColor(string input)
    {
        if (input.StartsWith("(") && input.EndsWith(")"))
            input = input.Substring(1, input.Length - 2);

        print("new input to parse a color from: " + input);
        string[] nums = input.Split(',');
        Color col = new Color(
            float.Parse(nums[0]),
            float.Parse(nums[1]),
            float.Parse(nums[2]),
            float.Parse(nums[3]));

        print("Successfully parsed " + col + " using the string: " + input);
        return col;
    }
}