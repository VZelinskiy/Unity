using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    public Button button;
    public Text nameText;

    public PlayerProfile playerProfile;
    public ProfilesList profilesList;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Setup(PlayerProfile currentProfile, ProfilesList currentProfileList)
    {
        playerProfile = currentProfile;
        nameText.text = playerProfile.Name;

        profilesList = currentProfileList;
    }

    public void CurrentButton(Button currentButton)
    {

    }
}
