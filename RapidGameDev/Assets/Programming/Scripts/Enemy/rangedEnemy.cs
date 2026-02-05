using UnityEngine;

public class rangedEnemy : baseEnemy
{
    public int currentColor;
    public GameObject enemyRef;

    public void GetCurrentColour()
    {
        string matName = GetComponent<Renderer>().material.name.ToLower();

        if (matName.Contains("cyan")) currentColor = 0;
        else if (matName.Contains("yellow")) currentColor = 1;
        else if (matName.Contains("magenta")) currentColor = 2;
        else
        {
            currentColor = -1;
            Debug.LogWarning("Unknown material: " + matName);
        }
    }
}
