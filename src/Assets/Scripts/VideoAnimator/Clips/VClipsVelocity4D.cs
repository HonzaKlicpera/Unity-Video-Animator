using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.Video;

public class VelocityDataOutOfBoundsException : System.Exception {}
public class VelocityDataBadFormatException : System.Exception { }

[System.Serializable]
public class VClipsVelocity4D : VClips4D
{
    public TextAsset sideVelocityText;
    public TextAsset upVelocityText;
    public TextAsset downVelocityText;

    public float fallbackConstant = 1.0f;

    float[] sideVelocityData;
    float[] upVelocityData;
    float[] downVelocityData;

    float sideVelocitySum;
    float upVelocitySum;
    float downVelocitySum;

    public float GetVelocityForFrame(LookDirection dir, long frame)
    {
        switch (dir)
        {
            case LookDirection.DOWN:
                return GetVelocityForFrame(downVelocityText, ref downVelocityData, ref downVelocitySum, frame);
            case LookDirection.UP:
                return GetVelocityForFrame(upVelocityText, ref upVelocityData, ref upVelocitySum, frame);
            case LookDirection.LEFT:
            case LookDirection.RIGHT:
                return GetVelocityForFrame(sideVelocityText, ref sideVelocityData, ref sideVelocitySum, frame);
            default:
                throw new UndefinedLookDirectionException();
        }
    }

    public float GetVelocitySum(LookDirection dir)
    {
        switch (dir)
        {
            case LookDirection.DOWN:
                return GetVelocitySum(downVelocityText, ref downVelocityData, ref downVelocitySum, clipDown.clip.frameCount);
            case LookDirection.UP:
                return GetVelocitySum(upVelocityText, ref upVelocityData, ref upVelocitySum, clipUp.clip.frameCount);
            case LookDirection.LEFT:
            case LookDirection.RIGHT:
                return GetVelocitySum(sideVelocityText, ref sideVelocityData, ref sideVelocitySum, clipRight.clip.frameCount);
            default:
                throw new UndefinedLookDirectionException();
        }
    }

    float GetVelocitySum(TextAsset textAsset, ref float[] data, ref float sum, ulong frameCount)
    {
        if (textAsset == null)
            return fallbackConstant * frameCount;
        if (data == null)
            data = ParseVelocityText(textAsset, ref sum);
        return sum;
    }


    float GetVelocityForFrame(TextAsset textAsset, ref float[] data, ref float sum, long frame)
    {
        if (textAsset == null)
            return fallbackConstant;
        else if (frame == -1)
            return 0.0f;

        if (data == null)
            data = ParseVelocityText(textAsset, ref sum);
        if (frame >= data.Length)
        {
            Debug.Log($"frame: {frame}, dataLength: {data.Length}");
            throw new VelocityDataOutOfBoundsException();
        }

        return System.Math.Abs(data[frame]);
    }

    float[] ParseVelocityText(TextAsset textAsset, ref float sum)
    {
        try
        {
            var lines = Regex.Split(textAsset.text, "\r\n|\r|\n")
                .Where(s => s != string.Empty);
            var parsed = lines.Select(line => float.Parse(line))
                .ToArray();
            sum = parsed.Sum();
            return parsed;
        }
        catch (System.FormatException)
        {
            throw new VelocityDataBadFormatException();
        }
    }

    
}
