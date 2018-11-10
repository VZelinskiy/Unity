using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System;
using UnityEngine.UI;

public class ProfilesList : MonoBehaviour
{
    public List<PlayerProfile> PlayerProfiles;
    public Transform ContentPanel;
    public ObjectPool ButtonObjectPool;
    public ProfileButton CurrentButton;

    void Start()
    {
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    public void AddButtons()
    {
        for (int i = 0; i < PlayerProfiles.Count; i++)
        {
            PlayerProfile playerProfile = PlayerProfiles[i];
            GameObject newButton = ButtonObjectPool.GetObject();
            newButton.transform.SetParent(ContentPanel);
            ProfileButton profileButton = newButton.GetComponent<ProfileButton>();
            profileButton.Setup(playerProfile, this);
        }
    }

    public void RemoveButtons()
    {
        while (ContentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            ButtonObjectPool.ReturnObject(toRemove);
        }
    }

    public void AddProfile()
    {
        name = GameObject.Find("ProfileNameInputField").GetComponent<InputField>().text;
        PlayerProfiles.Add(new PlayerProfile(name));
        RefreshDisplay();
    }

    public void RemoveProfile(ProfileButton currentButton)
    {
        PlayerProfiles.Remove(currentButton.playerProfile);
        RefreshDisplay();
    }

    public void EditProfile(PlayerProfile playerProfile)
    {
        playerProfile.Name = GameObject.Find("ProfileNameInputField").GetComponent<InputField>().text;
    }

}
