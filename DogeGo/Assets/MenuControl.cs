using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour 
{
    public Animator bottomPanel;

    public void ToggleBottomPanelPopUp()
    {
        bottomPanel.SetBool("Pop", !bottomPanel.GetBool("Pop"));
    }
	

}
