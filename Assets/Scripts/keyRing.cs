using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyRing : MonoBehaviour
{
    private bool hasRed = false;
    private bool hasYellow = false;
    private bool hasBlue = false;
    private bool hasGreen = false;

    /// <summary>
    /// This is the AddKey function that is public. This is accessable from any script and is mainly accessed from the KeyCard script.
    /// It will check, depending on the given parameter (key), if the key.colour is equal to a predefined colour. If it hasn't been picked up and added, then
    /// it will set the variable (hasBlue, hasRed etc.) to true.
    /// </summary>
    //public bool AddKey(KeyCard key)
    //{
    //    if (key.colour == KeyColour.blue)
    //    {
    //        if (hasBlue == false)
    //        {
    //            hasBlue = true;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (key.colour == KeyColour.red)
    //    {
    //        if (hasRed == false)
    //        {
    //            hasRed = true;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (key.colour == KeyColour.green)
    //    {
    //        if (hasGreen == false)
    //        {
    //            hasGreen = true;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (key.colour == KeyColour.yellow)
    //    {
    //        if (hasYellow == false)
    //        {
    //            hasYellow = true;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    /// <summary>
    /// These 4 functions return the values of the corresponding key colour to check if the player has picked them up.
    /// </summary>
    public bool HasBlueKey()
    {
        return hasBlue;
    }
    public bool HasGreenKey()
    {
        return hasGreen;
    }
    public bool HasYellowKey()
    {
        return hasYellow;
    }
    public bool HasRedKey()
    {
        return hasRed;
    }
}
