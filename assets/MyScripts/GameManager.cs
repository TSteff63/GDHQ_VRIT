using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int timelinePosition = 0;

    private void Update()
    {
        switch (timelinePosition)
        {
            case 0:
                //start of the game, coffee light flashes, player is prompted to push this button
                break;

            case 1:
                //Player has attempted to drink coffee, a new sound clip plays prompting the user to press flashing buttons
                //these buttons are on the front middle console level to the player and front middle console above player, roughly 16 buttons will be flashing
                break;

            case 2:
                //on success, another sound clip plays telling player to bress the flashing Throttle button...
                //Once pressed, A new sound clip will play 'launching the ship'
                //Music Track begins to play
                //VO clip will play and player's ship will begin to follow a track out of the larger ship.
                //at end of track, VO clip plays instructing player to flip open a cover that reveals another button to press.  This will enable the warp drive
                break;

            case 3:
                //warp sounds start to play and visuals begin to change
                break;

            default:
                break;
        }
    }
}
